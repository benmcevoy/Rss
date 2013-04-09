using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace Rss.Manager
{
    public class Feed
    {
        private readonly XNamespace _atomNamespace = "http://www.w3.org/2005/Atom";
        private readonly XNamespace _purlNamespace = "http://purl.org/rss/1.0/modules/content/";

        public Feed(Uri feedUri)
        {
            var xml = GetXml(feedUri);

            FeedUri = feedUri;
            Items = new List<Item>();
            Title = GetTitle(xml);
            HtmlUri = feedUri;
        }

        public Feed(Uri feedUri, string title, Uri htmlUri)
        {
            FeedUri = feedUri;
            Items = new List<Item>();
            Title = title;
            HtmlUri = htmlUri;
        }

        public void GetItemsFromWeb()
        {
            var xml = GetXml(FeedUri);

            if (xml.Root == null) return;

            switch (xml.Root.Name.LocalName.ToLower())
            {
                case "rss":
                    Items = GetRss(xml);
                    break;
                case "feed":
                    Items = GetAtom(xml);
                    break;
            }
        }

        private string GetTitle(XDocument xml)
        {
            try
            {
                if (xml.Root != null)
                {
                    switch (xml.Root.Name.LocalName.ToLower())
                    {
                        case "rss":
                            return xml.Element("rss").Element("channel")
                                .Element("title").Value;

                        case "feed":
                            return xml.Element("feed").Element("title").Value;
                    }
                }

                return "Can't find title";
            }
            catch
            {
                return "Can't find title";
            }
        }

        private List<Item> GetRss(XDocument xml)
        {
            var xElement4 = xml.Element("rss");
            if (xElement4 != null)
            {
                var channelElement = xElement4.Element("channel");

                if (channelElement != null && channelElement.Element("link") != null)
                {
                    var xElement = channelElement.Element("link");
                    if (xElement != null) BaseUrl = xElement.Value;
                }

                // check for the existance of content:encoded element and use that if we have any
                if (channelElement != null)
                {
                    var test = channelElement.Elements("item")
                                             .Descendants(_purlNamespace + "encoded");

                    if (test.Any())
                    {
                        return (from f in channelElement.Elements("item")
                                let xElement = f.Element("link")
                                where xElement != null
                                let element = f.Element(_purlNamespace + "encoded")
                                where element != null
                                let xElement1 = f.Element("title")
                                where xElement1 != null
                                let element1 = f.Element("pubDate")
                                where element1 != null
                                select new Item(xElement.Value,
                                                element.Value,
                                                xElement1.Value,
                                                element1.Value)).ToList();
                    }
                }

                if (channelElement != null)
                    return (from f in channelElement.Elements("item")
                            let xElement2 = f.Element("link")
                            where xElement2 != null
                            let element2 = f.Element("description")
                            where element2 != null
                            let xElement3 = f.Element("title")
                            where xElement3 != null
                            let element3 = f.Element("pubDate")
                            where element3 != null
                            select new Item(xElement2.Value,
                                            element2.Value,
                                            xElement3.Value,
                                            element3.Value)).ToList();
            }
            return null;
        }

        private List<Item> GetAtom(XDocument xml)
        {
            try
            {
                var xElement = xml.Element(_atomNamespace + "feed");
                if (xElement != null)
                {
                    var element = xElement.Element(_atomNamespace + "link");
                    if (element != null)
                        BaseUrl = element.Attribute("href").Value;
                }
            }
            catch { }

            return (from f in xml.Descendants(_atomNamespace + "entry")
                    let xElement1 = f.Element(_atomNamespace + "id")
                    where xElement1 != null
                    let element1 = f.Element(_atomNamespace + "content")
                    where element1 != null
                    let xElement2 = f.Element(_atomNamespace + "title")
                    where xElement2 != null
                    let element2 = f.Element(_atomNamespace + "published")
                    where element2 != null
                    select new Item(xElement1.Value,
                                    element1.Value,
                                    xElement2.Value,
                                    element2.Value)).ToList();
        }

        private XDocument GetXml(Uri feedUri)
        {
            XDocument xDocument = null;

            // should request feed url as xml and construct list of items
            try
            {
                var request = WebRequest.Create(feedUri);

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var dataStream = response.GetResponseStream())
                    {
                        if (dataStream != null)
                        {
                            var reader = new StreamReader(dataStream);
                            // Read the content.
                            xDocument = XDocument.Parse(reader.ReadToEnd());
                        }
                    }

                    response.Close();
                }
            }
            catch (Exception)
            {
                xDocument = new XDocument();

                var e = new XElement("broken");

                xDocument.Add(e);
            }

            return xDocument;
        }

        public string Title { get; set; }

        public Uri FeedUri { get; set; }

        public List<Item> Items { get; set; }

        public Uri HtmlUri { get; set; }

        public string BaseUrl { get; set; }

        public string UpdateFrequency { get; set; }

        public string UpdatePeriod { get; set; }

        public override bool Equals(object obj)
        {
            var o = obj as Feed;

            if (o == null)
            {
                return false;
            }

            if (ReferenceEquals(this, o))
            {
                return true;
            }

            return FeedUri.Equals(o.FeedUri);
        }

        public override int GetHashCode()
        {
            return FeedUri.GetHashCode();
        }
    }
}