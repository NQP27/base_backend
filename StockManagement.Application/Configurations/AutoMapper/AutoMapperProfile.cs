using AutoMapper;
using StockManagement.Application.CQRS.Query;
using StockManagement.Domain.DTOs;
using StockManagement.Domain.DTOs.Query;
using StockManagement.Domain.Entities;

namespace StockManagement.Application.Configurations.AutoMapper

{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ListCandleQuery, ListCandleQueryDTO>().ReverseMap().ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<OhlcDTO, ohlc_m1>().ReverseMap().ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<OhlcDTO, ohlc_m5>().ReverseMap().ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<OhlcDTO, ohlc_m15>().ReverseMap().ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<OhlcDTO, ohlc_m30>().ReverseMap().ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<OhlcDTO, ohlc_h1>().ReverseMap().ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<OhlcDTO, ohlc_h4>().ReverseMap().ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<OhlcDTO, ohlc_h12>().ReverseMap().ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<OhlcDTO, ohlc_day>().ReverseMap().ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<OhlcDTO, ohlc_week>().ReverseMap().ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<OhlcDTO, ohlc_month>().ReverseMap().ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}