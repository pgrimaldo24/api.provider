using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ripley.Api.Provider.Application.Provider.Commands.CreateProvider;
using Ripley.Api.Provider.Application.Provider.Queries.DetailProducts;
using Ripley.Api.Provider.CrossCutting.Base.Exception;
using Ripley.Api.Provider.CrossCutting.Helpers;
using Ripley.Api.Provider.Domain.Configuration.Pagination.Result;

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
        public async Task<ActionResult<PaginationResultModel<DetailProductQueryVm>>> DetailProduct(ODataQuery options)
        {
            try
            {
                var pageFilter = options.Filters.FirstOrDefault(c => c.Attribute.Equals("page", StringComparison.OrdinalIgnoreCase));
                var pageSizeFilter = options.Filters.FirstOrDefault(c => c.Attribute.Equals("pageSize", StringComparison.OrdinalIgnoreCase));
                var columnOrderFilter = options.Filters.FirstOrDefault(c => c.Attribute.Equals("columnOrder", StringComparison.OrdinalIgnoreCase));
                var merchantIdFilter = options.Filters.FirstOrDefault(c => c.Attribute.Equals("merchantId", StringComparison.OrdinalIgnoreCase));
                var descriptionFilter = options.Filters.FirstOrDefault(c => c.Attribute.Equals("description", StringComparison.OrdinalIgnoreCase));
                var categoryIdFilter = options.Filters.FirstOrDefault(c => c.Attribute.Equals("categoryId", StringComparison.OrdinalIgnoreCase));
                var sucursalIdFilter = options.Filters.FirstOrDefault(c => c.Attribute.Equals("sucursalId", StringComparison.OrdinalIgnoreCase));

                int page = 0;
                int pageSize = 0;
                string columnOrderStr = string.Empty;
                int merchantId = 0;
                string? descriptionStr = string.Empty;
                string? category = string.Empty;
                string? sucursal = string.Empty;

                if (pageFilter == null) throw new Exception("Page field returns null");
                if (pageSizeFilter == null) throw new Exception("PageSize field returns null");
                if (columnOrderFilter == null) throw new Exception("ColumnOrder field returns null");
                if (merchantIdFilter == null) throw new Exception("MerchantId field returns null");
                if (descriptionFilter == null) throw new Exception("Description field returns null");
                if (categoryIdFilter == null) throw new Exception("CategoryId field returns null");
                if (sucursalIdFilter == null) throw new Exception("SucursalId field returns null");

                if (pageFilter != null) page = int.Parse(pageFilter.Value);
                if (pageSizeFilter != null) pageSize = int.Parse(pageSizeFilter.Value);
                if (columnOrderFilter != null) columnOrderStr = columnOrderFilter.Value;
                if (merchantIdFilter != null) merchantId = int.Parse(merchantIdFilter.Value);
                if (descriptionFilter != null) descriptionStr = descriptionFilter.Value;
                if (categoryIdFilter != null) category = categoryIdFilter.Value;
                if (sucursalIdFilter != null) sucursal = sucursalIdFilter.Value;

                var request = new DetailProductQuery
                {
                    Page = page,
                    PageSize = pageSize,
                    ColumnOrder = columnOrderStr,
                    Order = "ASC",
                    MerchantId = merchantId,
                    DescriptionProduct = descriptionStr,
                    Category = category,
                    Sucursal = sucursal
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
