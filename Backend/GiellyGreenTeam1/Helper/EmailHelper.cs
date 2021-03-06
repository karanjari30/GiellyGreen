using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace GiellyGreenTeam1.Models
{
    public class EmailHelper
    {
        public static void SendEmailToSupplier(String ToEmail, int month, int year, Attachment attPDF, string companyname)
        {
            var HostAdd = ConfigurationManager.AppSettings["Host"].ToString();
            var FromEmail = ConfigurationManager.AppSettings["FromMail"].ToString();
            var Password = ConfigurationManager.AppSettings["MailPassword"].ToString();

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(FromEmail, "Gielly Green");
            mailMessage.Subject = "Your invoice for the " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month) + "-" + year;
            mailMessage.Body = "Please find attached a self-billed invoice to <b>" + companyname + " </b> , prepared on your behalf, as per the agreement. <br> Regard.<br> Gielly Green Limited";
            mailMessage.Attachments.Add(attPDF);
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(new MailAddress(ToEmail));
            
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