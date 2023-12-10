using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ripley.Api.Provider.Application.Products.Commands.CreateProduct;
using Ripley.Api.Provider.Application.Products.Queries.UpdateProduct;
using Ripley.Api.Provider.CrossCutting.Base.Exception;
using Ripley.Api.Provider.CrossCutting.Helpers;

namespace Ripley.Api.Provider.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private IMediator Mediator => _mediator;

        [HttpPost("create", Name = "CreateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateProductCommandResponse>> Create([FromBody] CreateProductCommand options)
        {
            try
            {
                var result = await Mediator.Send(options);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new BaseResponseException()
                    {
                        error_message = !ReferenceEquals(null, ex.InnerException) ? ex.InnerException.Message : ex.Message,
                        stackTracer = ex.StackTrace
                    });
            }
        }

        [HttpPut("update", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateProductCommandResponse>> Update(int productId)
        {
            try
            {
                var request = new UpdateProductCommand
                {
                    ProductId = productId
                };

                var result = await Mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new BaseResponseException()
                    {
                        error_message = !ReferenceEquals(null, ex.InnerException) ? ex.InnerException.Message : ex.Message,
                        stackTracer = ex.StackTrace
                    });
            }
        }
    }
}
