using System.Net;

namespace TestApi.Models
{
    public class ResultModel<T>
    {
        public object Data { get; set; }
        public bool IsSuccess { get; set; } = false;
        public List<object>? Errors { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public void SetResultProperties(bool isSuccess, object data , List<object>? errors = null)
        {
            IsSuccess = isSuccess;
            Data = data;
            Errors = errors;
            StatusCode = isSuccess ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
            Message = isSuccess ? "Operation successful." : "Operation failed.";
        }
    }
}