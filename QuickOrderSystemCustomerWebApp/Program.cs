using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using QuickOrderSystemCustomerWebApp;
using QuickOrderSystemCustomerWebApp.Data;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7278") });
builder.Services.AddScoped<ProductCartService>();
builder.Services.AddScoped<DataService>();

await builder.Build().RunAsync();
