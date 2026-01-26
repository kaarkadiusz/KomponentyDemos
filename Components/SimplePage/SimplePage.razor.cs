using Komponenty;
using Microsoft.AspNetCore.Components;

namespace Piaskownica.Components.SimplePage
{
    public partial class SimplePage : IDisposable
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        private KAJavascriptService JavascriptService { get; set; } = null!;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        public SimplePageContext Context { get; } = new();

        protected override Task OnInitializedAsync()
        {
            Context.OnArticleAdded += RefreshUI;
            Context.OnHeadingAdded += RefreshUI;
            return base.OnInitializedAsync();
        }

        private async Task ScrollToHeading(string elementId)
        {
            await JavascriptService.ScrollToElement(elementId);
        }

        private async void RefreshUI()
        {
            await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            Context.OnArticleAdded -= RefreshUI;
            Context.OnHeadingAdded -= RefreshUI;
            GC.SuppressFinalize(this);
        }
    }
}
