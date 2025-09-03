namespace TechritizeAuthAPI.Helpers
{
    public class AppSettings
    {
        public JwtSettings Jwt { get; set; }
        public EmailSettings Email { get; set; }
    }

    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiresInMinutes { get; set; }
    }

    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string SenderEmail { get; set; }
        public string AppPassword { get; set; }
    }
}
