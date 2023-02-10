using MSA.web.Models;

namespace MSA.web.Services.IServices
{
    public interface IBaseService: IDisposable
    {
        ResponseDto ResponseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
