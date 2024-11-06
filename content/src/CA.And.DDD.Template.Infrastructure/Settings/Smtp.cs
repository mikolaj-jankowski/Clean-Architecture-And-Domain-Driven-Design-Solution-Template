namespace CA.And.DDD.Template.Infrastructure.Settings
{
    public class Smtp
    {
        public required string Server { get; set; }
        public required int Port { get; set; }
        public required string User { get; set; }
        public required string Password { get; set; }
        public required bool EnableSsl { get; set; }
        public required string EmailFrom { get; set; }
    }
}
