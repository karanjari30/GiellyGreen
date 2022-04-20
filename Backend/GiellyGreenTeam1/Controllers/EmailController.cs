using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GiellyGreenTeam1.Controllers
{
    public class EmailController : ApiController
    {
        public void SendEmail(String ToEmail,String Subject,String Message)
        {
            var HostAdd = ConfigurationManager.AppSettings["Host"].ToString();
            var FromEmail = ConfigurationManager.AppSettings["FromMail"].ToString();
            var Password = ConfigurationManager.AppSettings["MailPassword"].ToString();

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(FromEmail);
            mailMessage.Subject = Subject;
            mailMessage.Body = Message;
            mailMessage.IsBodyHtml = true;

            string[] MultiEmailID = ToEmail.Split(',');
            foreach (string EmailID in MultiEmailID)
            {
                mailMessage.To.Add(new MailAddress(EmailID));
            }
            SmtpClient smtp = new SmtpClient();
            smtp.Host = HostAdd;

            smtp.EnableSsl = true;
            NetworkCredential networkCredential = new NetworkCredential();
            networkCredential.UserName = mailMessage.From.Address;
            networkCredential.Password = Password;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = networkCredential;
            smtp.Port = 587;
            smtp.Send(mailMessage);
        }
    }
}
