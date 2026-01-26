using System.Reflection;

namespace Piaskownica.Layout.Sidebar
{
    public partial class Sidebar
    {
        private bool _isDebug =
#if DEBUG
        true;
#else
        false;
#endif

        private List<SidebarItem> Items { get; set; } = [];

        protected override Task OnInitializedAsync()
        {
            InitializePages();
            return base.OnInitializedAsync();
        }

        private Task InitializePages()
        {
            Items.Clear();

            Items.AddRange(GetConstStrings(typeof(Metadata.Pages)).Select(CreateSidebarItemLink));

            Type[] groups = typeof(Metadata.Pages).GetNestedTypes(BindingFlags.Public);
            foreach(Type group in groups)
            {
                Items.Add(new SidebarItemGroup() { Title = group.Name, Children = [.. GetConstStrings(group).Select(CreateSidebarItemLink)] });
            }

            Items.Sort((x, y) => x.Title.CompareTo(y.Title));

            if(!_isDebug)
            {
                Items.RemoveAll(item => item.Title.StartsWith("test", StringComparison.InvariantCultureIgnoreCase));
            }

            return Task.CompletedTask;
        }

        private static IEnumerable<ConstStringField> GetConstStrings(Type type)
        {
            string getFieldName(FieldInfo field) => field.GetCustomAttribute<Metadata.TranslatedAttribute>()?.Localized ?? field.Name;
            string getFieldValue(FieldInfo field) => (string)field.GetRawConstantValue()!;

            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
                .Select(f => new ConstStringField(getFieldName(f), getFieldValue(f)));
        }
        private record ConstStringField(string Name, string Value);
        private SidebarItemLink CreateSidebarItemLink(ConstStringField constStringField)
        {
            return new SidebarItemLink() { Title = constStringField.Name, Href = constStringField.Value };
        }

        private abstract class SidebarItem
        {
            public required string Title { get; set; }
        }

        private class SidebarItemGroup : SidebarItem
        {
            public required SidebarItemLink[] Children { get; set; }
            public bool IsExpanded { get; set; } = true;
        }

        private class SidebarItemLink : SidebarItem
        {
            public required string Href { get; set; }

        }
    }
}
