using Catalog.Application.Dto;
using Catalog.Core.Entities;
using MediatR;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Catalog.Application.Commands
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
        
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public Brands Brands { get; set; }
        public Types Types { get; set; }

       
        public decimal Price { get; set; }
    }
}
