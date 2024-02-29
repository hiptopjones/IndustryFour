using IndustryFour.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("QuotesAPI",
    client => client.BaseAddress = new Uri(builder.Configuration["BaseServiceUrl"]));

var app = builder.Build();
await app.RunAsync();
