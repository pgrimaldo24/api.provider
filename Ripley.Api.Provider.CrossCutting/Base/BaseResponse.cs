using Microsoft.AspNetCore.Http;

namespace Ripley.Api.Provider.CrossCutting.Base
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = true;
            Status = StatusCodes.Status200OK;
        }

        public BaseResponse(string message, bool success)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
