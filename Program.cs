using CloudflareBlazorTrial;
using CloudflareBlazorTrial.Clients;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string webApiBaseAddress = builder.Configuration["webApiBaseAddress"] ?? string.Empty;
string azureFunctionsBaseAddress = builder.Configuration["azureFunctionsBaseAddress"] ?? string.Empty;

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(webApiBaseAddress) });
builder.Services.AddScoped(sp => new AzureFunctionsClient(new HttpClient { BaseAddress = new Uri(azureFunctionsBaseAddress) }));

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
});

await builder.Build().RunAsync();
