namespace Ripley.Api.Provider.Domain.Entities
{
    public partial class RolEntity
    {
        public RolEntity()
        {
            Users = new HashSet<UserEntity>();
        }

        public int RolId { get; set; }
        public string Description { get; set; } = null!;
        public bool Active { get; set; }

        public virtual ICollection<UserEntity> Users { get; set; }
    }
}
