using static MSA.web.SD;

namespace MSA.web.Models
{
    public class ApiRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; } 
        public string AccessToken { get; set; }
    }
}
