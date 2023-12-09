namespace Ripley.Api.Provider.Domain.Configuration.Pagination
{
    public class SortModel : PaginationModel
    {
        public string ColumnOrder { get; set; } = string.Empty;
        public string Order { get; set; } = string.Empty;
    }
}
