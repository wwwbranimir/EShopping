using Catalog.Application.Dto;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAllTypesHandler:IRequestHandler<GetAllTypesQuery, IList<TypeDto>>
    {
        private readonly ITypesRepository repository;

        public GetAllTypesHandler(ITypesRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IList<TypeDto>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await repository.GetAllTypes();
            return ProductMapper.MapperExtension.Map<IList<TypeDto>>(types);
        }
    }
}
