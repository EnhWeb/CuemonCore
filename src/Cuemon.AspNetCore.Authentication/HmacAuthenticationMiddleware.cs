﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cuemon.Security.Cryptography;
using Cuemon.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Cuemon.AspNetCore.Authentication
{
    /// <summary>
    /// Provides a HTTP HMAC Authentication middleware implementation for ASP.NET Core.
    /// </summary>
    public class HmacAuthenticationMiddleware : Middleware<HmacAuthenticationOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HmacAuthenticationMiddleware"/> class.
        /// </summary>
        /// <param name="next">The delegate of the request pipeline to invoke.</param>
        /// <param name="setup">The middleware <see cref="HmacAuthenticationOptions"/> which need to be configured.</param>
        public HmacAuthenticationMiddleware(RequestDelegate next, Action<HmacAuthenticationOptions> setup)
            : base(next, setup)
        {
        }

        /// <summary>
        /// Executes the <see cref="HmacAuthenticationMiddleware" />.
        /// </summary>
        /// <param name="context">The context of the current request.</param>
        /// <returns>A task that represents the execution of this middleware.</returns>
        public override async Task Invoke(HttpContext context)
        {
            if (!AuthenticationUtility.TryAuthenticate(context, Options.RequireSecureConnection, AuthorizationHeaderParser, TryAuthenticate))
            {
                context.Response.StatusCode = AuthenticationUtility.HttpNotAuthorizedStatusCode;
                context.Response.Headers.Add(HeaderNames.WWWAuthenticate, "{0}".FormatWith(Options.AuthenticationScheme));
                await context.WriteHttpNotAuthorizedBody(Options.HttpNotAuthorizedBody).ContinueWithSuppressedContext();
                return;
            }
            await Next.Invoke(context).ContinueWithSuppressedContext();
        }

        private bool TryAuthenticate(HttpContext context, Template<string, string> credentials, out ClaimsPrincipal result)
        {
            if (Options.Authenticator == null) { throw new InvalidOperationException("The {0} cannot be null.".FormatWith(nameof(Options.Authenticator))); }
            var requestBodyMd5 = context.Request.Headers[HeaderNames.ContentMD5].FirstOrDefault()?.ComputeHash(o =>
            {
                o.AlgorithmType = HashAlgorithmType.MD5;
                o.Encoding = Encoding.UTF8;
            }).ToHexadecimal();
            if (!requestBodyMd5.IsNullOrWhiteSpace() && !context.Request.Body.ComputeHash(o => o.AlgorithmType = HashAlgorithmType.MD5).ToHexadecimal().Equals(requestBodyMd5, StringComparison.Ordinal))
            {
                result = null;
                return false;
            }
            var publicKey = credentials.Arg1;
            var signature = credentials.Arg2;
            var stringToSign = Options.MessageDescriptor(context);
            byte[] privateKey = new byte[0];
            result = Options?.Authenticator(publicKey, out privateKey);
            if (privateKey == null)
            {
                result = null;
                return false;
            }
            var computedSignature = Options?.HmacSigner(new HmacAuthenticationParameters(Options.Algorithm, privateKey, stringToSign))?.ToBase64();
            return signature.Equals(computedSignature, StringComparison.Ordinal) && Condition.IsNotNull(result);
        }

        private Template<string, string> AuthorizationHeaderParser(HttpContext context, string authorizationHeader)
        {
            if (AuthenticationUtility.IsAuthenticationSchemeValid(authorizationHeader, Options.AuthenticationScheme) && authorizationHeader.Length > Options.AuthenticationScheme.Length)
            {
                var credentials = authorizationHeader.Remove(0, Options.AuthenticationScheme.Length + 1).Split(':');
                if (credentials.Length == 2)
                {
                    var publicKey = credentials[0];
                    var signature = credentials[1];
                    if (!publicKey.IsNullOrWhiteSpace() && !signature.IsNullOrWhiteSpace()) { return TupleUtility.CreateTwo(publicKey, signature); }
                }
            }
            return null;
        }
    }

    /// <summary>
    /// This is a factory implementation of the <see cref="HmacAuthenticationMiddleware"/> class.
    /// </summary>
    public static class HmacAuthenticationBuilderExtension
    {
        /// <summary>
        /// Adds a HTTP Basic Authentication scheme to the <see cref="IApplicationBuilder"/> request execution pipeline.
        /// </summary>
        /// <param name="builder">The type that provides the mechanisms to configure an application’s request pipeline.</param>
        /// <param name="setup">The HTTP <see cref="HmacAuthenticationOptions"/> middleware which need to be configured.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IApplicationBuilder UseHmacAuthentication(this IApplicationBuilder builder, Action<HmacAuthenticationOptions> setup)
        {
            return builder.UseMiddleware<HmacAuthenticationMiddleware>(setup);
        }
    }
}