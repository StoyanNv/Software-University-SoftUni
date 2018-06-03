namespace WebServer.Server.HTTP
{
    using Common;
    using Contracts;
    using Enums;
    using Exceptions;
    using System;
    using System.Net;
    using System.Linq;
    using System.Collections.Generic;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestText)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestText, nameof(requestText));
            this.FormData = new Dictionary<string, string>();
            this.QueryParameters = new Dictionary<string, string>();
            this.UrlParameters = new Dictionary<string, string>();
            this.Headers = new HttpHeaderCollection();
            this.ParseRequest(requestText);
        }
        public IDictionary<string, string> FormData { get; private set; }
        public HttpHeaderCollection Headers { get; private set; }
        public string Path { get; private set; }
        public IDictionary<string, string> QueryParameters { get; private set; }
        public HttpRequestMethod Method { get; private set; }
        public string Url { get; private set; }
        public IDictionary<string, string> UrlParameters { get; private set; }
        public void AddUrlParameter(string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            this.UrlParameters[key] = value;
        }
        private void ParseRequest(string requestText)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestText, nameof(requestText));

            var requestLines = requestText.Split(Environment.NewLine);
            if (!requestLines.Any())
            {
                BadRequestExcepticon.ThrowFromInvalidRequest();
            }
            var requestLine = requestLines.First().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (requestLine.Length != 3 || requestLine[2].ToLower() != "http/1.1")
            {
                BadRequestExcepticon.ThrowFromInvalidRequest();
            }

            this.Method = this.ParseMethod(requestLine.First());
            this.Url = requestLine[1];
            this.Path = this.ParsePath(this.Url);

            this.ParseHeaders(requestLines);
            this.ParseParameters();
            this.ParseFormData(requestLines.Last());
        }


        private HttpRequestMethod ParseMethod(string method)
        {
            HttpRequestMethod parsedMethod;
            if (!Enum.TryParse(method, true, out parsedMethod))
            {
                BadRequestExcepticon.ThrowFromInvalidRequest();
            }

            return parsedMethod;
        }
        private string ParsePath(string url) => "/" /*url.Split(new[] { '#', '?' }, StringSplitOptions.RemoveEmptyEntries)[0]*/;
        private void ParseHeaders(string[] requestLines)
        {
            var emptyLineAfterHeadersIndex = Array.IndexOf(requestLines, String.Empty);

            for (int i = 1; i < emptyLineAfterHeadersIndex; i++)
            {
                var headerParts = requestLines[i].Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
                if (headerParts.Length != 2)
                {
                    BadRequestExcepticon.ThrowFromInvalidRequest();
                }
                var headerKey = headerParts[0];
                var headerValue = headerParts[1].Trim();

                var header = new HttpHeader(headerKey, headerValue);
                this.Headers.Add(header);
            }
            if (!this.Headers.ContainsKey("Host"))
            {
                BadRequestExcepticon.ThrowFromInvalidRequest();
            }
        }
        private void ParseParameters()
        {
            if (!this.Url.Contains('?'))
            {
                return;
            }
            var query = this.Url
                .Split(new[] { '?' }, StringSplitOptions.RemoveEmptyEntries)
                .Last();
            this.ParseQuery(query, this.UrlParameters);
        }
        private void ParseFormData(string formDataLine)
        {
            if (this.Method == HttpRequestMethod.Get)
            {
                return;
            }

            this.ParseQuery(formDataLine, this.QueryParameters);
        }

        private void ParseQuery(string query, IDictionary<string, string> dic)
        {

            if (!query.Contains('='))
            {
                return;
            }

            var queryPairs = query.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var queryPair in queryPairs)
            {
                var queryKvp = queryPair.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                if (queryKvp.Length != 2)
                {
                    return;
                }

                var queryKey = WebUtility.UrlDecode(queryKvp[0]);
                var queryValue = WebUtility.UrlDecode(queryKvp[1]);

                dic.Add(queryKey, queryValue);
            }
        }
    }
}