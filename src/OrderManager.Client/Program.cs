using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OrderManager.Client;
using MudBlazor.Services;
using OrderManager.Integration.Http.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});
builder.Services.AddMudServices();
builder.Services.AddHttpOrderManagerServer("http://localhost:5227/");
    
await builder.Build().RunAsync();