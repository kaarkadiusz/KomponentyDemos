using Microsoft.AspNetCore.Components;
using Piaskownica.Services;
using System.Threading.Tasks;
using static Piaskownica.Services.ThemeAgent;

namespace Piaskownica.Layout
{
    public partial class MainLayout
    {
        [Inject]
        private ThemeAgent ThemeAgent { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await ThemeAgent.ReadModeAsync();
        }
    }
}
