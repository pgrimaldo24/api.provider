namespace Ripley.Api.Provider.Domain.Entities
{
    public partial class EmailHistoryEntity
    {
        public int Id { get; set; }
        public string? To { get; set; }
        public string? From { get; set; }
        public string? Cc { get; set; }
        public string? Subject { get; set; }
        public string? Content { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
