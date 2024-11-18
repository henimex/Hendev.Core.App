﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Brands.Commands.Delete;

public class DeleteBrandCommand : IRequest<DeletedBrandResponse>
{
    public Guid Id { get; set; }

    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, DeletedBrandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;


        public DeleteBrandCommandHandler(IMapper mapper, IBrandRepository brandRepository)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }

        public async Task<DeletedBrandResponse> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var receivedData = await _brandRepository.GetAsync(predicate: x => x.Id == request.Id,
                cancellationToken: cancellationToken);

            var deletedEntity = await _brandRepository.DeleteAsync(receivedData);

            return _mapper.Map<DeletedBrandResponse>(deletedEntity);
        }
    }
}