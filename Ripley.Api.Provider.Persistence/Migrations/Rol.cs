using System;
using System.Collections.Generic;

namespace Ripley.Api.Provider.Persistence.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Users = new HashSet<User>();
        }

        public int RolId { get; set; }
        public string Description { get; set; } = null!;
        public bool Active { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
