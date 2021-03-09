using System.Text.RegularExpressions;

namespace Radio7.HtmlCleaner
{
    public static class HtmlHelper
    {
        private static readonly Regex DodgyRegex = new Regex(@"/|\|\{|}|\[|]|\^|_|=|\t|\r|\n|~", RegexOptions.Compiled);

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        public static string StripTags(this string source)
        {
            var array = new char[source.Length];
            var arrayIndex = 0;
            var inside = false;

            foreach (var letter in source)
            {
                if (letter == '<')
                {
                    inside = true;
                    continue;
                }
                if (letter == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = letter;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        public static string TrimWithEllipsis(this string html, int length)
        {
            if (html.Length <= length)
            {
                return html;
            }

            return html.Substring(0, length - 3) + "...";
        }

        public static string RemoveWhitespace(this string html)
        {
            return Regex.Replace(html, @"\s+", " ").Trim();
        }

        public static string RemoveDodgyCharacters(this string html)
        {
            return DodgyRegex.Replace(html, " ");
        }

        public static string Decode(this string html)
        {
            return WebUtility.HtmlDecode(html);
        }
    }
}
