using MediatR;
using Ripley.Api.Provider.Application.Contracts.Persistence;

namespace Ripley.Api.Provider.Application.Provider.Queries.DetailProducts
{
    public class DetailProductQueryHandler : IRequestHandler<DetailProductQuery, List<DetailProductQueryVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DetailProductQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private IUnitOfWork UnitOfWork => _unitOfWork;

        public async Task<List<DetailProductQueryVm>> Handle(DetailProductQuery request, CancellationToken cancellationToken)
        {
            var detailProductQueryVmList = new List<DetailProductQueryVm>(); 
            var merchant = await UnitOfWork.ProviderRepository.GetByIdAsync(request.MerchantId);
            if (ReferenceEquals(merchant, null))
                throw new Exception($"Oops! The supplier with code {request.MerchantId} does not exist");

            var products = await UnitOfWork.ProductRepository.GetProductsByMerchantId(merchant.Id);

            if (products.Any())
            {
                foreach (var product in products)
                {
                    var category = await UnitOfWork.CategoryRepository.GetByIdAsync(product.CategoryId);
                    var sucursal = await UnitOfWork.SucursalRepository.GetByIdAsync(product.SucursalId);

                    var productVm = new DetailProductQueryVm
                    {
                        Id = product.Id,
                        Description = product.Description,
                        Category = category.Description,
                        Sucursal = sucursal.DES_SUCURSAL,
                        Stock = product.Stock,
                        BrutoVent = product.BrutoVent,
                        ImpuestoVent = product.ImpuestoVent
                    };
                    detailProductQueryVmList.Add(productVm);
                }
            }
            return detailProductQueryVmList;
        }
    }
}
