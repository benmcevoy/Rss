using System.Collections.Generic;
using System.Xml.Linq;

namespace Radio7.Rss
{
    public class Media
    {
        public Media(string mediaUrl)
        {
            MediaUrl = mediaUrl;
        }

        public string MediaUrl { get; set; }
    }

    public static class FeedExtensions
    {
        public static IEnumerable<Media> GetMedia(this IEnumerable<XElement> elements)
        {
            var media = new List<Media>();

            foreach (var element in elements)
            {
                if (element.Attribute("medium") != null)
                {
                    var xAttribute = element.Attribute("medium");
                    if (xAttribute != null && xAttribute.Value.ToLower() == "audio")
                    {
                        var attribute = element.Attribute("url");
                        if (attribute != null) media.Add(new Media(attribute.Value));
                    }
                }

                if (element.Attribute("type") != null)
                {
                    var xAttribute = element.Attribute("type");

                    if (xAttribute != null && (xAttribute.Value.ToLower() == "audio/mpeg" ||
                                                              xAttribute.Value.ToLower() == "audio/mp3"))
                    {
                        var attribute = element.Attribute("url");
                        if (attribute != null) media.Add(new Media(attribute.Value));
                    }
                }
            }

            return media;
        }
    }
}

