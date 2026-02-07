using Microsoft.AspNetCore.Components;
using Piaskownica.Services;
using static Piaskownica.Services.ThemeAgent;

namespace Piaskownica.Layout.Sidebar
{
    public partial class ThemeChanger
    {
        [Inject]
        private ThemeAgent ThemeAgent { get; set; } = null!;

        private ThemeMode Mode { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Mode = await ThemeAgent.GetModeAsync();
            await base.OnInitializedAsync();
        }

        private async Task ChangeTheme(ThemeMode themeMode)
        {
            await ThemeAgent.SetModeAsync(themeMode);
            Mode = await ThemeAgent.GetModeAsync();
        }

        private string IconFunc(ThemeMode themeMode)
        {
            return themeMode switch
            {
                ThemeMode.Light => Komponenty.Icons.Preset.Outline.Sun,
                ThemeMode.Dark => Komponenty.Icons.Preset.Fill.Moon,
                _ => Komponenty.Icons.Preset.Outline.QuestionMark,
            };
        }
    }
}
