using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace RecipeApp.Helper
{
    public class Email
    {
        public static int port = 587;
        public static string smtpServer = "smtp.gmail.com";
        public static string smtpUserName = "tricklyrecipeapp@gmail.com";
        public static string smtpUserPass = "bilmirem@";
        public static void SendEmail(string userEmail, string username, string Text, string recipeName)
        {
            using (SmtpClient smtpSend = new SmtpClient())
            {
                smtpSend.Host = Email.smtpServer;
                smtpSend.Port = Email.port;

                smtpSend.Credentials = new System.Net.NetworkCredential(Email.smtpUserName, Email.smtpUserPass);

                smtpSend.EnableSsl = true;

                MailMessage emailMessage = new System.Net.Mail.MailMessage();

                emailMessage.To.Add(userEmail);
                emailMessage.From = new MailAddress("tricklyrecipeapp@gmail.com");
                emailMessage.Subject = "Hormetli" + " " + username + recipeName + Text;
                emailMessage.Body = "Hormetli" + " " + username + recipeName + Text;



                smtpSend.Send(emailMessage);
            }
        }
    
    }

      
}