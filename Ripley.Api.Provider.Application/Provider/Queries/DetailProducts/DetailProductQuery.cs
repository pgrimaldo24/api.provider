using MediatR;

namespace Ripley.Api.Provider.Application.Provider.Queries.DetailProducts
{
    public class DetailProductQuery : IRequest<List<DetailProductQueryVm>>
    {
        public int MerchantId { get; set; }
    }
}
