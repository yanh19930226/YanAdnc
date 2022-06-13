using Adnc.Infra.Repository.IRepositories.Models;
using Adnc.Ord.Application.Contracts.Dtos;
using Adnc.Ord.Domain.Aggregates.OrderAggregate;
using Adnc.Shared.Application.Contracts.Dtos.Searchs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Ord.Application.AutoMapper
{
    public class OrdProfile : Profile
    {
        public OrdProfile()
        {
            CreateMap(typeof(PagedModel<>), typeof(PageModelDto<>)).ForMember("XData", opt => opt.Ignore());

            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderDto.OrderItemDto>();
        }
    }
}
