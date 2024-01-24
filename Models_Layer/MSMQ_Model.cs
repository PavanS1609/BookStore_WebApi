using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;
using Experimental.System.Messaging;

namespace Models_Layer
{
    public class MSMQ_Model
    {
        MessageQueue messageQueue = new MessageQueue();
        private string receiverEmailAddr;
        private string receiverName;

        //Method to send Token Using MessageQueue and Delegate
        public void SendMessage(string token, string emailId, string name)
        {
            receiverEmailAddr = emailId;
            receiverName = name;
            messageQueue.Path = @".\Private$\Token";
            try
            {
                if (!MessageQueue.Exists(messageQueue.Path))
                {
                    MessageQueue.Create(messageQueue.Path);
                }
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                messageQueue.ReceiveCompleted += MessageQueue_ReceiverCompleted;
                messageQueue.Send(token);
                messageQueue.BeginReceive();
                messageQueue.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Delegate to send token as Message to the sender emailId and MailMessage
        private void MessageQueue_ReceiverCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = messageQueue.EndReceive(e.AsyncResult);
                string token = msg.Body.ToString();
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("pavankumar.springboot@gmail.com", "qwyh avtg bklg qspu"),
                };
                mailMessage.From = new MailAddress("pavankumar.springboot@gmail.com");
                mailMessage.To.Add(new MailAddress(receiverEmailAddr));
                string mailBody = $"<!DOCTYPE html>" +
                                  $"<html>" +
                                  $"<style>" +
                                  $".blink" +
                                  $"</style>" +
                                    $"<body style = \"backgroung-color:#DBFF73;text-align:center;padding:5px;\">" +
                                    $"<h1 style = \"color:#6A8D02; border-bottom: 3px solid #84AF08; margin-top: 5px;\"> Dear <b>{receiverName}<\b> </h1>\n" +
                                    $"<h3 style = \"color:#8AB411;\"> For Resetting Password The Below Link Is Issued</h3>" +
                                    $"<h3 style  = \"color:#8AB411;\"> Please Click The Link Below To Reset Your Password</h3>" +
                                    $"<a style = \"color:#00802b; text-decoration: none; font-size:20px;\" href = 'http://localhost:4200/ResetPassword/{token}'>Click me</a>\n" +
                                    $"<h3 style = \"color:#8AB411; margin-bottom:5px;\"><blink>This Token Will Be Valid For Next 6 Hours<blink></h3>" +
                                    $"</body>" +
                                    $"</html>";
                mailMessage.Body = mailBody;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = "Book Store Password Reset Link";
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
