using MediatR;
using Ripley.Api.Provider.Application.Contracts.Persistence;
using Ripley.Api.Provider.Domain.Entities;

namespace Ripley.Api.Provider.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork => _unitOfWork;

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var createProductCommandResponse = new CreateProductCommandResponse();

            if (ReferenceEquals(request, null))
                throw new Exception($"Oops! El objeto de los productos ha retornado null");

            var @dataEntity = new ProductEntity
            {
                Description = request.DescriptionProduct,
                CategoryId = request.CategoryId,
                SucursalId = request.SucursalId,
                Stock = request.Stock,
                BrutoVent = request.BrutoVent,
                ImpuestoVent = request.ImpuestoVent,
                MerchantId = request.MerchantId,
                CreatedBy = "sistemas",
                CreatedAt = DateTime.UtcNow
            };
            @dataEntity = await UnitOfWork.ProductRepository.AddAsync(@dataEntity);

            await UnitOfWork.CompletedAsync();

            createProductCommandResponse.Message = "The product has been successfully registered";
            return createProductCommandResponse;
        }
    }
}
