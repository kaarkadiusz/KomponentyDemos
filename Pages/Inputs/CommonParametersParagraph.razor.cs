using Komponenty.Inputs;
using Komponenty.Utilities;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace Piaskownica.Pages.Inputs
{
    public partial class CommonParametersParagraph
    {
        [Parameter]
        public string[] ParameterNames { get; set; } = [];
        [Parameter]
        public Type? ComponentType { get; set; }

        protected override Task OnInitializedAsync()
        {
            if (ComponentType is not null)
            {
                var props = ComponentType
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).ToList();

                ParameterNames = [.. ComponentType
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(p => p.IsParameter && IsPropertyInheritedFromInputBase(p))
                    .Select(p => p.Name)];
            }
            return base.OnInitializedAsync();
        }

        private static bool IsPropertyInheritedFromInputBase(PropertyInfo prop) => prop.DeclaringType is not null && prop.DeclaringType.IsGenericType && prop.DeclaringType.GetGenericTypeDefinition() == typeof(KAInputBase<>);
    }
}