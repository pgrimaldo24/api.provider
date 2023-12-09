namespace Ripley.Api.Provider.Domain.Entities
{
    public partial class EmailEntity
    { 
        public int Id { get; set; }
        public string Sender { get; set; }
        public string? Cc { get; set; }
        public string? ConfigSubject { get; set; }
        public string? ConfigContent { get; set; }
        public bool Active { get; set; } 
    }
}
