﻿using MCA.web.Models;

namespace MCA.web.Services.IServices
{
    public interface IBaseService: IDisposable
    {
        ResponseDto ResponseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
