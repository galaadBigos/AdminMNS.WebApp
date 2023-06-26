namespace AdminMNS.WebApp.Options
{
    public class MailOptions
    {
        public string Email { get; set; } = null!;
        public string Server { get; set; } = null!;
        public int Port { get; set; }
        public bool Ssl { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
