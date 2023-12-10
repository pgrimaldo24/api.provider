using MediatR;
using Ripley.Api.Provider.Domain.Configuration.Pagination;
using Ripley.Api.Provider.Domain.Configuration.Pagination.Result;

namespace Ripley.Api.Provider.Application.Provider.Queries.DetailProducts
{
    public class DetailProductQuery : SortModel, IRequest<PaginationResultModel<DetailProductQueryVm>>
    {
        public int MerchantId { get; set; }
        public string? DescriptionProduct { get; set; }
        public string? Category { get; set; }
        public string? Sucursal { get; set; }
    }
}
