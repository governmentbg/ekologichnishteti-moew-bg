using System.Text.RegularExpressions;

namespace Zopoesht.Infrastructure.Helpers.Extensions
{
    public static class ValidatePropertiesHelperExtension
    {
        public static bool IsValidEmail(string value)
        {
            return new Regex("[A-Za-z0-9._%+\\-]{2,}@[a-zA-Z0-9\\-]{1,}([.]{1}[a-zA-Z0-9]{2,}|[.]{1}[a-zA-Z0-9\\-]{2,}[.]{1}[a-zA-Z\\-]{2,})|([.]{1}[a-zA-Z\\-]{2,}[.]{1}[a-zA-Z\\-]{2,})").IsMatch(value);
        }

        public static bool IsValidPassword(string value)
        {
            return new Regex("^(?=.*[a-z])(?=.*[0-9])(?=.*[A-Z]).{6,}$").IsMatch(value);
        }

        public static bool IsValidLatinName(string value)
        {
            return new Regex("^[a-zA-Z, -]+$").IsMatch(value);
        }

        public static bool IsValidNumber(string value)
        {
            return new Regex("^[0-9]+$").IsMatch(value);
        }

        public static bool IsValidCyrillicName(string value)
        {
            return new Regex("^[аАбБвВгГдДеЕжЖзЗиИйЙкКлЛмМнНоОпПрРсСтТуУфФхХцЦчЧшШщЩьъЪюЮяЯ, -.]+$").IsMatch(value);
        }

        public static bool IsValidLatinAndCyrillicName(string value)
        {
            return new Regex("^[A-Za-zА-Яа-я\\-\\s]+$").IsMatch(value);
        }

        public static bool IsValidPhoneNumber(string value)
        {
            return new Regex("^[0-9, +-]+$").IsMatch(value);
        }

        public static bool IsValidLettersAndNumbers(string value)
        {
            return new Regex("^[A-Za-zаАбБвВгГдДеЕжЖзЗиИйЙкКлЛмМнНоОпПрРсСтТуУфФхХцЦчЧшШщЩьъЪюЮяЯ,0-9, -.]+$").IsMatch(value);
        }

        public static bool IsValidDiplomaNumber(string value)
        {
            return new Regex("^[A-Za-zаАбБвВгГдДеЕжЖзЗиИйЙкКлЛмМнНоОпПрРсСтТуУфФхХцЦчЧшШщЩьъЪюЮяЯ,0-9,-]+$").IsMatch(value);
        }

        public static bool IsValidLatinAndNumber(string value)
        {
            return new Regex("^[a-zA-Z,0-9\\/\\- ]+$").IsMatch(value);
        }

        public static bool IsValidLatinAndNumberOnly(string value)
        {
            return new Regex("^[a-zA-Z, 0-9]+$").IsMatch(value);
        }

        public static bool IsValidCyrillicAndNumber(string value)
        {
            return new Regex("^[аАбБвВгГдДеЕжЖзЗиИйЙкКлЛмМнНоОпПрРсСтТуУфФхХцЦчЧшШщЩьъЪюЮяЯ,0-9, -.]+$").IsMatch(value);
        }

        public static bool IsValidLettersNumbersAndSymbols(string value)
        {
            return new Regex("^[A-Za-zаАбБвВгГдДеЕжЖзЗиИйЙкКлЛмМнНоОпПрРсСтТуУфФхХцЦчЧшШщЩьъЪюЮяЯ,0-9, -.'\"\\:\\;]+$").IsMatch(value);
        }
    }
}
