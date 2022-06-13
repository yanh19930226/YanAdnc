using Adnc.Ord.Application.Contracts.Dtos;
using Adnc.Shared.Application.Contracts.Attributes;
using Adnc.Shared.Application.Contracts.Dtos.Searchs;
using Adnc.Shared.Application.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Ord.Application.Contracts.Services
{
    /// <summary>
    /// 订单管理
    /// </summary>
    public interface IOrderAppService : IAppService
    {
        [OperateLog(LogName = "订单创建")]
        [UnitOfWork(SharedToCap = true)]
        Task<OrderDto> CreateAsync(OrderCreationDto input);

        [OperateLog(LogName = "调整订单状态")]
        Task MarkCreatedStatusAsync(long id, OrderMarkCreatedStatusDto input);

        [OperateLog(LogName = "订单付款")]
        [UnitOfWork(SharedToCap = true)]
        Task<OrderDto> PayAsync(long id);

        [OperateLog(LogName = "订单更新")]
        Task<OrderDto> UpdateAsync(long id, OrderUpdationDto input);

        [OperateLog(LogName = "订单取消")]
        [UnitOfWork(SharedToCap = true)]
        Task<OrderDto> CancelAsync(long id);

        [OperateLog(LogName = "订单删除")]
        Task DeleteAsync(long id);

        [OperateLog(LogName = "订单搜索")]
        Task<PageModelDto<OrderDto>> GetPagedAsync(OrderSearchPagedDto search);

        [OperateLog(LogName = "订单详情")]
        Task<OrderDto> GetAsync(long id);
    }
}
