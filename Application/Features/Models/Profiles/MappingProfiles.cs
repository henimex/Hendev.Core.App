using Application.Features.Models.Queries.GetList;
using Application.Features.Models.Queries.GetListByDynamic;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Models.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        //Version 1 General Usage Could Be Used if There is No Problem
        CreateMap<Model, GetListModelListItemDto>().ReverseMap();
        CreateMap<Model, GetListByDynamicModelListItemDto>();

        CreateMap<Paginate<Model>, GetListResponse<GetListModelListItemDto>>().ReverseMap();
        CreateMap<Paginate<Model>, GetListResponse<GetListByDynamicModelListItemDto>>().ReverseMap();


        //Version 2 Specified Fields !! For Advanced Usage or renamed tables
        //CreateMap<Model, GetListModelListItemDto>()
        //    .ForMember(destinationMember: c => c.BrandName, memberOptions: opt => opt.MapFrom(c => c.Brand.Name))
        //    .ForMember(destinationMember: c => c.FuelName, memberOptions: opt => opt.MapFrom(c => c.Fuel.Name))
        //    .ForMember(destinationMember: c => c.TransmissionName, memberOptions: opt => opt.MapFrom(c => c.Transmission.Name))
        //    .ReverseMap();
        //CreateMap<Model, GetListByDynamicModelListItemDto>()
        //    .ForMember(destinationMember: c => c.BrandName, memberOptions: opt => opt.MapFrom(c => c.Brand.Name))
        //    .ForMember(destinationMember: c => c.FuelName, memberOptions: opt => opt.MapFrom(c => c.Fuel.Name))
        //    .ForMember(destinationMember: c => c.TransmissionName, memberOptions: opt => opt.MapFrom(c => c.Transmission.Name))
        //    .ReverseMap();

        //CreateMap<Paginate<Model>, GetListResponse<GetListModelListItemDto>>().ReverseMap();
        //CreateMap<Paginate<Model>, GetListResponse<GetListByDynamicModelListItemDto>>().ReverseMap();
    }
}