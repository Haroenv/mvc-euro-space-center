using System;
using EuroSpaceCenter.Models;
using System.Net.Mail;
using System.Net;

namespace EuroSpaceCenter.util {
    internal class Email {
        internal static bool sendInvite(user u, activation a) {
            try {
                // Send email for activating
                string body = "Hello " + u.name + ",<br /><br />Thank you for registering on Space Fun!<br />Please click the following link to activate your account<br /><a href= 'http://eurospacecenter.haroenviaene.ikdoeict.net/Account/Activate/" + a.code + "'>Click here to activate your account.</a><br /><br />Space Fun";
                MailMessage message = new MailMessage();
                //"Space Fun", user.Email.ToString(), "Activation account", body
                message.Subject = "Space Fun - Activation account";
                message.From = new MailAddress("spacefunpark@hotmail.com");
                message.To.Add(new MailAddress(u.email));
                message.Body = body;
                message.IsBodyHtml = true;
                NetworkCredential netCred = new NetworkCredential("spacefunpark@hotmail.com", "ok");
                SmtpClient client = new SmtpClient("smtp.live.com", 587);
                client.EnableSsl = true;
                client.Credentials = netCred;
                client.Send(message);

                return true;
            } catch {
                return false;
            }
        }
    }
}