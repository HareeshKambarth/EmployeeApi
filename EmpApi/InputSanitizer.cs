using System.Text.RegularExpressions;
namespace EmpApi
{
    public class InputSanitizer
    {
        public static string Sanitize(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Remove HTML tags
            var cleaned= Regex.Replace(input, "<.*?>", string.Empty).Trim();
            return string.IsNullOrWhiteSpace(cleaned) ? null : cleaned;
        }
    }
}
