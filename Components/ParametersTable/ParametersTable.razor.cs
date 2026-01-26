using Komponenty.Utilities;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;

namespace Piaskownica.Components.ParametersTable
{
    public partial class ParametersTable<T> where T : IComponent
    {
        private List<ParameterToDisplay> Parameters { get; set; } = [];

        protected override Task OnInitializedAsync()
        {
            List<PropertyInfo> componentParameters = [.. typeof(T).GetProperties().Where(prop => prop.IsParameter)];
            T defaultComponent = Activator.CreateInstance<T>();
            Parameters.Clear();
            foreach (PropertyInfo componentParameter in componentParameters)
            {
                ParameterToDisplay parameterToDisplay = new()
                {
                    Property = componentParameter,
                    DefaultValue = componentParameter.GetValue(defaultComponent)
                };
                Parameters.Add(parameterToDisplay);
            }

            Parameters.Sort((a, b) => a.Name.CompareTo(b.Name));

            return base.OnInitializedAsync();
        }

        private record ParameterToDisplay
        {
            public required PropertyInfo Property { get; set; }
            public required object? DefaultValue { get; set; }

            public string Name => Property.Name;
            public Type ParameterType => Property.PropertyType;
            public string Type => Komponenty.Utilities.TypeHelper.GetTypeString(ParameterType);
            public string Default => GetDefaultValueString();

            private string GetDefaultValueString()
            {
                if (DefaultValue is null)
                {
                    return "null";
                }
                if (DefaultValue is string str)
                {
                    if(string.IsNullOrEmpty(str))
                    {
                        return str is null ? "null" : "\"\"";
                    }
                    if(str.StartsWith("<svg") && str.EndsWith("svg>"))
                    {
                        return "<svg>…</svg>";
                    }
                }
                Type type = DefaultValue.GetType();
                if (type == typeof(EventCallback) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(EventCallback<>)))
                {
                    return nameof(EventCallback);
                }

                return DefaultValue.ToString() ?? string.Empty;
            }
        }
    }
}
