namespace CMS.Api.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        public ApiResponse()
        {
            Success = true;
        }

        public ApiResponse(T data, string? message = null)
        {
            Success = true;
            Data = data;
            Message = message;
        }

        public ApiResponse(string message, List<string>? errors = null)
        {
            Success = false;
            Message = message;
            Errors = errors;
        }

        public static ApiResponse<T> SuccessResult(T data, string message = "Success")
        {
            return new ApiResponse<T>(data, message);
        }

        public static ApiResponse<T> FailureResult(string message, List<string>? errors = null)
        {
            return new ApiResponse<T>(message, errors);
        }
    }
}
