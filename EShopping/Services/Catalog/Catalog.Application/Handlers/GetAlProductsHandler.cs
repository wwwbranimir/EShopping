using Catalog.Application.Dto;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAlProductsHandler : IRequestHandler<GetAllProductsQuery, IList<ProductDto>>
    {
        private readonly IProductRepository repository;


        //constructor
        public GetAlProductsHandler(IProductRepository repository)
        {
            this.repository = repository;

        }
        public async Task<IList<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await repository.GetProducts();
            return ProductMapper.MapperExtension.Map<IList<ProductDto>>(products);
        }
    }
}
