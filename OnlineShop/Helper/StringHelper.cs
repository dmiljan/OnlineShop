using System.Text.RegularExpressions;

namespace OnlineShop.Helper
{
    public class StringHelper
    {
        public static string ConvertToStringKey(string str)
        {
            // Remove special characters except for space and /
            str = Regex.Replace(str, @"[^0-9a-zA-Z /]+", "");

            // Replace spaces from within string with dash.
            str = Regex.Replace(str.Trim(), @"\s", "-");

            // Convert all to lower case
            str = str.ToLower();

            return str;
        }
    }
}
