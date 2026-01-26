using Komponenty.Inputs;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace Piaskownica.Pages.Inputs
{
    public partial class ValidTypesParagraph
    {
        [Parameter]
        public string[] ValidTypes { get; set; } = [];
        [Parameter]
        public Type? ComponentType { get; set; }

        protected override Task OnInitializedAsync()
        {
            if (ComponentType is not null)
            {
                if (ComponentType.GetProperty(nameof(KAInputBase<>.ValidTypes)) is not PropertyInfo validTypesProperty ||
                    validTypesProperty.PropertyType != typeof(Type[]))
                {
                    return base.OnInitializedAsync();
                }

                Type[]? validTypes = (Type[]?)validTypesProperty.GetValue(Activator.CreateInstance(ComponentType));
                if (validTypes is null)
                {
                    return base.OnInitializedAsync();
                }

                ValidTypes = [.. validTypes.Select(Komponenty.Utilities.TypeHelper.GetTypeString)];
            }

            return base.OnInitializedAsync();
        }
    }
}