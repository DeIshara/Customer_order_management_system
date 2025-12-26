namespace CustomerOrderManagementSystem.API.Shared
{
    using System.Net;

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }

}
