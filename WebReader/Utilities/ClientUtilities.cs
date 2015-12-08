using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;

namespace WebReader.Utilities
{
    public class ClientUtilities
    {
        internal static ContentResult GetWebResponse(string uri, string accept, Uri baseUri)
        {
            var message = "Failed to load data from server. ";
            var contentType = "text/html";

            if (!string.IsNullOrEmpty(uri))
            {
                uri = WebUtilities.SetAbsoluteUri(uri, baseUri);
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        if (!string.IsNullOrEmpty(accept) && accept.Contains("/"))
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(accept));
                        message = GetWebResponse(uri, ref contentType, client, baseUri);
                    }
                }
                catch (WebException wex)
                {
                    if (wex.Response != null)
                    {
                        using (var stream = wex.Response.GetResponseStream())
                        using (var reader = new StreamReader(stream))
                        {
                            var msg = System.Text.RegularExpressions.Regex.Unescape(reader.ReadToEnd());
                            message = string.Format("Server encountered probelm! Server response :  {0}", msg);
                        }
                    }
                    else
                        message += string.Format("Server encountered probelm! Server response :  {0}", wex.Message);
                }
                catch (Exception ex)
                {
                    message += string.Format("Server encountered probelm! Server response :  {0}", ex.Message);
                }
            }
            else
                message = "Invalid url";

            return new ContentResult
            {
                Content = message,
                ContentType = contentType
            };
        }

        internal static string GetWebResponse(string uri, ref string contentType, HttpClient client, Uri baseUri)
        {
            var responseContent = string.Empty;
            var response = client.GetAsync(uri).Result;
            if (response != null)
            {
                contentType = response.Content.Headers.ContentType.MediaType;
                if (!response.IsSuccessStatusCode)
                    responseContent = string.Format("Server responded with [{0}] : {1}", (int)response.StatusCode, response.ReasonPhrase);
                else
                {
                    responseContent = response.Content.ReadAsStringAsync().Result;
                    if (contentType == "text/html")
                    {
                        var webReaderUri = new Uri(WebUtilities.GetWebReaderUri(baseUri));
                        responseContent = WebUtilities.SetAbsoluteLinks(responseContent, 
                                                                        WebUtilities.GetBaseUriForLinks(uri, webReaderUri),
                                                                        WebUtilities.GetBaseUriForLinks(uri, webReaderUri, true));
                    }
                }
            }
            else
                responseContent = "Server response was null!";
            return responseContent;
        }
    }
}