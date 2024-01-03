using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
//using System.Web.Mail;
using EASendMail;
using SmtpClient = EASendMail.SmtpClient;

namespace UniqueTrade_App.CommanFunction
{
    public class SendMail
    {
        //public static string SendMails(string Tos, string CCs, string From,
        //   string Subject, string body, string filePath, bool sendMeCopy)
        //{

        //    try
        //    {
        //        if (bool.Parse(WebConfigurationManager.AppSettings["IsEmailAllow"]))
        //        {
        //            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
        //            message.To.Add(Tos); //recipient
        //            message.Subject = Subject;
        //            message.From = new System.Net.Mail.MailAddress(WebConfigurationManager.AppSettings["senderEmlID"].ToString(), WebConfigurationManager.AppSettings["senderName"].ToString()); //from email
        //            message.Body = body;
        //            message.IsBodyHtml = true;
        //            SmtpClient smtp = new SmtpClient();
        //            //smtp.Host = "smtpout.asia.secureserver.net";//relay-hosting.secureserver.net
        //            smtp.Host = WebConfigurationManager.AppSettings["senderHost"].ToString();
        //            smtp.EnableSsl = false;

        //            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
        //            NetworkCred.UserName = WebConfigurationManager.AppSettings["senderEmlID"].ToString();
        //            NetworkCred.Password = WebConfigurationManager.AppSettings["senderEmlPass"].ToString();
        //            smtp.UseDefaultCredentials = false;
        //            smtp.Credentials = NetworkCred;
        //            smtp.Port = int.Parse(WebConfigurationManager.AppSettings["senderPort"].ToString());//3535
        //            smtp.Send(message);
        //        }
        //        return "True";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.ToString(); ;
        //    }
        //}
        //public static string SendMailll(string Tos, string CCs, string From,
        //    string Subject, string body, string filePath, bool sendMeCopy)
        //{

        //    try
        //    {

        //        System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
        //        message.To.Add(Tos);
        //        message.CC.Add(CCs);
        //        message.From = new System.Net.Mail.MailAddress(From);
        //        //if (sendMeCopy)
        //        //    message.Bcc.Add(From);
        //        message.Subject = Subject;
        //        message.Body = body;
        //        message.IsBodyHtml = true;
        //        message.Priority = System.Net.Mail.MailPriority.High;
        //        if (!String.IsNullOrEmpty(filePath))
        //        {
        //            System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(filePath);
        //            message.Attachments.Add(attach);
        //        }
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = "smtpout.asia.secureserver.net";
        //        //smtp.Host = "smtpout.secureserver.net";
        //        smtp.EnableSsl = false;
        //        System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();

        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = NetworkCred;
        //        smtp.Port = 25;
        //        smtp.Send(message);
        //        message.Dispose();
        //        return "Your Enquiry Sent Successfully.";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.ToString(); ;
        //    }
        //}



        public static string mailMain(string tomail, string body, string subject)
        {
            try
            {
                SmtpMail oMail = new SmtpMail("TryIt");

                // Your gmail email address
                oMail.From = "RoyalBlocks52@gmail.com";
                // Set recipient email address
                oMail.To = tomail;

                // Set email subject
                oMail.Subject = subject;
                // Set email body
                //oMail.TextBody = body;
                oMail.HtmlBody = body;
                //oMail.IsBodyHtml = true;

                // Gmail SMTP server address
                SmtpServer oServer = new SmtpServer("smtp.gmail.com");

                // Gmail user authentication
                // For example: your email is "gmailid@gmail.com", then the user should be the same
                oServer.User = "RoyalBlocks52@gmail.com";

                // Create app password in Google account
                // https://support.google.com/accounts/answer/185833?hl=en
                //oServer.Password = "sahu7470@#";
                oServer.Password = "mvvwmgprudpmdoqg";



                // Set 465 port
                //oServer.Port = 465;
                oServer.Port = 465;

                // detect SSL/TLS automatically
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                Console.WriteLine("start to send email over SSL ...");

                EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
                oSmtp.SendMail(oServer, oMail);

                Console.WriteLine("email was sent successfully!");
                return "True";
            }
            catch (Exception ex)
            {
                Console.WriteLine("failed to send email with the following error:");
                Console.WriteLine(ex.Message);
                return ex.ToString();
            }
        }
    }
}