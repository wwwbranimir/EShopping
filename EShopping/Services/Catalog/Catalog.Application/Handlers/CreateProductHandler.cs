using Catalog.Application.Commands;
using Catalog.Application.Dto;
using Catalog.Application.Mappers;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductRepository repository;

        public CreateProductHandler(IProductRepository repository)
        {
            this.repository = repository;
        }
        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = ProductMapper.MapperExtension.Map<Product>(request);
            if (productEntity is null)
            {
                throw new ApplicationException("There was an error mapping the product");
            }
            var newProduct = await repository.CreateProduct(productEntity);
            var productDto = ProductMapper.MapperExtension.Map<ProductDto>(newProduct);
            return productDto;

        }
    }
}
