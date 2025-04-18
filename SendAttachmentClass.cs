using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hitachi_Task
{
    class SendAttachmentClass
    {
        public static void SendAttachment()
        {
            string fromEmail = "valentin02kr@gmail.com";
            string password = "blhb ysps ktvr cozt";
            Console.Write("Write the email address to which you will send the attachment: ");
            string toEmail = Console.ReadLine();
            string subject = "Shortest Path CSV Report";
            string body = "In this attachment you will see the solution of the task.";
            string attachmentPath = "D:\\Test\\matrix.txt";

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(fromEmail);
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;

                if (File.Exists(attachmentPath))
                {
                    Attachment attachment = new Attachment(attachmentPath);
                    mail.Attachments.Add(attachment);
                }
                else
                {
                    Console.WriteLine("Attachment file not found.");
                    return;
                }

                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential(fromEmail, password);
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
                Console.WriteLine("Attachment file was sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
    }
}
