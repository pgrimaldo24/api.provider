namespace Ripley.Api.Provider.CrossCutting.Configuration
{
    public class AppSetting
    {
        public ConnectionString ConnectionStrings { get; set; }
        public string Secret { get; set; }
        public int HoursOfExpires { get; set; }
        public string Password { get; set; }
    }

    public class ConnectionString
    {
        public string DataSource { get; set; }
        public string Catalog { get; set; }
    }
}
