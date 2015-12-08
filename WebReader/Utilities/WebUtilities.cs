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

        internal static string GetWebReaderUri(Uri baseUri)
        {
            return string.Format("{0}://{1}{2}", baseUri.Scheme, baseUri.Authority, baseUri.AbsolutePath);
        }

        internal static string GetBaseUriForLinks(string uri, Uri baseUri, bool keepDirectLink = false)
        {
            if (ConfigUtilities.GetAppSettingsBool(Constants.IsSinglePageRendering)) //app setting to show original links
                keepDirectLink = true;
            var uriObj = new Uri(uri);
            var directUri = string.Format("{0}://{1}", uriObj.Scheme, uriObj.Authority);
            if (keepDirectLink)
                return directUri;
            return GetWebReaderUri(baseUri) + "?uri=" + directUri; //TODO: remove hard coding
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentHtml"></param>
        /// <param name="divertUri">Uri for WebReader redirection</param>
        /// <param name="directUri">Uri for direct link to original resource</param>
        /// <returns></returns>
        internal static string SetAbsoluteLinks(string contentHtml, string divertUri, string directUri)
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
                    string href = att.Value; //TODO: re-evaluate "link"
                    if (link.OriginalName.ToLower() == "img" || link.OriginalName.ToLower() == "audio"
                            || link.OriginalName.ToLower() == "video" || link.OriginalName.ToLower() == "link") //we are not good at rendering binary. yet.
                    {
                        if (!IsAbsoluteUri(href))
                        {
                            //uri = new Uri(new Uri(baseUri), uri);
                            att.Value = directUri + href; //uri.ToString(); //TODO: check fix
                        }
                        continue;
                    }
                    // ignore javascript on buttons using a tags
                    if (href.StartsWith("javascript", StringComparison.InvariantCultureIgnoreCase))
                        continue;

                    if (!IsAbsoluteUri(href))
                    {
                        //uri = new Uri(new Uri(baseUri), uri);
                        att.Value = divertUri + href; //uri.ToString(); //TODO: check fix
                    }
                }
            }
            return htmlDoc.DocumentNode.OuterHtml;
        }

        static bool IsAbsoluteUri(string link)
        {
            Uri uri = new Uri(link, UriKind.RelativeOrAbsolute);
            return link.StartsWith("//") || uri.IsAbsoluteUri;
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