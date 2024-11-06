namespace CA.And.DDD.Template.Infrastructure.Settings
{
    public class Smtp
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public string EmailFrom { get; set; }
    }
}
