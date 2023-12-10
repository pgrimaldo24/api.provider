using MediatR;
using Ripley.Api.Provider.Application.Contracts.Persistence;
using Ripley.Api.Provider.CrossCutting.Extensions.Pagination.QueryableExtension;
using Ripley.Api.Provider.Domain.Configuration.Pagination.Result;

namespace Ripley.Api.Provider.Application.Provider.Queries.DetailProducts
{
    public class DetailProductQueryHandler : IRequestHandler<DetailProductQuery, PaginationResultModel<DetailProductQueryVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DetailProductQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private IUnitOfWork UnitOfWork => _unitOfWork;

        public async Task<PaginationResultModel<DetailProductQueryVm>> Handle(DetailProductQuery request, CancellationToken cancellationToken)
        {
            var detailProductQueryVmList = new PaginationResultModel<DetailProductQueryVm>();
            var detailProductVmList = new List<DetailProductQueryVm>();
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
                    detailProductVmList.Add(productVm);
                }
            }

            var data = detailProductVmList.Where(x => !string.IsNullOrEmpty(x.Description));

            if (!string.IsNullOrEmpty(request.DescriptionProduct))
                data = data.Where(x => x.Description.Contains(request.DescriptionProduct));

            if (!string.IsNullOrEmpty(request.Category) && request.Category != "null")
                data = data.Where(x => x.Category == request.Category);

            if (!string.IsNullOrEmpty(request.Sucursal) && request.Sucursal != "null")
                data = data.Where(x => x.Sucursal == request.Sucursal);

            var query = data.Select(response => new DetailProductQueryVm
            {
                Id = response.Id,
                Description = response.Description,
                Category = response.Category,
                Sucursal = response.Sucursal,
                Stock = response.Stock,
                ImpuestoVent = response.ImpuestoVent,
                BrutoVent = response.BrutoVent
            }).AsQueryable();
            var response = query.SortBy(request.Order, request.ColumnOrder)
                         .GetPaged(request.Page, request.PageSize);
            detailProductQueryVmList = response;

            if (ReferenceEquals(detailProductQueryVmList, null))
                throw new Exception($"There was an error in the pagination configuration");
            return detailProductQueryVmList;
        }
    }
}
