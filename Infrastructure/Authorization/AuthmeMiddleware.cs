using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.EntityFrameworkCore;
using SceletonAPI.Application.Interfaces.Authorization;
using SceletonAPI.Application.Interfaces;
using System.Threading;


namespace SceletonAPI.Infrastructure.Authorization
{
    public class AuthmeMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthmeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context, IDBContext dbContext)
        {
            var authFilterCtx = context.Request;
            var request = authFilterCtx.HttpContext.Request;

            var TypeOfReqest = request.Path.ToString();



            string authHeader = "";
            if (context.Request.Headers.TryGetValue("Authorization", out StringValues authToken))
            {
                authHeader = authToken.SingleOrDefault();
            }
            else
            {

                throw new UnauthorizedAccessException();

            }

            if (String.IsNullOrEmpty(authHeader)) throw new UnauthorizedAccessException();

            var token = authHeader.Replace("Bearer", "").Trim();
            var auth = token.Equals("OjSdRPAFbymkoZatRdSWPLk7m9HemAomGtj+COouHgY=");

            if (!auth) throw new UnauthorizedAccessException();
            return _next.Invoke(context);
        }
    }
}
