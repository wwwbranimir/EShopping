using Catalog.Application.Dto;
using Catalog.Core.Specifications;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetAllProductsQuery:IRequest<Pagination<ProductDto>>
    {
        public CatalogSpecificationParameters CatalogSpecificationParams { get; }
        public GetAllProductsQuery(CatalogSpecificationParameters catalogSpecificationParams)
        {
            CatalogSpecificationParams = catalogSpecificationParams;
        }   
    }
}
