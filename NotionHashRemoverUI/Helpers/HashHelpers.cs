using System.Text.RegularExpressions;

namespace NotionHashRemoverUI.Helpers
{
    internal static class HashHelpers
    {
        /// <summary>
        /// Finds MD5-Hash in input string and removes it
        /// </summary>
        /// <param name="input">content of string where MD5 should be removed</param>
        /// <returns>new string without MD5</returns>
        internal static string RemoveMD5InFiles(string input)
        {
            foreach (var item in Regex.Matches(input, "[0-9a-fA-F]{32}", RegexOptions.Compiled).ToList())
            {
                if (item.Success)
                {
                    input = input.Replace(item.Value, "").Trim();
                }
            }

            return input;
        }

        /// <summary>
        /// Finds MD5-Hash in input string and removes it
        /// </summary>
        /// <param name="input">Directory Paths as string</param>
        /// <returns>new string without MD5</returns>
        internal static string RemoveMD5InDirectories(string input)
        {
            //Directories need another Regexpression. Directory paths would contain a Whitespace otherwise..
            foreach (var item in Regex.Matches(input, "\\s[0-9a-fA-F]{32}", RegexOptions.Compiled).ToList())
            {
                if (item.Success)
                {
                    input = input.Replace(item.Value, "").Trim();
                }
            }

            return input;
        }
    }
}
