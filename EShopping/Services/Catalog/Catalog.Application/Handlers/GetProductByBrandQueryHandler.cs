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
    public class GetProductByBrandQueryHandler : IRequestHandler<GetProductByBrandNameQuery, IList<ProductDto>>
    {
        private readonly IProductRepository repository;

        public GetProductByBrandQueryHandler(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IList<ProductDto>> Handle(GetProductByBrandNameQuery request, CancellationToken cancellationToken)
        {
            var product = await repository.GetProductByBrand(request.BrandName);
            return ProductMapper.MapperExtension.Map<IList<ProductDto>>(product);
        }

    }
}
