using DomainModels.Utilities;
using System;
using System.Net;

namespace App.RestApi.Utilities
{
    public static class StatusCodeFinder
    {
        public static HttpStatusCode FindHttpStatusCode(Exception exception)
        {
            return exception switch
            {
                MyApplicationException => FindHttpStatusCode(((MyApplicationException)exception).ExceptionStatus),
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                AccessViolationException => HttpStatusCode.Forbidden,
                InvalidOperationException when exception.Message.Contains("no elements") => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError,
            };
        }

        public static HttpStatusCode FindHttpStatusCode(ExceptionStatus status)
        {
            return status switch
            {
                ExceptionStatus.OK => HttpStatusCode.OK,

                ExceptionStatus.TooManyRequests => HttpStatusCode.TooManyRequests,

                ExceptionStatus.Unauthenticated => HttpStatusCode.Unauthorized,
                ExceptionStatus.InvalidArgument => HttpStatusCode.BadRequest,
                ExceptionStatus.InvalidOperation => HttpStatusCode.BadRequest,
                ExceptionStatus.AlreadyExists => HttpStatusCode.BadRequest,
                ExceptionStatus.OutOfRange => HttpStatusCode.BadRequest,
                ExceptionStatus.PermissionDenied => HttpStatusCode.Forbidden,
                ExceptionStatus.NotFound => HttpStatusCode.NotFound,

                ExceptionStatus.Unavailable => HttpStatusCode.InternalServerError,
                ExceptionStatus.DataLoss => HttpStatusCode.InternalServerError,
                ExceptionStatus.Cancelled => HttpStatusCode.InternalServerError,
                ExceptionStatus.Unknown => HttpStatusCode.InternalServerError,
                ExceptionStatus.ResourceExhausted => HttpStatusCode.InternalServerError,
                ExceptionStatus.FailedPrecondition => HttpStatusCode.InternalServerError,
                ExceptionStatus.Aborted => HttpStatusCode.InternalServerError,
                ExceptionStatus.Unimplemented => HttpStatusCode.InternalServerError,
                ExceptionStatus.Internal => HttpStatusCode.InternalServerError,
                ExceptionStatus.DeadlineExceeded => HttpStatusCode.GatewayTimeout,
                _ => HttpStatusCode.InternalServerError,
            };
        }
    }
}
