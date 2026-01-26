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

        private static string GetFlagEmoji(CultureInfo culture)
        {
            var region = new RegionInfo(culture.Name);
            string countryCode = region.TwoLetterISORegionName.ToUpperInvariant();

            const int regionalIndicatorBase = 0x1F1E6;

            int first = regionalIndicatorBase + (countryCode[0] - 'A');
            int second = regionalIndicatorBase + (countryCode[1] - 'A');

            return char.ConvertFromUtf32(first) + char.ConvertFromUtf32(second);
        }

    }
}
