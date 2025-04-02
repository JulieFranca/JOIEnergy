using System;

namespace JOIEnergy.Services.Auth
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int StatusCode { get; set; }

        public static ApiResponse SuccessResponse(object data, string message = null)
        {
            return new ApiResponse
            {
                Success = true,
                Message = message,
                Data = data,
                StatusCode = 200
            };
        }

        public static ApiResponse ErrorResponse(string message, int statusCode = 400)
        {
            return new ApiResponse
            {
                Success = false,
                Message = message,
                Data = null,
                StatusCode = statusCode
            };
        }
    }
} 