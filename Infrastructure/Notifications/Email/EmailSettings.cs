namespace SceletonAPI.Infrastructure.Notifications.Email
{
    public class EmailSettings
    {
        public string MailServer { set; get; }
        public string Target {set;get;}
        public int MailPort { set; get; }
        public string SenderName { set; get; }
        public string Sender { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
    }
}
