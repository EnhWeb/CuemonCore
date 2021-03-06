﻿using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Cuemon.Text;
using Cuemon.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Cuemon.AspNetCore.Authentication
{
    /// <summary>
    /// Provides a HTTP Basic Authentication middleware implementation for ASP.NET Core.
    /// </summary>
    public class BasicAuthenticationMiddleware : Middleware<BasicAuthenticationOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicAuthenticationMiddleware"/> class.
        /// </summary>
        /// <param name="next">The delegate of the request pipeline to invoke.</param>
        /// <param name="setup">The middleware <see cref="BasicAuthenticationOptions"/> which need to be configured.</param>
        public BasicAuthenticationMiddleware(RequestDelegate next, Action<BasicAuthenticationOptions> setup)
            : base(next, setup)
        {
        }

        /// <summary>
        /// Executes the <see cref="BasicAuthenticationMiddleware"/>.
        /// </summary>
        /// <param name="context">The context of the current request.</param>
        /// <returns>A task that represents the execution of this middleware.</returns>
        public override async Task Invoke(HttpContext context)
        {
            if (!AuthenticationUtility.TryAuthenticate(context, Options.RequireSecureConnection, AuthorizationHeaderParser, TryAuthenticate))
            {
                context.Response.StatusCode = AuthenticationUtility.HttpNotAuthorizedStatusCode;
                context.Response.Headers.Add(HeaderNames.WWWAuthenticate, "{0} realm=\"{1}\"".FormatWith(AuthenticationScheme, Options.Realm));
                await context.WriteHttpNotAuthorizedBody(Options.HttpNotAuthorizedBody).ContinueWithSuppressedContext();
                return;
            }
            await Next.Invoke(context).ContinueWithSuppressedContext();
        }

        /// <summary>
        /// Gets the name of the authentication scheme.
        /// </summary>
        /// <value>The name of the authentication scheme.</value>
        public string AuthenticationScheme => "Basic";

        private bool TryAuthenticate(HttpContext context, Template<string, string> credentials, out ClaimsPrincipal result)
        {
            if (Options.Authenticator == null) { throw new InvalidOperationException("The {0} cannot be null.".FormatWith(nameof(Options.Authenticator))); }
            result = Options.Authenticator(credentials.Arg1, credentials.Arg2);
            return Condition.IsNotNull(result);
        }

        private Template<string, string> AuthorizationHeaderParser(HttpContext context, string authorizationHeader)
        {
            if (AuthenticationUtility.IsAuthenticationSchemeValid(authorizationHeader, AuthenticationScheme))
            {
                string base64Credentials = authorizationHeader.Remove(0, AuthenticationScheme.Length + 1);
                if (StringUtility.IsBase64(base64Credentials))
                {
                    string[] credentials = StringConverter.FromBytes(Convert.FromBase64String(base64Credentials), options =>
                    {
                        options.Encoding = EncodingUtility.AsciiEncoding;
                        options.Preamble = PreambleSequence.Remove;
                    }).Split(AuthenticationUtility.BasicAuthenticationCredentialSeparator);
                    if (credentials.Length == 2 &&
                        !string.IsNullOrEmpty(credentials[0]) &&
                        !string.IsNullOrEmpty(credentials[1]))
                    { return TupleUtility.CreateTwo(credentials[0], credentials[1]); }
                }
            }
            return null;
        }
    }

    /// <summary>
    /// This is a factory implementation of the <see cref="BasicAuthenticationMiddleware"/> class.
    /// </summary>
    public static class BasicAuthenticationBuilderExtension
    {
        /// <summary>
        /// Adds a HTTP Basic Authentication scheme to the <see cref="IApplicationBuilder"/> request execution pipeline.
        /// </summary>
        /// <param name="builder">The type that provides the mechanisms to configure an application’s request pipeline.</param>
        /// <param name="setup">The HTTP <see cref="BasicAuthenticationOptions"/> middleware which need to be configured.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IApplicationBuilder UseBasicAuthentication(this IApplicationBuilder builder, Action<BasicAuthenticationOptions> setup)
        {
            return builder.UseMiddleware<BasicAuthenticationMiddleware>(setup);
        }
    }
}