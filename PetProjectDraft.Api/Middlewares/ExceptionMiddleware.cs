using PetProjectDraft.Api.Contracts;
using PetProjectDraft.Domain.Common;
using System.Net;

namespace PetProjectDraft.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {


                _logger.LogError(ex.Message);

                var errorInfo = new ErrorInfo(Errors.General.Iternal(ex.Message));
                var envelope = Envelope.Error([errorInfo]);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(envelope);

                //_logger.LogError(ex.Message);

                //var error = new Error("server.iternal",ex.Message);
                //await context.Response.WriteAsJsonAsync(error);
                //context.Response.ContentType = "application/json";
                //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


                //var envelope = Envelope.Error([errorInfo]);

                //context.Response.ContentType = "application/json";
                //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //await context.Response.WriteAsJsonAsync(envelope);
            }
        }
    }
}
