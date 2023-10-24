using System.Net;

namespace Magic_Villa_WebAPI.Domain.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }  

        public bool IsSuccess { get; set; } 

        public List<string> ErrorMessages { get; set; } 

        public object APIContent { get; set; }  
    }
}
