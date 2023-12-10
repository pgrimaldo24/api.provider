using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ripley.Api.Provider.Application.Provider.Commands.CreateProvider;
using Ripley.Api.Provider.Application.Provider.Queries.DetailProducts;
using Ripley.Api.Provider.CrossCutting.Base.Exception;
using Ripley.Api.Provider.CrossCutting.Helpers;

namespace Ripley.Api.Provider.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    { 
        private readonly IMediator _mediator;

        public ProviderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private IMediator Mediator => _mediator;

        [HttpPost("create", Name = "CreateProvider")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateProviderCommandResponse>> Create([FromBody] CreateProviderCommand options)
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

        [HttpGet("detail", Name = "DetailProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<DetailProductQueryVm>>> DetailProduct(ODataQuery options)
        {
            try
            { 
                var merchantIdFilter = options.Filters.FirstOrDefault(c => c.Attribute.Equals("merchantId", StringComparison.OrdinalIgnoreCase));

                int merchantId = 0;

                if (merchantIdFilter == null) throw new Exception("MerchantId field returns null");

                if (merchantIdFilter != null) merchantId = int.Parse(merchantIdFilter.Value);

                var request = new DetailProductQuery
                {
                    MerchantId  = merchantId
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
