using Piaskownica.Classes;
using static Piaskownica.Classes.LocalizationBase;

namespace Piaskownica.Metadata
{
    public static class Pages
    {
        private const string DocBase = "doc";

        public const string Button = $"{DocBase}/button";
        public const string Icon = $"{DocBase}/icon";

        public static class Inputs
        {
            private const string Base = $"{DocBase}/inputs";

            [Translated(nameof(Introduction), "Wprowadzenie")]
            public const string Introduction = $"{Base}/introduction";
            [Translated("Common parameters", "Wspólne parametry")]
            public const string CommonParameters = $"{Base}/commonparameters";
            [Translated(nameof(Validation), "Walidacja")]
            public const string Validation = $"{Base}/validation";
            public const string InputCheckbox = $"{Base}/inputcheckbox";
            public const string InputDate = $"{Base}/inputdate";
            public const string InputNumber = $"{Base}/inputnumber";
            public const string InputRadio = $"{Base}/inputradio";
            public const string InputSelect= $"{Base}/inputselect";
            public const string InputText = $"{Base}/inputtext";
            public const string InputTextArea= $"{Base}/inputtextarea";
        }

        public const string Toggle = "toggle";

        public const string Test = "test";
    }

    [System.AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class TranslatedAttribute : Attribute
    {
        private readonly string _en;
        private readonly string _pl;
        public string Localized => CurrentLocalization switch
        {
            LocalizationVariant.PL => _pl,
            _ or LocalizationVariant.EN => _en,
        };

        public TranslatedAttribute(string en, string pl)
        {
            _en = en;
            _pl = pl;
        }
    }
}
