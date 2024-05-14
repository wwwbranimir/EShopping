using Catalog.Application.Dto;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetAllProductsQuery:IRequest<IList<ProductDto>>
    {

    }
}
