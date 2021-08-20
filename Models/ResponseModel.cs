namespace moo_telegram.Models
{
    public class ResponseModel
    {
        public ResponseModel(object data = null, bool isSuccess = true, string message = null)
        {
            Data = data;
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
