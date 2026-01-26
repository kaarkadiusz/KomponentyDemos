using Microsoft.AspNetCore.Components;
using System.Security;

namespace Piaskownica.Components.SimplePage
{
    public partial class SimplePageArticleHeading
    {
        [CascadingParameter(Name = "Context")]
        public SimplePageContext Context { get => field ?? throw new ArgumentNullException(nameof(Context)); set => field = value; }
        [Parameter]
        public string Text { get; set; } = string.Empty;
        [Parameter]
        public byte Level
        {
            get => field;
            set
            {
                if (value is < 1 or > 6)
                {
                    throw new ArgumentOutOfRangeException(nameof(Level));
                }
                field = value;
            }
        } = 1;

        public string ElementId => $"{nameof(SimplePageArticleHeading)}-{GetHashCode()}";

        protected override Task OnInitializedAsync()
        {
            Context.AddHeading(this);
            return base.OnInitializedAsync();
        }
    }

}
