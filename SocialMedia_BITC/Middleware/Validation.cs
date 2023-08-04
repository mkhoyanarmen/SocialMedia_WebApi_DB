using System.Text.RegularExpressions;

namespace SocialMedia_BITC.Middleware
{
    public class Validation
    {
        public static bool IsUsername(string username)
        {
            string pattern;
            // start with a letter, allow letter or number, length between 6 to 12.
            pattern = "^[a-zA-Z][a-zA-Z0-9]{5,11}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(username); 
        }
        public static bool IsPassword(string password)
        {
            string pattern;
            // start with a letter, allow letter or number, length between 6 to 12.
            pattern = "^[0-9][0-9]{5,24}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(password);
        }
    }
}
