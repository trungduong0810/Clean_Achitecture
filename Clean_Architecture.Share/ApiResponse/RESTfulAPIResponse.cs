namespace Clean_Architecture.Share.ApiResponse
{
    public class RESTfulAPIResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
        public object? Errors { get; set; }

        private RESTfulAPIResponse() { }

        public static RESTfulAPIResponse<T> SuccessResponse(T data, string? message = null)
        {
            return new RESTfulAPIResponse<T>
            {
                Success = true,
                Data = data,
                Message = message,
                Errors = null
            };
        }

        public static RESTfulAPIResponse<T> FailResponse(string? message = null, object? errors = null)
        {
            return new RESTfulAPIResponse<T>
            {
                Success = false,
                Data = default,
                Message = message,
                Errors = errors
            };
        }
    }
}
