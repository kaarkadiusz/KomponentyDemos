using Microsoft.AspNetCore.Components;
using Piaskownica.Services;
using static Piaskownica.Services.ThemeAgent;

namespace Piaskownica.Layout.Sidebar
{
    public partial class ThemeChanger
    {
        [Inject]
        private ThemeAgent ThemeAgent { get; set; } = null!;

        private async Task ChangeTheme(ThemeMode themeMode)
        {
            await ThemeAgent.SetModeAsync(themeMode);
        }
    }
}
