using Catalog.Application.Commands;
using Catalog.Application.Dto;
using Catalog.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    public class CatalogController : ApiController
    {
        private readonly IMediator mediator;

        public CatalogController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        [Route("ProductById/{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        public async Task<IActionResult> GetProductById(string id)
        {
            var query = new GetProductByIdQuery(id);
            var product = await mediator.Send(query);
            return Ok(product);
        }


        [HttpGet]
        [Route("ProductByName/{name}", Name = "GetProductByName")]
        [ProducesResponseType(typeof(IList<ProductDto>), 200)]
        public async Task<ActionResult<IList<ProductDto>>> GetProductByName(string name)
        {
            var query = new GetProductByNameQuery(name);
            var products = await mediator.Send(query);
            return Ok(products);
        }



        [HttpGet]
        [Route("GetAllProducts")]
        [ProducesResponseType(typeof(IList<ProductDto>), 200)]
        public async Task<ActionResult<IList<ProductDto>>> GetAllProducts()
        {
            var query = new GetAllProductsQuery();
            var products = await mediator.Send(query);
            return Ok(products);
        }

        [HttpGet]
        [Route("GetAllBrands")]
        [ProducesResponseType(typeof(IList<BrandDto>), 200)]
        public async Task<ActionResult<IList<BrandDto>>> GetAllBrands()
        {
            var query = new GetAllBrandsQuery();
            var products = await mediator.Send(query);
            return Ok(products);
        }

        [HttpGet]
        [Route("GetAllTypes")]
        [ProducesResponseType(typeof(IList<TypeDto>), 200)]
        public async Task<ActionResult<IList<TypeDto>>> GetAllTypes()
        {
            var query = new GetAllTypesQuery();
            var products = await mediator.Send(query);
            return Ok(products);
        }

        [HttpGet]
        [Route("ProductsByBrandName/{name}", Name = "GetProductsByBrandName")]
        [ProducesResponseType(typeof(IList<ProductDto>), 200)]
        public async Task<ActionResult<IList<ProductDto>>> GetProductsByBrandName(string name)
        {
            var query = new GetProductByBrandNameQuery(name);
            var product = await mediator.Send(query);
            return Ok(product);
        }


        [HttpPost]
        [Route("CreateProduct")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductCommand command)
        {
            var product = await mediator.Send(command);
            return Ok(product);
        }


        [HttpPut]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<ActionResult<bool>> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            var product = await mediator.Send(command);
            return Ok(product);
        }

        [HttpDelete]
        [Route("{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<ActionResult<bool>> DeleteProduct(string id)
        {
            var command = new DeleteProductByIdCommand(id);
            var product = await mediator.Send(command);
            return Ok(product);
        }
    }
}
