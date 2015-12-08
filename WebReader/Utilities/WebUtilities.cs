using HtmlAgilityPack;
using System;

namespace WebReader.Utilities
{
    internal class WebUtilities
    {
        internal static string SetAbsoluteUri(string uri, Uri baseUri)
        {
            if (uri.StartsWith("/"))
                uri = string.Format("{0}://{1}{2}", baseUri.Scheme, baseUri.Authority, uri);
            else if (!uri.ToLower().Contains("http://") && !uri.ToLower().Contains("https://"))
                uri = "http://" + uri;
            return uri;
        }

        internal static string GetBaseUri(string uri)
        {
            var uriObj = new Uri(uri);
            var baseUri = string.Format("{0}://{1}", uriObj.Scheme, uriObj.Authority);
            return baseUri;
        }

        internal static string SetAbsoluteLinks(string contentHtml, string baseUri)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(contentHtml);

            var allLinks = htmlDoc.DocumentNode.SelectNodes("//*[@href or @src or @action]");
            if (allLinks == null)
                return contentHtml;

            foreach (HtmlNode link in allLinks)
            {
                HtmlAttribute att = GetAttributeForLink(link);
                if (att != null)
                {
                    string href = att.Value;

                    // ignore javascript on buttons using a tags
                    if (href.StartsWith("javascript", StringComparison.InvariantCultureIgnoreCase))
                        continue;

                    Uri uri = new Uri(href, UriKind.RelativeOrAbsolute);
                    if (!uri.IsAbsoluteUri)
                    {
                        uri = new Uri(new Uri(baseUri), uri);
                        att.Value = uri.ToString();
                    }
                }
            }
            return htmlDoc.DocumentNode.OuterHtml;
        }

        static HtmlAttribute GetAttributeForLink(HtmlNode link)
        {
            var linkTypes = new[] { "href", "src", "action" };
            foreach (var type in linkTypes)
            {
                var attribute = link.Attributes[type];
                if (attribute != null && !string.IsNullOrEmpty(attribute.Value))
                    return attribute;
            }
            return null;
        }
    }
}