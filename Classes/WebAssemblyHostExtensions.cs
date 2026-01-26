using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Piaskownica.Metadata;
using Piaskownica.Services;
using System.Globalization;

namespace Piaskownica.Classes
{
    public static class WebAssemblyHostExtensions
    {
        public static async Task SetDefaultCulture(this WebAssemblyHost host)
        {
            LocalStorageService localStorage = host.Services.GetRequiredService<LocalStorageService>();
            string? culture = await localStorage.GetValueAsync<string>(BrowserStorageKey.Culture);

            CultureInfo cultureInfo;
            if(culture is null || !CultureInfo.GetCultures(CultureTypes.AllCultures).Select(c => c.Name).Contains(culture))
            {
                cultureInfo = new("en-GB");
            }
            else
            {
                cultureInfo = new(culture);
            }

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
        
        public static async Task SetTheme(this WebAssemblyHost host)
        {
            ThemeAgent themeAgent = host.Services.GetRequiredService<ThemeAgent>();
            await themeAgent.ReadModeAsync();
        }
    }
}
