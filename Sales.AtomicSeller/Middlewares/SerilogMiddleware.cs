using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.AtomicSeller.Middlewares
{
    public class SerilogMiddleware
    {

        private readonly RequestDelegate _next;

        public SerilogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (context != null)
                {

                    LogContext.PushProperty("RemoteIpAddress", context.Connection?.RemoteIpAddress);
                    if (context.Request != null)
                    {
                        LogContext.PushProperty("Host", context.Request.Host);
                        LogContext.PushProperty("Path", context.Request.Path);
                        LogContext.PushProperty("IsHttps", context.Request.IsHttps);
                        LogContext.PushProperty("Scheme", context.Request.Scheme);
                        LogContext.PushProperty("Method", context.Request.Method);
                        LogContext.PushProperty("ContentType", context.Request.ContentType);
                        LogContext.PushProperty("Protocol", context.Request.Protocol);
                        LogContext.PushProperty("QueryString", context.Request.QueryString);
                        LogContext.PushProperty("Query", context.Request.Query);

                        LogContext.PushProperty("TraceIdentifier", context.TraceIdentifier);

                        if (context.User != null && context.User.Identity != null && context.User.Identity.IsAuthenticated)
                        {
                            LogContext.PushProperty("User", context.User.Identity.Name);
                            LogContext.PushProperty("UserClaims", context.User.Claims.Select(a => new KeyValuePair<string, string>(a.Type, a.Value)).ToList());
                        }
                    }
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                await _next.Invoke(context);
            }
        }
    }
}
