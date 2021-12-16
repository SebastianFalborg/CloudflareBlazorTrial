using CloudflareBlazorTrial;
using CloudflareBlazorTrial.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string webApiBaseAddress = builder.Configuration["webApiBaseAddress"] ?? string.Empty;
string azureFunctionsBaseAddress = builder.Configuration["azureFunctionsBaseAddress"] ?? string.Empty;

builder.Services.AddScoped(sp => new WebApiHttpClient { BaseAddress = new Uri(webApiBaseAddress) });
builder.Services.AddScoped(sp => new AzureFunctionsHttpClient { BaseAddress = new Uri(azureFunctionsBaseAddress) });

await builder.Build().RunAsync();
