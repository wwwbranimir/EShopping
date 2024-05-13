using AutoMapper;
using Catalog.Application.Dto;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IList<BrandDto>>
    {
        private readonly IBrandRepository repository;
  

        //constructor
        public GetAllBrandsHandler(IBrandRepository repository)
        {
            this.repository = repository;
          
        }
        public async  Task<IList<BrandDto>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await repository.GetAllBrands();
           return  ProductMapper.MapperExtension.Map<IList<BrandDto>>(brands);
        }
    }
}
