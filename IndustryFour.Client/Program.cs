using IndustryFour.Client;
using IndustryFour.Client.HttpRepository;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("CoreAPI",
    client => client.BaseAddress = new Uri(builder.Configuration["CoreServiceUrl"]));

builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("CoreAPI"));

builder.Services.AddScoped<IDocumentHttpRepository, DocumentHttpRepository>();

var app = builder.Build();
await app.RunAsync();
