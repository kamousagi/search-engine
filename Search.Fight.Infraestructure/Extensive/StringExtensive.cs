using System.Text.RegularExpressions;

namespace Search.Fight.Infraestructure.Extensive
{
    public static class StringExtensive
    {

        public static string ToCorrect(this string text)
        {
            return text;
        }

        public static string Clear(this string text)
        {
            Regex rgx = new Regex("^[a-zA-Z0-9]");
            return rgx.Replace(text, "");
        }

    }
}