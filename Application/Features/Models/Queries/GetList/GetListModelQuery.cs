﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Models.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Models.Queries.GetList;

public class GetListModelQuery : IRequest<GetListResponse<GetListModelListItemDto>>, ICacheRequest
{
    public PageRequest PageRequest { get; set; }

    public string CacheKey => $"GetListModelQuery({PageRequest.PageIndex}, {PageRequest.PageSize})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => CacheModelGroupKeys.ModelGroupKey;
    public TimeSpan? SlidingExpiration { get; }

    public class GetListModelQueryHandler : IRequestHandler<GetListModelQuery, GetListResponse<GetListModelListItemDto>>
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;

        public GetListModelQueryHandler(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListModelListItemDto>> Handle(GetListModelQuery request,
            CancellationToken cancellationToken)
        {
            Paginate<Model> models = await _modelRepository.GetListAsync(
                include: x => x.Include(x => x.Brand)
                    .Include(x => x.Fuel)
                    .Include(x => x.Transmission),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken);
            var response = _mapper.Map<GetListResponse<GetListModelListItemDto>>(models);

            return response;
        }
    }
}