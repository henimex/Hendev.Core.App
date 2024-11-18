﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Queries.GetListWithDeleted;

public class GetListBrandAllQuery : IRequest<GetListResponse<GetListBrandAllItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class
        GetListBrandAllQueryHandler : IRequestHandler<GetListBrandAllQuery, GetListResponse<GetListBrandAllItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;

        public GetListBrandAllQueryHandler(IMapper mapper, IBrandRepository brandRepository)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }

        public async Task<GetListResponse<GetListBrandAllItemDto>> Handle(GetListBrandAllQuery request,
            CancellationToken cancellationToken)
        {
            Paginate<Brand> brands = await _brandRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                withDeleted: true, 
                cancellationToken: cancellationToken);

            GetListResponse<GetListBrandAllItemDto> response =
                _mapper.Map<GetListResponse<GetListBrandAllItemDto>>(brands);

            return response;
        }
    }
}