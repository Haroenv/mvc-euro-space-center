using System;
using EuroSpaceCenter.Models;
using System.Net.Mail;
using System.Net;
using System.Web.Configuration;
using RestSharp;
using RestSharp.Authenticators;

namespace EuroSpaceCenter.util {
    internal class Email {

        internal static bool sendVerification(user u, activation a) {
            string cta = "Verify now!";
            string link = $"https://eurospacecenter.haroenviaene.ikdoeict.net/Account/Activate?code={a.code}";
            string body = $"Welcome to Euro Space Center. You have just registered for an account!<br/> We just quickly want you to verify that you're actually you and then you're ready for liftoff 🚀.<br/> If the button doesn't work, try navigating 🛰 to {link}";
            string subject = "Activation";
            return Send(u.email, u.name, body, cta, link, subject);
        }

        internal static bool sendPlanInvite(user u, parkplan p) {
            string cta = "Join the plan!";
            string link = $"https://eurospacecenter.haroenviaene.ikdoeict.net/Plan/Join?id={p.id}";
            string body = $"Welcome to Euro Space Center. Someone invited you to be joined at their visit!<br/> We just quickly want you to verify that you're actually you and then you're ready for liftoff 🚀.<br/> If the button doesn't work, try navigating 🛰 to {link}.<br/> Don't worry if you don't have an account yet, you can make one now!";
            string subject = "Join a visit";
            return Send(u.email, u.name, body, cta, link, subject);
        }

        internal static bool Send(string recipient, string name, string body, string cta, string link, string subject) {
            try {
                string message = $"<!DOCTYPE html><html><head> <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"> <meta name=\"viewport\" content=\"width=device-width\"> </head><body style=\"background-color: #f6f6f6;font-family: -apple-system, Segoe UI, Roboto, sans-serif;-webkit-font-smoothing: antialiased;font-size: 14px;line-height: 1.4;margin: 0;padding: 0;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"body\" style=\"border-collapse: separate;mso-table-lspace: 0pt;mso-table-rspace: 0pt;width: 100%;background-color: #f6f6f6;\"><tbody> <tr> <td style=\"font-family: -apple-system, Segoe UI, Roboto, sans-serif;font-size: 16px !important;vertical-align: top;\">&nbsp;</td><td class=\"container\" style=\"font-family: -apple-system, Segoe UI, Roboto, sans-serif;font-size: 16px !important;vertical-align: top;display: block;max-width: 580px;padding: 0 !important;width: 100% !important;margin: 0 auto !important;\"> <div class=\"content\" style=\"box-sizing: border-box;display: block;margin: 0 auto;max-width: 580px;padding: 0 !important;\"> <span class=\"preheader\" style=\"color: transparent;display: none;height: 0;max-height: 0;max-width: 0;opacity: 0;overflow: hidden;mso-hide: all;visibility: hidden;width: 0;font-size: 16px !important;\">This is preheader text. Some clients will show this text as a preview.</span><table class=\"main\" style=\"border-collapse: separate;mso-table-lspace: 0pt;mso-table-rspace: 0pt;width: 100%;background: #fff;border-radius: 0 !important;border-left-width: 0 !important;border-right-width: 0 !important;\"><tbody> <tr> <td class=\"wrapper\" style=\"font-family: -apple-system, Segoe UI, Roboto, sans-serif;font-size: 16px !important;vertical-align: top;box-sizing: border-box;padding: 10px !important;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"border-collapse: separate;mso-table-lspace: 0pt;mso-table-rspace: 0pt;width: 100%;\"><tbody> <tr> <td style=\"font-family: -apple-system, Segoe UI, Roboto, sans-serif;font-size: 16px !important;vertical-align: top;\"> <p style=\"font-family: -apple-system, Segoe UI, Roboto, sans-serif;font-size: 16px !important;font-weight: normal;margin: 0;margin-bottom: 15px;\">Hey {name},</p><p style=\"font-family: -apple-system, Segoe UI, Roboto, sans-serif;font-size: 16px !important;font-weight: normal;margin: 0;margin-bottom: 15px;\">{body}</p><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"btn btn-primary\" style=\"border-collapse: separate;mso-table-lspace: 0pt;mso-table-rspace: 0pt;width: 100%;box-sizing: border-box;\"><tbody> <tr> <td align=\"left\" style=\"font-family: -apple-system, Segoe UI, Roboto, sans-serif;font-size: 16px !important;vertical-align: top;padding-bottom: 15px;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"border-collapse: separate;mso-table-lspace: 0pt;mso-table-rspace: 0pt;width: 100% !important;\"><tbody> <tr> <td style=\"font-family: -apple-system, Segoe UI, Roboto, sans-serif;font-size: 16px !important;vertical-align: top;background-color: #3498db;border-radius: 5px;text-align: center;\"> <a href=\"{link}\" target=\"_blank\" style=\"color: #ffffff;text-decoration: none;background-color: #3498db;border: solid 1px #3498db;border-radius: 5px;box-sizing: border-box;cursor: pointer;display: inline-block;font-size: 16px !important;font-weight: bold;margin: 0;padding: 12px 25px;text-transform: capitalize;border-color: #3498db;width: 100% !important;\">{cta}</a></td></tr></tbody></table></td></tr></tbody></table><p style=\"font-family: -apple-system, Segoe UI, Roboto, sans-serif;font-size: 16px !important;font-weight: normal;margin: 0;margin-bottom: 15px;\">Have a nice day!</p><p style=\"font-family: -apple-system, Segoe UI, Roboto, sans-serif;font-size: 16px !important;font-weight: normal;margin: 0;margin-bottom: 15px;\">Euro Space Center \uD83D\uDE80</p></td></tr></tbody></table></td></tr></tbody></table><div class=\"footer\" style=\"clear: both;padding-top: 10px;text-align: center;width: 100%;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"border-collapse: separate;mso-table-lspace: 0pt;mso-table-rspace: 0pt;width: 100%;\"><tbody> <tr> <td class=\"content-block\" style=\"font-family: -apple-system, Segoe UI, Roboto, sans-serif;font-size: 16px !important;vertical-align: top;color: #999999;text-align: center;\"> <span class=\"apple-link\" style=\"color: #999999;font-size: 16px !important;text-align: center;\">Euro Space Center, 1 rue Devant les H\u00EAtres, B-6890 Transinne, Belgium</span></td></tr></tbody></table></div></div></td><td style=\"font-family: -apple-system, Segoe UI, Roboto, sans-serif;font-size: 16px !important;vertical-align: top;\">&nbsp;</td></tr></tbody></table></body></html>";

                RestClient client = new RestClient();
                client.BaseUrl = new Uri("https://api.mailgun.net/v3");
                client.Authenticator = new HttpBasicAuthenticator("api", WebConfigurationManager.AppSettings["mailgunKey"]);
                RestRequest request = new RestRequest();
                request.AddParameter("domain", WebConfigurationManager.AppSettings["mailgunDomain"], ParameterType.UrlSegment);
                request.Resource = "{domain}/messages";
                request.AddParameter("from", $"Euro Space Center <postmaster@{WebConfigurationManager.AppSettings["mailgunDomain"]}>");
                request.AddParameter("to", recipient);
                request.AddParameter("subject", subject);
                request.AddParameter("html", message);
                request.Method = Method.POST;
                client.Execute(request);
                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
}