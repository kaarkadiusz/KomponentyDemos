using Microsoft.AspNetCore.Components;

namespace Piaskownica.Components.SimplePage
{
    public partial class SimplePageParagraph
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
