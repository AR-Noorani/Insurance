using System;

namespace DomainModels.Utilities
{
    public sealed class MyApplicationException : Exception
    {
        public const string BASE_MESSAGE =
            "یک خطای غیر منتظره رخ داد. لطفا با ادمین تماس بگیرید.";

        public MyApplicationException(string message, ExceptionStatus exceptionStatus, Exception innerException, string? logMessage = default)
            : base(GetMessage(message), innerException)
        {
            ExceptionStatus = exceptionStatus;
            LogMessage = logMessage;
            TraceId = TraceIdGenerator.Generate();
        }

        public MyApplicationException(string message, Exception innerException, string? logMessage = default)
            : base(GetMessage(message), innerException)
        {
            LogMessage = logMessage;
            TraceId = TraceIdGenerator.Generate();
        }

        public MyApplicationException(string message, ExceptionStatus exceptionStatus, string? logMessage = default)
            : base(GetMessage(message))
        {
            ExceptionStatus = exceptionStatus;
            LogMessage = logMessage;
            TraceId = TraceIdGenerator.Generate();
        }

        public MyApplicationException(string message, string? logMessage = default)
            : base(GetMessage(message))
        {
            LogMessage = logMessage;
            TraceId = TraceIdGenerator.Generate();
        }

        public MyApplicationException(ExceptionStatus exceptionStatus, string? logMessage = default)
            : base(BASE_MESSAGE)
        {
            ExceptionStatus = exceptionStatus;
            LogMessage = logMessage;
            TraceId = TraceIdGenerator.Generate();
        }

        public MyApplicationException(string? logMessage = default)
            : base(BASE_MESSAGE)
        {
            LogMessage = logMessage;
            TraceId = TraceIdGenerator.Generate();
        }

        public string TraceId { get; private set; } = default!;
        public ExceptionStatus ExceptionStatus { get; private set; } = ExceptionStatus.Internal;
        public string? LogMessage { get; private set; } = BASE_MESSAGE;

        private static string GetMessage(string message)
        {
            return string.IsNullOrWhiteSpace(message) ? BASE_MESSAGE : message;
        }
    }
}
