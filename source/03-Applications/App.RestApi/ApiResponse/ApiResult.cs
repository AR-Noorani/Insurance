namespace App.RestApi.ApiResponse
{
    /// <summary>
    /// This is a wrapper on final api response to unify all responses.
    /// </summary>
    public sealed class ApiResult
    {
        private ApiResult()
        { }

        public object Result { get; private set; } = new();

        public static ApiResult From(object obj)
        {
            return new ApiResult
            {
                Result = obj
            };
        }
    }
}
