using Catalog.Application.Dto;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetAllBrandsQuery:IRequest<IList<BrandDto>>
    {

    }
}
