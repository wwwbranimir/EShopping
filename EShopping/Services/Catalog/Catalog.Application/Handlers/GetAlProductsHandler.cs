using Catalog.Application.Dto;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Core.Repositories;
using Catalog.Core.Specifications;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAlProductsHandler : IRequestHandler<GetAllProductsQuery, Pagination<ProductDto>>
    {
        private readonly IProductRepository repository;


        //constructor
        public GetAlProductsHandler(IProductRepository repository)
        {
            this.repository = repository;

        }
        public async Task<Pagination<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await repository.GetProducts(request.CatalogSpecificationParams);
            return ProductMapper.MapperExtension.Map<Pagination<ProductDto>>(products);
        }
    }
}
