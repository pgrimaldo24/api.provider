namespace Ripley.Api.Provider.Persistence.Models
{
    public partial class EmailHistory
    {
        public int Id { get; set; }
        public int EmailId { get; set; }
        public string? To { get; set; }
        public string? From { get; set; }
        public string? Cc { get; set; }
        public string? Subject { get; set; }
        public string? Content { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Email Email { get; set; } = null!;
    }
}
