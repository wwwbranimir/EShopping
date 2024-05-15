using Catalog.Application.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository repository;

        public UpdateProductHandler(IProductRepository repository)
        {
            this.repository = repository;
        }
        public async  Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var res = await repository.UpdateProduct(new Product { Id = request.Id, 
                Name = request.Name, Description = request.Description,
                Price = request.Price,
                ImageFile = request.ImageFile,
                Brands = request.Brands, 
                Types = request.Types
            });

            return res;
        }
    }
}
