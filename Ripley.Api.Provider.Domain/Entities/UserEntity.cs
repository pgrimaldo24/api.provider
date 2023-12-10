namespace Ripley.Api.Provider.Domain.Entities
{
    public partial class UserEntity
    {
        public int Id { get; set; }
        public string User { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RolId { get; set; }
        public bool Active { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DateExpiry { get; set; }
        public virtual RolEntity Rol { get; set; } = null!;
    }
}
