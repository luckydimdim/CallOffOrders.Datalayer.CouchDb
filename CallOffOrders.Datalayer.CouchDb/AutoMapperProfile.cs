using AutoMapper;
using Cmas.BusinessLayers.CallOffOrders.Entities;
using Cmas.DataLayers.CouchDb.CallOffOrders.Dtos;
using System;

namespace Cmas.DataLayers.CouchDb.CallOffOrders
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CallOffOrder, CallOffOrderDto>();
            CreateMap<Rate, RateDto>();
            CreateMap<CallOffOrderDto, CallOffOrder>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src._id))
                .ForMember(
                    dest => dest.Rev,
                    opt => opt.MapFrom(src => src._rev));

            CreateMap<RateDto, Rate>();

            CreateMap<RateUnit, int>().ConvertUsing(src => (int)src);
            CreateMap<int, RateUnit>()
                .ConvertUsing(src => (RateUnit)Enum.Parse(typeof(RateUnit), src.ToString()));
        }
    }
}
