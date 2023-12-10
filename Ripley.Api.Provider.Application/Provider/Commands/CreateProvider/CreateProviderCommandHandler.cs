using MediatR;
using Ripley.Api.Provider.Application.Contracts.Infraestructure;
using Ripley.Api.Provider.Application.Contracts.Persistence;
using Ripley.Api.Provider.Application.Mail.Model;
using Ripley.Api.Provider.Domain.Entities;

namespace Ripley.Api.Provider.Application.Provider.Commands.CreateProvider
{
    public class CreateProviderCommandHandler : IRequestHandler<CreateProviderCommand, CreateProviderCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailServerService _mailServerSerivce;

        public CreateProviderCommandHandler(IUnitOfWork unitOfWork, IMailServerService mailServerService)
        {
            _unitOfWork = unitOfWork;
            _mailServerSerivce = mailServerService;
        }

        private IUnitOfWork UnitOfWork => _unitOfWork;
        private IMailServerService MailServerService => _mailServerSerivce;

        public async Task<CreateProviderCommandResponse> Handle(CreateProviderCommand request, CancellationToken cancellationToken)
        {
            var createProviderCommandResponse = new CreateProviderCommandResponse();

            var validatorCreateProvider = new CreateProviderCommandValidator();

            var validationResult = await validatorCreateProvider.ValidateAsync(request, CancellationToken.None);

            if (validationResult.Errors.Any())
            {
                var expcetionResponse = new List<string>();

                foreach (var error in validationResult.Errors)
                {
                    expcetionResponse.Add(error.ErrorMessage);
                }

                if (expcetionResponse.Any()) throw new Exception(String.Join(" | ", expcetionResponse.ToList()));
            }

            var responseProvider = new ProviderEntity
            {
                VendorName = request.VendorName,
                VendorNumber = request.VendorNumber,
                ProviderAddress = request.Address,
                CategoryProvider = request.Rubro,
                Email = request.Email,
                ContactName = request.ContactName,
                ContactNumber = request.ContactNumber,
                Observations = request.Observations,
                CreatedBy = "sistemas",
                CreatedAt = DateTime.Now
            };

            responseProvider = await UnitOfWork.ProviderRepository.AddAsync(responseProvider);

            var dateExpiry = DateTime.Now;
            dateExpiry = dateExpiry.AddDays(1);
            var @createUserEntity = new UserEntity
            {
                User = request.Email,
                Password = "123456789",
                RolId = 1,
                StartDate = DateTime.Now,
                DateExpiry = dateExpiry
            };

            @createUserEntity = await UnitOfWork.UserRepository.AddAsync(@createUserEntity);

            await UnitOfWork.CompletedAsync();

            var emailEntity = await UnitOfWork.EmailRepository.GetSenderEmailAsync("sistemasripley@hotmail.com");

            if (ReferenceEquals(emailEntity, null))
                throw new Exception("Oops! An exception has been generated with the main email");

            var parameters = new ParametersMailSendingModel
            {
                sender_email = emailEntity.Sender,
                receptor_email = @createUserEntity.User,
                asunto = String.Format(emailEntity.ConfigSubject, responseProvider.VendorName),
                cc = emailEntity.Cc,
                description = String.Format(emailEntity.ConfigContent, responseProvider.VendorName, @createUserEntity.User)
            };

            MailServerService.SendEmail(parameters);

            var @history = new EmailHistoryEntity
            {
                From = parameters.sender_email,
                To = parameters.receptor_email,
                Cc = parameters.cc,
                Subject = parameters.asunto,
                Content = parameters.description,
                CreatedBy = "sistemas",
                CreatedAt = DateTime.Now
            };
            @history = await UnitOfWork.EmailHistoryRepository.AddAsync(@history);
            await UnitOfWork.CompletedAsync();
            createProviderCommandResponse.Message = "The provider was successfully registered";
            return createProviderCommandResponse;
        }
    }
}
