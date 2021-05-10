using Flurl.Http;
using Newtonsoft.Json;
using Pizza.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Application.Mail
{
    public class MailService
    {
        FlurlClient flurlClient = new FlurlClient("http://api-portal-dev.fushan.fihnbb.com/mail/send-mail");
        public MailService()
        {
        }
        public async Task<bool> SendMail(MailModel mail)
        {
            flurlClient.Configure(settings => settings.Timeout = TimeSpan.FromHours(1));
            var result = await flurlClient.Request("")
                .PostJsonAsync(mail).ReceiveString();
            return true;
        }
    }
}
