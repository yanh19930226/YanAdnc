﻿using Adnc.Shared.Rpc.Rest.Rtos;
using Refit;
using System.Threading.Tasks;

namespace Adnc.Shared.Rpc.Rest.Services;

public interface IMaintRestClient : IRestClient
{
    /// <summary>
    /// 获取字典数据
    /// </summary>
    /// <param name="jwtToken">token</param>
    /// <param name="id">id</param>
    /// <returns></returns>
    [Get("/maint/dicts/{id}")]
    [Headers("Authorization: Basic", "Cache: 2000")]
    Task<ApiResponse<DictRto>> GetDictAsync(long id);
}