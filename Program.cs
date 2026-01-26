using Komponenty.Utilities;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Piaskownica;
using Piaskownica.Classes;
using Piaskownica.Services;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddLocalization();  

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<ThemeAgent>();

builder.Services.AddKomponentyJavascript();

var host = builder.Build();

await host.SetDefaultCulture();
await host.SetTheme();

await host.RunAsync();
