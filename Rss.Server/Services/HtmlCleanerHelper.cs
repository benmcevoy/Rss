using HtmlAgilityPack;
using System.IO;

namespace Rss.Server.Services
{
    internal static class HtmlCleanerHelper
    {
        internal static string Clean(string raw)
        {
            var htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(raw);

            using (var fixedHtmlStream = new MemoryStream())
            {
                htmlDoc.Save(fixedHtmlStream);
                return ConvertToString(fixedHtmlStream);
            }
        }

        private static string ConvertToString(Stream stream)
        {
            string contents;

            using (var reader = new StreamReader(stream))
            {
                stream.Position = 0;
                contents = reader.ReadToEnd();
            }

            return contents;
        }

        internal static string GetSnippet(string html, int length)
        {
            html = StripTagsCharArray(html);

            if (html.Length <= length)
            {
                return html;
            }

            return html.Substring(0, length - 3) + "...";
        }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        internal static string StripTagsCharArray(string source)
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
    }
}
