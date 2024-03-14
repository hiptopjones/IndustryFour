using Blazored.Toast;
using IndustryFour.Client;
using IndustryFour.Client.HttpInterceptor;
using IndustryFour.Client.HttpRepository;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("CoreAPI", (provider, client) =>
{
    client.BaseAddress = new Uri(builder.Configuration["CoreServiceUrl"]);
    client.EnableIntercept(provider);
});

builder.Services.AddHttpClientInterceptor();

builder.Services.AddScoped(provider => provider.GetService<IHttpClientFactory>().CreateClient("CoreAPI"));

builder.Services.AddScoped<IDocumentHttpRepository, DocumentHttpRepository>();

builder.Services.AddScoped<HttpInterceptorService>();

builder.Services.AddBlazoredToast();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();
await app.RunAsync();
