using Application.Features.Brands.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.Update;

public class UpdateBrandCommand :IRequest<UpdatedBrandResponse>, ICacheRemoverRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string? CacheKey { get; }
    public bool BypassCache { get; }
    public string? CacheGroupKey => CacheBrandGroupKeys.BrandGroupKey;

    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, UpdatedBrandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;

        public UpdateBrandCommandHandler(IMapper mapper, IBrandRepository brandRepository)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }

        public async Task<UpdatedBrandResponse> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand? currentBrand = await _brandRepository.GetAsync(predicate: x => x.Id == request.Id);

            var changedBrand = _mapper.Map(request, currentBrand);
            
            var response = await _brandRepository.UpdateAsync(changedBrand);
            var result = _mapper.Map<UpdatedBrandResponse>(response);
            return result;
        }
    }
}