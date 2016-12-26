using System;
using EuroSpaceCenter.Models;
using System.Net.Mail;
using System.Net;
using System.Web.Configuration;

namespace EuroSpaceCenter.util {
    internal class Email {
        internal static bool sendInvite(user u, activation a) {
            try {
                string body = "Hey " + u.name + ",<br><br>Welcome to Euro Space Center. Click <a href= 'http://eurospacecenter.haroenviaene.ikdoeict.net/Account/Activate/" + a.code + "'>this link</a> to verify your account!<br><br> if the link is not readable you can go to http://eurospacecenter.haroenviaene.ikdoeict.net/Account/Activate/" + a.code+ "/!<br><br>greetz,<br><br>Euro Space Center";
                MailMessage message = new MailMessage();
                message.Subject = "Euro Space Center - Activation account";
                message.From = new MailAddress(WebConfigurationManager.AppSettings["emailAddress"]);
                message.To.Add(new MailAddress(u.email));
                message.Body = body;
                message.IsBodyHtml = true;
                NetworkCredential netCred = new NetworkCredential(WebConfigurationManager.AppSettings["emailAddress"], WebConfigurationManager.AppSettings["emailPass"]);
                SmtpClient client = new SmtpClient(WebConfigurationManager.AppSettings["emailServer"], int.Parse(WebConfigurationManager.AppSettings["emailPort"]));
                client.EnableSsl = true;
                client.Credentials = netCred;
                client.Send(message);

                return true;
            } catch (Exception e) {
                throw e;
                return false;
            }
        }
    }
}