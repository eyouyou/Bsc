using System;
using System.Net.Mail;
using System.Web;
using Bsc.Dmtds.Common.IO;
using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Sites.Models
{
    public static class ModelExtensions
    {
        public static Repository GetRepository(this Site site)
        {
            site = site.AsActual();
            if (site != null && !string.IsNullOrEmpty(site.Repository))
            {
                return new Repository(site.Repository).AsActual();
            }
            return null;
        }

        public static Bsc.Dmtds.Membership.Models.Membership GetMembership(this Site site)
        {
            site = site.AsActual();

            if (site != null && !string.IsNullOrEmpty(site.Membership))
            {
                return new Bsc.Dmtds.Membership.Models.Membership(site.Membership).AsActual();
            }
            return null;
        }

        public static void SendMailToCustomer(this Site site, string to, string subject, string body, bool isBodyHtml, HttpFileCollectionBase files = null)
        {
            var smtp = site.Smtp;
            if (smtp == null)
            {
                throw new ArgumentNullException("smtp");
            }

            MailMessage message = new MailMessage() { From = new MailAddress(smtp.From) };
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isBodyHtml;
            SmtpClient smtpClient = smtp.ToSmtpClient();

            smtpClient.Send(message);
        }

        public static void SendMailToSiteManager(this Site site, string from, string subject, string body, bool isBodyHtml, HttpFileCollectionBase files = null)
        {
            var smtp = site.Smtp;
            if (smtp == null)
            {
                throw new ArgumentNullException("smtp");
            }


            MailMessage message = new MailMessage() { From = new MailAddress(from) };
            foreach (var item in smtp.To)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    message.To.Add(item);
                }
            }
            message.Subject = subject;
            message.Body = body;


            message.IsBodyHtml = isBodyHtml;

            if (files != null && files.Count > 0)
            {
                foreach (string key in files.AllKeys)
                {
                    HttpPostedFileBase file = files[key] as HttpPostedFileBase;

                    message.Attachments.Add(new Attachment(file.InputStream, file.FileName, IOUtility.MimeType(file.FileName)));
                }
            }

            SmtpClient smtpClient = smtp.ToSmtpClient();

            smtpClient.Send(message);
        } 
    }
}