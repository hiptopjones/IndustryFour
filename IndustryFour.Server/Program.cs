using IndustryFour.Server;
using IndustryFour.Server.Context;
using IndustryFour.Server.Repositories;
using IndustryFour.Server.Retrieval;
using IndustryFour.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using NLog;
using NLog.Web;
using System.Text.Json.Serialization;


var logger = LogManager.Setup().LoadConfigurationFromFile(Path.Combine(Directory.GetCurrentDirectory(), "nlog.config")).GetCurrentClassLogger();
logger.Debug("Main program setup");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            // Prevent problems with EF Core doing automatic fix-up of navigation properties leading to cycles
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("OpenPolicy",
            builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("*");
            });
    });

    builder.Services.Configure<IISOptions>(options => { });

    builder.Services.AddSingleton<QuotesService>();
    builder.Services.AddScoped<DocumentStoreDbContext>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
    builder.Services.AddScoped<IDocumentService, DocumentService>();
    builder.Services.AddScoped<IChunkRepository, ChunkRepository>();
    builder.Services.AddScoped<IChunkService, ChunkService>();
    builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
    builder.Services.AddScoped<IConversationService, ConversationService>();
    builder.Services.AddScoped<ITurnRepository, TurnRepository>();
    builder.Services.AddScoped<ITurnService, TurnService>();
    builder.Services.AddScoped<IDocumentIndexService, DocumentIndexService>();
    builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
    builder.Services.AddScoped<IQuestionService, QuestionService>();
    builder.Services.AddScoped<IChatService, ChatService>();
    builder.Services.AddScoped<ITextSplitter, TextSplitter>(sp =>
        new TextSplitter(
            sp.GetRequiredService<ILogger<TextSplitter>>(),
            separator: ". ", // Include space so we don't split on 4.0
            chunkSize: 1000,
            chunkOverlap: 300));
    builder.Services.AddScoped<IEmbeddingProvider, OllamaEmbeddingProvider>();
    builder.Services.AddScoped<IChatProvider, OpenAiChatProvider>();
    //builder.Services.AddScoped<IChatProvider, OllamaChatProvider>();

    builder.Services.AddNpgsql<DocumentStoreDbContext>(builder.Configuration.GetConnectionString("sqlConnection"));

    builder.Services.AddHttpClient();

    builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();

        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseCors("OpenPolicy");
    app.UseAuthorization();

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
        RequestPath = new PathString("/StaticFiles")
    });

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Error during program setup");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}