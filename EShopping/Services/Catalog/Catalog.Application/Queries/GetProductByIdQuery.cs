using Catalog.Application.Dto;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetProductByIdQuery: IRequest<ProductDto>
    {
        public string Id { get; set; }
       public GetProductByIdQuery(string id)
        {
            Id = id;
        }

    }
}
