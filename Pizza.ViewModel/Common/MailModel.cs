using System;
using System.Collections.Generic;
using System.Text;

namespace Pizza.ViewModel.Common
{
    public class MailModel
    {
        public string Sender { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
