using Catalog.Application.Commands;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class DeleteProductByIdHandler : IRequestHandler<DeleteProductByIdCommand, bool>
    {
        private readonly IProductRepository repository;

        public DeleteProductByIdHandler(IProductRepository repository)
        {
            this.repository = repository;
        }
        public async Task<bool> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            var product = await repository.GetProductById(request.Id);
            if (product == null)
            {
                throw new ApplicationException("The product does not exist");
            }

            return await repository.DeleteProduct(request.Id);
        }
    }
}
