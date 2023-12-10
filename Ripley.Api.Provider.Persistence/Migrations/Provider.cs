﻿namespace Ripley.Api.Provider.Persistence.Models
{
    public partial class Provider
    {
        public Provider()
        {
            Emails = new HashSet<Email>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string? VendorName { get; set; }
        public string? VendorNumber { get; set; }
        public string? ProviderAddress { get; set; }
        public string? CategoryProvider { get; set; }
        public string? Email { get; set; }
        public string? ContactName { get; set; }
        public string? ContactNumber { get; set; }
        public string? Observations { get; set; }
        public bool Active { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Email> Emails { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
