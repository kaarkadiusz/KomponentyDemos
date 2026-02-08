using Microsoft.AspNetCore.Components;
using Piaskownica.Metadata;
using Piaskownica.Services;
using System.Globalization;

namespace Piaskownica.Layout.Sidebar
{
    public partial class CultureChanger
    {
        [Inject]
        private LocalStorageService LocalStorage { get; set; } = null!;
        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;

        private CultureInfo[] SupportedCultures { get; } = [
            new CultureInfo("en-GB"),
            new CultureInfo("pl-PL"),
            ];

        private CultureInfo? CurrentCulture => CultureInfo.DefaultThreadCurrentCulture;

        private async Task ChangeCulture(CultureInfo cultureInfo)
        {
            CultureInfo? currentCulture = CultureInfo.DefaultThreadCurrentCulture;
            if(currentCulture?.Name == cultureInfo.Name)
            {
                return;
            }

            await LocalStorage.SetValueAsync(BrowserStorageKey.Culture, cultureInfo.Name);
            NavigationManager.Refresh();
        }
    }
}
