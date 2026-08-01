namespace Game106_API.Models
{
    public class ResponseApi
    {
        public bool isSuccess {get; set;} = true;
        public string? Notification {get; set;}
        public object? Data {get; set;}
    }
}