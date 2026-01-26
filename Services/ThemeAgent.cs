using Microsoft.JSInterop;
using Piaskownica.Metadata;

namespace Piaskownica.Services
{
    public class ThemeAgent
    {
        private IJSRuntime JSRuntime { get; }
        private LocalStorageService LocalStorage { get; }

        public ThemeAgent(IJSRuntime jSRuntime, LocalStorageService localStorage)
        {
            JSRuntime = jSRuntime;
            LocalStorage = localStorage;
        }

        public async Task ReadModeAsync()
        {
            var themeModeString = await LocalStorage.GetValueAsync<string>(BrowserStorageKey.ThemeMode);
            if (!Enum.TryParse(themeModeString, out ThemeMode themeMode))
            {
                themeMode = ThemeMode.Light;
            }
            await SetThemeUsingJs(themeMode);
        }
        public async Task SetModeAsync(ThemeMode themeMode)
        {
            await LocalStorage.SetValueAsync(BrowserStorageKey.ThemeMode, themeMode.ToString());
            await SetThemeUsingJs(themeMode);
        }

        private async Task SetThemeUsingJs(ThemeMode themeMode)
        {
            await JSRuntime.InvokeVoidAsync(JavascriptFunctionName.SetTheme, themeMode.ToString().ToLower());
        }

        public enum ThemeMode
        {
            Light = 0,
            Dark = 1,
        }
    }
}
