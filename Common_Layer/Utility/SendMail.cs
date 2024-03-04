using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace Common_Layer.Utility
{
    public class SendMail
    {
        public string Send_Mail(string toEmail, string token)
        {
            try
            {
                string fromEmail = "raistar1067@gmail.com";
                string fromEmailPassword = "gxbo pbec gprc xbtb";
                MailMessage message = new MailMessage(fromEmail, toEmail);
                string mailBody = "Token Generated: " + token;
                message.Subject = "Token Generated For Resetting Password";
                message.Body = mailBody;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(fromEmail, fromEmailPassword);
                    smtp.Send(message);
                }
                return "Email sent successfully to: " + toEmail;
            }
            catch (Exception ex)
            {
                return "Failed to send email. Error: " + ex.Message;
            }
        }
    }
}
