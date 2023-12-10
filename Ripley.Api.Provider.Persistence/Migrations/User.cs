namespace Ripley.Api.Provider.Persistence.Models
{
    public partial class User
    {
        public User()
        {
            Emails = new HashSet<Email>();
        }

        public int Id { get; set; }
        public string User1 { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RolId { get; set; }
        public string? Email { get; set; }
        public bool Active { get; set; }

        public virtual Rol Rol { get; set; } = null!;
        public virtual ICollection<Email> Emails { get; set; }
    }
}
