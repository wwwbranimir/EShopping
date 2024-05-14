using Catalog.Application.Dto;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetProductByNameQuery: IRequest<IList<ProductDto>>
    {
        public string Name { get; set; }
        public GetProductByNameQuery(string name)
        {
            Name = name;
        }

    }
}
