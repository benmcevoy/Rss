using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Rss.Manager.Export
{
    public class OpmlExporter
    {
        // TODO: should accept RootFolder
        public string Export(IEnumerable<Feed> feeds)
        {
            var x = new XDocument(new XElement("opml",
                 new XAttribute("version", "1.0"),
                    new XElement("head",
                        new XElement("title", "subscriptions")),
                            new XElement("body",
                                new XElement("outline",
                                    new XAttribute("title", "home"),
                                        new XAttribute("text", "home"),
                                           from f in feeds
                                           select new XElement("outline",
                                                               new XAttribute("text", f.Title),
                                                               new XAttribute("title", f.Title),
                                                               new XAttribute("type", "rss"),
                                                               new XAttribute("xmlUrl", f.FeedUri.ToString()),
                                                               new XAttribute("htmlUrl", f.HtmlUri.ToString())
                                               )
                                  )
                            )
                )
            );

            return x.ToString();
        }
    }
}
