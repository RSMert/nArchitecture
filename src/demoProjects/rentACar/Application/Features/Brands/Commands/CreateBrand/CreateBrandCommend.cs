using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommend : IRequest<CreatedBrandDto>
    {
        public string Name { get; set; }

        public class CreateBrandCommendHandler : IRequestHandler<CreateBrandCommend, CreatedBrandDto>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly BrandBusinessRules _brandBusinessRules;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public CreateBrandCommendHandler(IBrandRepository brandRepository, IMediator mediator, IMapper mapper, BrandBusinessRules brandBusinessRules)
            {
                _brandRepository = brandRepository;
                _mediator = mediator;
                _mapper = mapper;
                _brandBusinessRules = brandBusinessRules;
            }

            public async Task<CreatedBrandDto> Handle(CreateBrandCommend request, CancellationToken cancellationToken)
            {
                await _brandBusinessRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);
                
                Brand mappedBrand = _mapper.Map<Brand>(request);
                Brand createdBrand = await _brandRepository.AddAsync(mappedBrand);
                CreatedBrandDto createdSomeFeatureEntityDto = _mapper.Map<CreatedBrandDto>(createdBrand);
                
                return createdSomeFeatureEntityDto;
            }
        }
    }
}
