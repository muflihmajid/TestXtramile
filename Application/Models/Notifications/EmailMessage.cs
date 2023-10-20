using System.Collections.Generic;
namespace SceletonAPI.Application.Models.Notifications
{
    public class EmailMessage
    {
        public EmailMessage()
        {
            CC = new HashSet<string>();
            BCC = new HashSet<string>();
            Attachments = new HashSet<string>();
        }

        public string From { set; get; }
        public string FromName { set; get; }
        public string To { set; get; }
        public string Subject { set; get; }
        public string Body { set; get; }
        public ICollection<string> CC { set; get; }
        public ICollection<string> BCC { set; get; }
        public ICollection<string> Attachments { set; get; }

    }
}
