using MediatR;
using Microsoft.IdentityModel.Tokens;
using Ripley.Api.Provider.Application.Contracts.Persistence;
using Ripley.Api.Provider.CrossCutting.Configuration;
using Ripley.Api.Provider.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ripley.Api.Provider.Application.Auth.Commands.UserAuthentication
{
    public class UserAuthenticationCommandHandler : IRequestHandler<UserAuthenticationCommand, UserAuthenticationCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSetting _appSettings;

        public UserAuthenticationCommandHandler(IUnitOfWork unitOfWork, AppSetting appSettings)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings;
        }

        private IUnitOfWork UnitOfWork => _unitOfWork;

        public async Task<UserAuthenticationCommandResponse> Handle(UserAuthenticationCommand request, CancellationToken cancellationToken)
        {
            var userAuthenticationCommandResponse = new UserAuthenticationCommandResponse();

            var validatorAuth = new UserAuthenticationCommandValidator();

            var validationResult = await validatorAuth.ValidateAsync(request, CancellationToken.None);

            if (validationResult.Errors.Any())
            {
                var expcetionResponse = new List<string>();

                foreach (var error in validationResult.Errors)
                {
                    expcetionResponse.Add(error.ErrorMessage);
                }

                if (expcetionResponse.Any())
                {
                    throw new Exception(String.Join(" | ", expcetionResponse.ToList()));
                }
            }
            var userResponseEntity = await UnitOfWork.UserRepository.ValidationCredentialsAsync(request.User, request.Password);
            if (ReferenceEquals(userResponseEntity, null))
                throw new Exception($"Oops! An exception has been generated, the username or password does not exist, try again.");

            if (userResponseEntity.DateExpiry == null)
                throw new Exception($"Oops! The user has expired, contact the system administrator");

            var token = await GenerarTokenJwtAsync(userResponseEntity);
            userAuthenticationCommandResponse.Message = "Successfully generated token";
            userAuthenticationCommandResponse.Token = token;

            return userAuthenticationCommandResponse;
        }

        private async Task<string> GenerarTokenJwtAsync(UserEntity userEntity)
        {
            DateTime expires = DateTime.Now.AddHours(_appSettings.HoursOfExpires);

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var rolEntityById = await UnitOfWork.RolRepository.GetByIdAsync(userEntity.RolId);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                  new Claim("user", userEntity.User),
                  new Claim("rolId", rolEntityById.Description)
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var response = tokenHandler.WriteToken(token);

            return response;
        }
    }
}
