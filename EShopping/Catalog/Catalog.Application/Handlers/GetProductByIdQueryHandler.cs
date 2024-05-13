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
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository repository;

        public GetProductByIdQueryHandler(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await repository.GetProductById(request.Id);
            return ProductMapper.MapperExtension.Map<ProductDto>(product);
        }

    }
}
