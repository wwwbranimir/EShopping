using Catalog.Application.Dto;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetAllTypesQuery:IRequest<IList<TypeDto>>
    {


    }
}
