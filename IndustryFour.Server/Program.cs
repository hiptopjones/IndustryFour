using IndustryFour.Server;
using IndustryFour.Server.Context;
using IndustryFour.Server.Repositories;
using IndustryFour.Server.Retrieval;
using IndustryFour.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using NLog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

LogManager.Setup().LoadConfigurationFromFile(Path.Combine(Directory.GetCurrentDirectory(), "nlog.config"));

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
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.Configure<IISOptions>(options => { });

builder.Services.AddSingleton<QuotesService>();
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddScoped<DocumentStoreDbContext>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IDocumentIndex, DocumentIndex>();
builder.Services.AddScoped<ITextSplitter, TextSplitter>(sp => 
    new TextSplitter(
        separator: ".",
        chunkSize: 1000,
        chunkOverlap: 300));
builder.Services.AddScoped<IEmbeddingProvider, OllamaEmbeddingProvider>();
builder.Services.AddScoped<IVectorStore, ChromaDbVectorStore>();
builder.Services.AddScoped<IChatProvider, OpenAiChatProvider>();

builder.Services.AddSqlite<DocumentStoreDbContext>(builder.Configuration.GetConnectionString("sqlConnection"));

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
app.UseAuthorization();
app.UseCors();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
    RequestPath = new PathString("/StaticFiles")
});

app.MapControllers();

app.Run();
