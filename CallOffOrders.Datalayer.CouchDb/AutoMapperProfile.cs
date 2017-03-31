﻿using AutoMapper;
using Cmas.BusinessLayers.CallOffOrders.Entities;
using Cmas.DataLayers.CouchDb.CallOffOrders.Dtos;

namespace Cmas.DataLayers.CouchDb.CallOffOrders
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CallOffOrder, CallOffOrderDto>();
            CreateMap<Rate, RateDto>();
            CreateMap<CallOffOrderDto, CallOffOrder>();
            CreateMap<RateDto, Rate>();
        }
    }
}