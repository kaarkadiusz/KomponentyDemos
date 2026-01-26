using Microsoft.AspNetCore.Components;

namespace Piaskownica.Components.SimplePage
{
    public partial class SimplePageArticle
    {
        [CascadingParameter(Name = "Context")]
        public SimplePageContext Context { get => field ?? throw new ArgumentNullException(nameof(Context)); set => field = value; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        protected override Task OnInitializedAsync()
        {
            Context.AddArticle(this);
            return base.OnInitializedAsync();
        }
    }
}
