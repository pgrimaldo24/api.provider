using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ripley.Api.Provider.Application.Auth.Commands.UserAuthentication;
using Ripley.Api.Provider.CrossCutting.Base.Exception;

namespace Ripley.Api.Provider.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private IMediator Mediator => _mediator;

        [HttpPost("", Name = "UserAuthentication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserAuthenticationCommandResponse>> GetPromotionsRebates([FromBody] UserAuthenticationCommand options)
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
    }
}
