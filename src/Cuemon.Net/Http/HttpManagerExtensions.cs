﻿using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Cuemon.Net.Http
{
    /// <summary>
    /// This is an extension implementation of the most common methods on the <see cref="HttpManager"/> class.
    /// </summary>
    public static class HttpManagerExtensions
    {
        /// <summary>
        /// Send a DELETE request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpDeleteAsync(this Uri location)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpDeleteAsync(location);
            }
        }

        /// <summary>
        /// Send a DELETE request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpDeleteAsync(this Uri location, TimeSpan timeout)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpDeleteAsync(location, timeout);
            }
        }

        /// <summary>
        /// Send a GET request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpGetAsync(this Uri location)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpGetAsync(location);
            }
        }

        /// <summary>
        /// Send a GET request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpGetAsync(this Uri location, TimeSpan timeout)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpGetAsync(location, timeout);
            }
        }

        /// <summary>
        /// Send a HEAD request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpHeadAsync(this Uri location)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpHeadAsync(location);
            }
        }

        /// <summary>
        /// Send a HEAD request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpHeadAsync(this Uri location, TimeSpan timeout)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpHeadAsync(location, timeout);
            }
        }

        /// <summary>
        /// Send an OPTIONS request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpOptionsAsync(this Uri location)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpOptionsAsync(location);
            }
        }

        /// <summary>
        /// Send an OPTIONS request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpOptionsAsync(this Uri location, TimeSpan timeout)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpOptionsAsync(location, timeout);
            }
        }

        /// <summary>
        /// Send a POST request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpPostAsync(this Uri location, string contentType, Stream content)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpPostAsync(location, contentType, content);
            }
        }

        /// <summary>
        /// Send a POST request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpPostAsync(this Uri location, string contentType, Stream content, TimeSpan timeout)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpPostAsync(location, contentType, content, timeout);
            }
        }

        /// <summary>
        /// Send a POST request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpPostAsync(this Uri location, MediaTypeHeaderValue contentType, Stream content)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpPostAsync(location, contentType, content);
            }
        }

        /// <summary>
        /// Send a POST request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpPostAsync(this Uri location, MediaTypeHeaderValue contentType, Stream content, TimeSpan timeout)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpPostAsync(location, contentType, content, timeout);
            }
        }

        /// <summary>
        /// Send a PUT request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpPutAsync(this Uri location, string contentType, Stream content)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpPutAsync(location, contentType, content);
            }
        }

        /// <summary>
        /// Send a PUT request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpPutAsync(this Uri location, string contentType, Stream content, TimeSpan timeout)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpPutAsync(location, contentType, content, timeout);
            }
        }

        /// <summary>
        /// Send a PUT request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpPutAsync(this Uri location, MediaTypeHeaderValue contentType, Stream content)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpPutAsync(location, contentType, content);
            }
        }

        /// <summary>
        /// Send a PUT request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpPutAsync(this Uri location, MediaTypeHeaderValue contentType, Stream content, TimeSpan timeout)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpPutAsync(location, contentType, content, timeout);
            }
        }

        /// <summary>
        /// Send a PUT request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpPatchAsync(this Uri location, string contentType, Stream content)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpPatchAsync(location, contentType, content);
            }
        }

        /// <summary>
        /// Send a PUT request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpPatchAsync(this Uri location, string contentType, Stream content, TimeSpan timeout)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpPatchAsync(location, contentType, content, timeout);
            }
        }

        /// <summary>
        /// Send a PUT request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpPatchAsync(this Uri location, MediaTypeHeaderValue contentType, Stream content)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpPatchAsync(location, contentType, content);
            }
        }

        /// <summary>
        /// Send a PUT request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpPatchAsync(this Uri location, MediaTypeHeaderValue contentType, Stream content, TimeSpan timeout)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpPatchAsync(location, contentType, content, timeout);
            }
        }

        /// <summary>
        /// Send a TRACE request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpTraceAsync(this Uri location)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpTraceAsync(location);
            }
        }

        /// <summary>
        /// Send a TRACE request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpTraceAsync(this Uri location, TimeSpan timeout)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpTraceAsync(location, timeout);
            }
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="method">The HTTP method.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpAsync(this Uri location, string method)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpAsync(method, location);
            }
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpAsync(this Uri location, string method, TimeSpan timeout)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpAsync(method, location, timeout);
            }
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpAsync(this Uri location, string method, string contentType, Stream content)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpAsync(method, location, contentType, content);
            }
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
        public static async Task<HttpResponseMessage> HttpAsync(this Uri location, string method, string contentType, Stream content, TimeSpan timeout)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpAsync(method, location, contentType, content, timeout);
            }
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpAsync(this Uri location, HttpMethod method, string contentType, Stream content)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpAsync(method, location, contentType, content);
            }
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpAsync(this Uri location, HttpMethod method, string contentType, Stream content, TimeSpan timeout)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpAsync(method, location, contentType, content, timeout);
            }
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpAsync(this Uri location, string method, MediaTypeHeaderValue contentType, Stream content)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpAsync(method, location, contentType, content);
            }
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpAsync(this Uri location, string method, MediaTypeHeaderValue contentType, Stream content, TimeSpan timeout)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpAsync(method, location, contentType, content, timeout);
            }
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpAsync(this Uri location, HttpMethod method, MediaTypeHeaderValue contentType, Stream content)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpAsync(method, location, contentType, content);
            }
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="contentType">The Content-Type header of the HTTP request sent to the server.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpAsync(this Uri location, HttpMethod method, MediaTypeHeaderValue contentType, Stream content, TimeSpan timeout)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpAsync(method, location, contentType, content, timeout);
            }
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="method">The HTTP method.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpAsync(this Uri location, HttpMethod method)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpAsync(method, location);
            }
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="location">The Uri the request is sent to.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> HttpAsync(this HttpMethod method, Uri location, TimeSpan timeout)
        {
            using (var manager = new HttpManager())
            {
                return await manager.HttpAsync(method, location, timeout);
            }
        }
    }
}