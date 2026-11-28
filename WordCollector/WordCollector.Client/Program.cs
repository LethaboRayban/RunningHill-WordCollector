using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WordCollector.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseUrl = builder.Configuration["ApiBaseUrl"];

builder.Services.AddScoped(sp => new HttpClient 
{
    BaseAddress = string.IsNullOrWhiteSpace(apiBaseUrl)
        ? new Uri(builder.HostEnvironment.BaseAddress)
        : new Uri(apiBaseUrl)
});

await builder.Build().RunAsync();
