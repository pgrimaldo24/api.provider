namespace Ripley.Api.Provider.CrossCutting.Base.Exception
{
    public class BaseResponseException
    {
        public string error_message { get; set; }
        public string? stackTracer { get; set; }
    }
}
