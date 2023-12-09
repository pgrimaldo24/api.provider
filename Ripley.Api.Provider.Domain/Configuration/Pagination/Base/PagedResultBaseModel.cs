namespace Ripley.Api.Provider.Domain.Configuration.Pagination.Base
{
    public class PagedResultBaseModel
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
    }
}
