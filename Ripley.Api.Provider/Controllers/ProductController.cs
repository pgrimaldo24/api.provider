using MailKit.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        [HttpGet("update", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UpdateProductVm>> Update(ODataQuery options)
        {
            try
            {
                var productIdFilter = options.Filters.FirstOrDefault(c => c.Attribute.Equals("productId", StringComparison.OrdinalIgnoreCase));
                var descriptionFilter = options.Filters.FirstOrDefault(c => c.Attribute.Equals("description", StringComparison.OrdinalIgnoreCase));
                var categoryIdFilter = options.Filters.FirstOrDefault(c => c.Attribute.Equals("categoryId", StringComparison.OrdinalIgnoreCase));
                var stockFilter = options.Filters.FirstOrDefault(c => c.Attribute.Equals("stock", StringComparison.OrdinalIgnoreCase));
                var sucursalIdFilter = options.Filters.FirstOrDefault(c => c.Attribute.Equals("sucursalId", StringComparison.OrdinalIgnoreCase));
                var brutoVentFilter = options.Filters.FirstOrDefault(c => c.Attribute.Equals("brutoVent", StringComparison.OrdinalIgnoreCase));
                var impuestoVentFilter = options.Filters.FirstOrDefault(c => c.Attribute.Equals("impuestoVent", StringComparison.OrdinalIgnoreCase));
                var merchantIdFilter = options.Filters.FirstOrDefault(c => c.Attribute.Equals("merchantId", StringComparison.OrdinalIgnoreCase));

                int productId = 0;
                string? descriptionStr = null;
                int? categoryId = 0;
                int? sucursalId = 0;
                int? stock = 0;
                decimal? brutoVent = 0;
                decimal? impuestoVent = 0;
                int? merchantId = 0;

                if (productIdFilter != null) productId = int.Parse(productIdFilter.Value);
                if (descriptionFilter != null) descriptionStr = descriptionFilter.Value;
                if (categoryIdFilter != null) categoryId = categoryIdFilter.Value == null ? 0 : int.Parse(categoryIdFilter.Value);
                if (stockFilter != null) sucursalId = stockFilter.Value == null ? 0 : int.Parse(stockFilter.Value);
                if (sucursalIdFilter != null) stock = sucursalIdFilter.Value == null ? 0 : int.Parse(sucursalIdFilter.Value);
                if (brutoVentFilter != null) brutoVent = brutoVentFilter.Value == null ? 0 : Convert.ToDecimal(brutoVentFilter.Value);
                if (impuestoVentFilter != null) impuestoVent = impuestoVentFilter.Value == null ? 0 : Convert.ToDecimal(impuestoVentFilter.Value);
                if (merchantIdFilter != null) merchantId = int.Parse(merchantIdFilter.Value);

                var request = new UpdateProductCommand
                {
                    ProductId = productId,
                    Description = descriptionStr,
                    CategoryId = categoryId,
                    SucursalId = sucursalId,
                    Stock = stock,
                    BrutoVent = brutoVent,
                    ImpuestoVent = impuestoVent,
                    MerchantId = merchantId
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
