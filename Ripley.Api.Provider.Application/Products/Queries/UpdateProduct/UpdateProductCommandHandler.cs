using MediatR;
using Ripley.Api.Provider.Application.Contracts.Persistence;

namespace Ripley.Api.Provider.Application.Products.Queries.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork => _unitOfWork;

        public async Task<UpdateProductVm> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var updateProductVm = new UpdateProductVm();

            var product = await UnitOfWork.ProductRepository.GetByIdAsync(request.ProductId);

            if(request.Description != product.Description)
            {
                product.Description = request.Description;
            }

            if (request.CategoryId != product.CategoryId)
            {
                product.CategoryId = (int)request.CategoryId;
            }

            if (request.SucursalId != product.SucursalId)
            {
                product.SucursalId = (int)request.SucursalId;
            }

            if (request.Stock != product.Stock)
            {
                product.Stock = (int)request.Stock;
            }

            if (request.BrutoVent != product.BrutoVent)
            {
                product.BrutoVent = Convert.ToDecimal(request.BrutoVent);
            }

            if (request.ImpuestoVent != product.ImpuestoVent)
            {
                product.ImpuestoVent = Convert.ToDecimal(request.ImpuestoVent);
            }

            if (request.MerchantId != product.MerchantId)
            {
                product.MerchantId = (int)request.MerchantId;
            } 
            product = UnitOfWork.ProductRepository.Update(product);
            await UnitOfWork.CompletedAsync();
            updateProductVm.Message = $"The product with Id {request.ProductId} has been updated successfully"; 
            return updateProductVm;
        }
    }
}
