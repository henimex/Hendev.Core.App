using Application.Features.Brands.Constants;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.Create;

public class CreateBrandCommand:IRequest<CreatedBrandResponse>, ICacheRemoverRequest, ILogRequest
{
    public string Name { get; set; }

    public string? CacheKey { get; }
    public bool BypassCache { get; }
    public string? CacheGroupKey => CacheBrandGroupKeys.BrandGroupKey;

    public class CreateBrandCommandHandler:IRequestHandler<CreateBrandCommand, CreatedBrandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;
        private readonly BrandBusinessRules _brandBusinessRules;

        public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _brandBusinessRules = brandBusinessRules;
        }

        public async Task<CreatedBrandResponse>? Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            await _brandBusinessRules.BrandNameCannotBeDuplicatedWhenInserted(request.Name);
            Brand brand = _mapper.Map<Brand>(request);
            brand.Id = Guid.NewGuid();

            var result = await _brandRepository.AddAsync(brand);

            return _mapper.Map<CreatedBrandResponse>(result);
        }
    }


}

