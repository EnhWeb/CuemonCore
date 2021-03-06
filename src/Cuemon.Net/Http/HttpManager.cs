﻿using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Cuemon.Net.Http
{
    /// <summary>
    /// Provides ways for sending HTTP requests and receiving HTTP responses from a resource identified by a URI. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public sealed class HttpManager : IDisposable
    {
        private volatile bool _isDisposed;
        private readonly Lazy<HttpClient> _httpClient;
        private const string HttpPatchVerb = "PATCH";

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpManager"/> class.
        /// </summary>
        public HttpManager(Action<HttpManagerOptions> setup = null)
        {
            var options = setup.ConfigureOptions();
            _httpClient = new Lazy<HttpClient>(() =>
            {
                var client = new HttpClient(options.Handler, options.DisposeHandler);
                foreach (var header in options.DefaultRequestHeaders)
                {
                    if (client.DefaultRequestHeaders.Contains(header.Key)) { continue; }
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
                return client;
            });
        }

        private HttpClient Client => _httpClient.Value;

        /// <summary>
        /// Gets the headers which should be sent with each request.
        /// </summary>
        /// <value>The headers which should be sent with each request.</value>
        public HttpRequestHeaders DefaultRequestHeaders => Client.DefaultRequestHeaders;

        /// <summary>
        /// Gets or sets the timespan to wait before the request times out.
        /// </summary>
        /// <value>The timespan to wait before the request times out.</value>
        public TimeSpan Timeout
        {
            get
            {
                return Client.Timeout;
            }
            set
            {
                ValidateTimeout(value, nameof(value));
                Client.Timeout = value;
            }
        }

        /// <summary>
        /// Send a DELETE request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpDeleteAsync(Uri location)
        {
            return HttpDeleteAsync(location, Client.Timeout);
        }

        /// <summary>
        /// Send a DELETE request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpDeleteAsync(Uri location, TimeSpan timeout)
        {
            return HttpAsync(HttpMethod.Delete, location, timeout);
        }

        /// <summary>
        /// Send a GET request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpGetAsync(Uri location)
        {
            return HttpGetAsync(location, Client.Timeout);
        }

        /// <summary>
        /// Send a GET request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpGetAsync(Uri location, TimeSpan timeout)
        {
            return HttpAsync(HttpMethod.Get, location, timeout);
        }

        /// <summary>
        /// Send a HEAD request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpHeadAsync(Uri location)
        {
            return HttpHeadAsync(location, Client.Timeout);
        }

        /// <summary>
        /// Send a HEAD request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpHeadAsync(Uri location, TimeSpan timeout)
        {
            return HttpAsync(HttpMethod.Head, location, timeout);
        }

        /// <summary>
        /// Send an OPTIONS request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpOptionsAsync(Uri location)
        {
            return HttpOptionsAsync(location, Client.Timeout);
        }

        /// <summary>
        /// Send an OPTIONS request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpOptionsAsync(Uri location, TimeSpan timeout)
        {
            return HttpAsync(HttpMethod.Options, location, timeout);
        }

        /// <summary>
        /// Send a POST request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpPostAsync(Uri location, string contentType, Stream content)
        {
            return HttpPostAsync(location, contentType, content, Client.Timeout);
        }

        /// <summary>
        /// Send a POST request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpPostAsync(Uri location, string contentType, Stream content, TimeSpan timeout)
        {
            return HttpAsync(HttpMethod.Post, location, contentType, content, timeout);
        }

        /// <summary>
        /// Send a POST request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpPostAsync(Uri location, MediaTypeHeaderValue contentType, Stream content)
        {
            return HttpPostAsync(location, contentType, content, Client.Timeout);
        }

        /// <summary>
        /// Send a POST request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpPostAsync(Uri location, MediaTypeHeaderValue contentType, Stream content, TimeSpan timeout)
        {
            return HttpAsync(HttpMethod.Post, location, contentType, content, timeout);
        }

        /// <summary>
        /// Send a PUT request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpPutAsync(Uri location, string contentType, Stream content)
        {
            return HttpPutAsync(location, contentType, content, Client.Timeout);
        }

        /// <summary>
        /// Send a PUT request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpPutAsync(Uri location, string contentType, Stream content, TimeSpan timeout)
        {
            return HttpAsync(HttpMethod.Put, location, contentType, content, timeout);
        }

        /// <summary>
        /// Send a PUT request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpPutAsync(Uri location, MediaTypeHeaderValue contentType, Stream content)
        {
            return HttpPutAsync(location, contentType, content, Client.Timeout);
        }

        /// <summary>
        /// Send a PUT request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpPutAsync(Uri location, MediaTypeHeaderValue contentType, Stream content, TimeSpan timeout)
        {
            return HttpAsync(HttpMethod.Put, location, contentType, content, timeout);
        }

        /// <summary>
        /// Send a PUT request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpPatchAsync(Uri location, string contentType, Stream content)
        {
            return HttpPatchAsync(location, contentType, content, Client.Timeout);
        }

        /// <summary>
        /// Send a PUT request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpPatchAsync(Uri location, string contentType, Stream content, TimeSpan timeout)
        {
            return HttpAsync(new HttpMethod(HttpPatchVerb), location, contentType, content, timeout);
        }

        /// <summary>
        /// Send a PUT request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpPatchAsync(Uri location, MediaTypeHeaderValue contentType, Stream content)
        {
            return HttpPatchAsync(location, contentType, content, Client.Timeout);
        }

        /// <summary>
        /// Send a PUT request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpPatchAsync(Uri location, MediaTypeHeaderValue contentType, Stream content, TimeSpan timeout)
        {
            return HttpAsync(new HttpMethod(HttpPatchVerb), location, contentType, content, timeout);
        }

        /// <summary>
        /// Send a TRACE request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpTraceAsync(Uri location)
        {
            return HttpTraceAsync(location, Client.Timeout);
        }

        /// <summary>
        /// Send a TRACE request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpTraceAsync(Uri location, TimeSpan timeout)
        {
            return HttpAsync(HttpMethod.Trace, location, timeout);
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpAsync(string method, Uri location)
        {
            return HttpAsync(method, location, Client.Timeout);
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpAsync(string method, Uri location, TimeSpan timeout)
        {
            Validator.ThrowIfNullOrEmpty(method, nameof(method));
            return HttpAsync(new HttpMethod(method), location, timeout);
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpAsync(string method, Uri location, string contentType, Stream content)
        {
            return HttpAsync(method, location, contentType, content, Client.Timeout);
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpAsync(string method, Uri location, string contentType, Stream content, TimeSpan timeout)
        {
            Validator.ThrowIfNullOrEmpty(method, nameof(method));
            return HttpAsync(new HttpMethod(method), location, contentType, content, timeout);
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpAsync(HttpMethod method, Uri location, string contentType, Stream content)
        {
            return HttpAsync(method, location, contentType, content, Client.Timeout);
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpAsync(HttpMethod method, Uri location, string contentType, Stream content, TimeSpan timeout)
        {
            Validator.ThrowIfNullOrEmpty(contentType, nameof(contentType));
            return HttpAsync(method, location, MediaTypeHeaderValue.Parse(contentType), content, Client.Timeout);
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpAsync(string method, Uri location, MediaTypeHeaderValue contentType, Stream content)
        {
            return HttpAsync(method, location, contentType, content, Client.Timeout);
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpAsync(string method, Uri location, MediaTypeHeaderValue contentType, Stream content, TimeSpan timeout)
        {
            Validator.ThrowIfNullOrEmpty(method, nameof(method));
            return HttpAsync(new HttpMethod(method), location, contentType, content, timeout);
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpAsync(HttpMethod method, Uri location, MediaTypeHeaderValue contentType, Stream content)
        {
            return HttpAsync(method, location, contentType, content, Client.Timeout);
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpAsync(HttpMethod method, Uri location, MediaTypeHeaderValue contentType, Stream content, TimeSpan timeout)
        {
            Validator.ThrowIfNull(method, nameof(method));
            Validator.ThrowIfNull(location, nameof(location));
            Validator.ThrowIfNull(contentType, nameof(contentType));
            Validator.ThrowIfNull(content, nameof(content));
            ValidateTimeout(timeout, nameof(timeout));
            HttpRequestMessage message = new HttpRequestMessage(method, location)
            {
                Content = new StreamContent(content)
            };
            message.Content.Headers.ContentType = contentType;
            return HttpAsync(message, timeout);
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpAsync(HttpMethod method, Uri location)
        {
            return HttpAsync(method, location, Client.Timeout);
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpAsync(HttpMethod method, Uri location, TimeSpan timeout)
        {
            Validator.ThrowIfNull(method, nameof(method));
            Validator.ThrowIfNull(location, nameof(location));
            HttpRequestMessage message = new HttpRequestMessage(method, location);
            return HttpAsync(message, timeout);
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="message">The HTTP request message to send.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpAsync(HttpRequestMessage message)
        {
            return HttpAsync(message, Client.Timeout);
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="message">The HTTP request message to send.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpAsync(HttpRequestMessage message, TimeSpan timeout)
        {
            ValidateTimeout(timeout, nameof(timeout));
            return HttpAsync(message, new CancellationTokenSource(timeout));
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="message">The HTTP request message to send.</param>
        /// <param name="cts">The cancellation token to cancel operation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> HttpAsync(HttpRequestMessage message, CancellationTokenSource cts)
        {
            Validator.ThrowIfNull(message, nameof(message));
            Validator.ThrowIfNull(cts, nameof(cts));
            return Client.SendAsync(message, ParseCompletionOption(message.Method), cts.Token);
        }

        private HttpCompletionOption ParseCompletionOption(HttpMethod method)
        {
            return (method == HttpMethod.Head || method == HttpMethod.Trace)
                ? HttpCompletionOption.ResponseHeadersRead
                : HttpCompletionOption.ResponseContentRead;
        }

        private static void ValidateTimeout(TimeSpan timeout, string paramName)
        {
            Validator.ThrowIfNull(timeout, paramName);
            Validator.ThrowIfLowerThanOrEqual(timeout.TotalMilliseconds, -1, paramName);
            Validator.ThrowIfGreaterThan(timeout.TotalMilliseconds, int.MaxValue, paramName);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (_isDisposed || !disposing) { return; }
            _isDisposed = true;
            Client?.Dispose();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
    }
}