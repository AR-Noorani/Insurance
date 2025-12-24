namespace App.RestApi.ApiResponse
{
    /// <summary>
    /// This is a uniform http api error
    /// </summary>
    public sealed class ApiError
    {
        public ApiError()
        { }

        public ApiError(string message, string traceId)
        {
            Message = message;
            TraceId = traceId;
        }

        public string Message { get; init; } = default!;
        public string TraceId { get; init; } = default!;
    }
}
