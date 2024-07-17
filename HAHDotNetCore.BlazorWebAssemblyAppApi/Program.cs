using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HAHDotNetCore.BlazorWebAssemblyAppApi;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string domain = "https://localhost:7131";
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(domain) });

await builder.Build().RunAsync();

