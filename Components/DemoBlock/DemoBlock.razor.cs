using Microsoft.AspNetCore.Components;
using Piaskownica.Classes;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Piaskownica.Components.DemoBlock
{
    public partial class DemoBlock<T> : WasmComponentBase where T : IComponent
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public DemoBlockDisplayStyle DisplayStyle { get; set; }

        private string SourceCode { get; set; } = string.Empty;
        private bool ShowSourceCode { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await LoadSource();
        }

        private Task LoadSource()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith($"{typeof(T).Namespace}.{typeof(T).Name}.razor")) ?? throw new InvalidOperationException("Source file was not found.");
            Stream stream = assembly.GetManifestResourceStream(resourceName) ?? throw new Exception($"{nameof(assembly.GetManifestResourceStream)} returned null.");
            using StreamReader reader = new(stream);
            SourceCode = reader.ReadToEnd();
            return Task.CompletedTask;  
        }

        private string GetMainCssClass()
        {
            StringBuilder sb = new();
            sb.AppendLine("demo");
            if(ShowSourceCode)
            {
                sb.AppendLine("showsourcecode");
            }
            sb.AppendLine($"displaystyle-{DisplayStyle.ToString()}");
            return sb.ToString();
        }

        private Task ToggleShowSourceCode()
        {
            ShowSourceCode = !ShowSourceCode;
            return Task.CompletedTask;
        }
    }

    public enum DemoBlockDisplayStyle
    {
        FlexCenter = 0,
        GridColumnCenter = 1
    }
}
