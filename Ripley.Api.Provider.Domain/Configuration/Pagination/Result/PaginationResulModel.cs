using Ripley.Api.Provider.Domain.Configuration.Pagination.Base;

namespace Ripley.Api.Provider.Domain.Configuration.Pagination.Result
{
    public class PaginationResultModel<T> : PagedResultBaseModel where T : class
    {
        public PaginationResultModel()
        {
            Results = new List<T>();
        }
        public IList<T> Results { get; set; }
    }
}
