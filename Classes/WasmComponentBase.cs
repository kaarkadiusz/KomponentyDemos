using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Piaskownica.Metadata;

namespace Piaskownica.Classes
{
    public abstract class WasmComponentBase : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; } = null!;

        protected Task NavigateTo(string url)
        {
            NavigationManager.NavigateTo(url);
            return Task.CompletedTask;
        }
    }
}
