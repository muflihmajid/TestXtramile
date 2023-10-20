using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using SceletonAPI.Application.Exceptions;
using SceletonAPI.Application.Interfaces;
using SceletonAPI.Infrastructure.Persistences;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SceletonAPI.Infrastructure.ErrorHandler {
    public class ErrorHandlerMiddleware {
        private readonly RequestDelegate next;
        

        public ErrorHandlerMiddleware (RequestDelegate next) {
            this.next = next;
        }

        public async Task Invoke (HttpContext context, IDBContext dbContext /* other dependencies */ ) {
            try {
                await next (context);
            } catch (Exception ex) {
                await HandleExceptionAsync (context, ex);
                //await WriteRequestToLog (dbContext, context, ex);
            }
        }
        // private async Task WriteRequestToLog (IDBContext db, HttpContext context, Exception ex) {
        //     var authFilterCtx = context.Request;
            
        //     var request = authFilterCtx.HttpContext.Request;
        //     var LogsRequest = db.Logs;
        //     var errorMsg = ex.Message;
        //     var CreatedLogs = new Domain.Entities.Logsrequest ();
        //     var TypeOfReqest = request.Path.ToString ();

        //     if (!String.IsNullOrEmpty (errorMsg)) {
        //         CreatedLogs = new Domain.Entities.Logsrequest () {
        //             Process = TypeOfReqest,
        //             LastUpdateBy = "Jatis",
        //             LastUpdateDate = DateTime.Now,
        //             Status = "Erorr",
        //             Message = errorMsg,
        //             Json = "",
        //             CreateDate = DateTime.Now,
        //             CreateBy = "Jatis"
        //         };
        //         db.Logs.Add (CreatedLogs);
        //         await db.SaveChangesAsync1 ();
        //     }
        // }

        private static Task HandleExceptionAsync (HttpContext context, Exception ex) {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var errorMsg = ex.Message;

            if (ex is UnauthorizedAccessException) {
                code = HttpStatusCode.Unauthorized;
            }

            if (ex is ValidationException) {
                code = HttpStatusCode.BadRequest;
                errorMsg = string.Join ("\n", ex.Message);
            }

            var result = JsonConvert.SerializeObject (new {
                success = false,
                    message = errorMsg
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;

            //dbContext.SaveChangesAsync (cancellationToken);
            return context.Response.WriteAsync (result);
        }
    }
}