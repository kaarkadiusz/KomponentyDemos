using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace Piaskownica.Classes
{
    public static class LocalizationBase
    {
        private const string _isoCodeEN = "en";
        private const string _isoCodePL = "pl";
        public static LocalizationVariant CurrentLocalization
        {
            get
            {
                return CultureInfo.DefaultThreadCurrentUICulture?.TwoLetterISOLanguageName switch
                {
                    _isoCodePL => LocalizationVariant.PL,
                    _ or _isoCodeEN => LocalizationVariant.EN,
                };
            }
        }

        public enum LocalizationVariant
        {
            EN,
            PL
        }
    }
}
