using System;
using System.Collections.Generic;

namespace Ripley.Api.Provider.Persistence.Models
{
    public partial class Email
    {
        public Email()
        {
            EmailHistories = new HashSet<EmailHistory>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProviderId { get; set; }
        public string? Cc { get; set; }
        public string? ConfigSubject { get; set; }
        public string? ConfigContent { get; set; }
        public bool Active { get; set; }
        public string? CreatedBy { get; set; }

        public virtual Provider Provider { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<EmailHistory> EmailHistories { get; set; }
    }
}
