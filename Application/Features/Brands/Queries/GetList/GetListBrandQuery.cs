﻿using Application.Features.Brands.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;


namespace Application.Features.Brands.Queries.GetList;

public class GetListBrandQuery:IRequest<GetListResponse<GetListBrandListItemDto>>, ICacheRequest,ILogRequest
{
    public PageRequest PageRequest { get; set; }

    public string CacheKey => $"GetListBrandQuery({PageRequest.PageIndex}, {PageRequest.PageSize})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => CacheBrandGroupKeys.BrandGroupKey;
    public TimeSpan? SlidingExpiration { get; }

    public class GetListBrandQueryHandler:IRequestHandler<GetListBrandQuery, GetListResponse<GetListBrandListItemDto>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public GetListBrandQueryHandler(IMapper mapper, IBrandRepository brandRepository)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }

        public async Task<GetListResponse<GetListBrandListItemDto>> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
        {
            Paginate<Brand> brands = await _brandRepository.GetListAsync(index: request.PageRequest.PageIndex, size: request.PageRequest.PageSize, cancellationToken: cancellationToken);

            GetListResponse<GetListBrandListItemDto> response = _mapper.Map<GetListResponse<GetListBrandListItemDto>>(brands);
            return response;
        }
    }


}
