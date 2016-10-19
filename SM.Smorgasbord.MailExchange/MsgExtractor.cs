using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.WebServices.Data;
using System.IO;
using Aspose.Email.Outlook;
using System.Collections.ObjectModel;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace SM.Smorgasbord.MailExchange
{
    public class MsgExtractor
    {
        private EmailMessage message;
        private ExchangeService service = new ExchangeService();

        string uriString = "https://casarray1.hp.com/ews/Exchange.asmx";
        string userName = "zhend@hp.com";
        string password;
        string domain = "ASIAPACIFIC";
        private DateTime deleveryTime = new DateTime();

        public EmailMessage Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }

        public string UriString
        {
            get
            {
                return uriString;
            }
            set
            {
                uriString = value;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public string Domain
        {
            get
            {
                return domain;
            }
            set
            {
                domain = value;
            }
        }

        public void BuildFromMSGFile(Stream messageStream)
        {
            MapiMessage msg = MapiMessage.FromStream(messageStream);
            message = new EmailMessage(service);

            //Get the subject of the mail.
            string subject = msg.Subject;

            //Get Mail From
            MailReceipt mailFrom = ExtractReceipts(msg.Headers["From"])[0];
            //To List
            Collection<MailReceipt> toList = ExtractReceipts(msg.Headers["To"]);
            //CC list
            Collection<MailReceipt> ccList = ExtractReceipts(msg.Headers["Cc"]);


            message.Subject = subject;
            message.Body = new MessageBody();
            message.Body.BodyType = BodyType.HTML;
            message.Body.Text = msg.BodyHtml;

            //message.From = new EmailAddress(mailFrom.DisplayName, mailFrom.MailAddress);
            message.Sender = new EmailAddress(mailFrom.DisplayName, mailFrom.MailAddress);
            deleveryTime = msg.DeliveryTime;
            foreach (MailReceipt toUser in toList)
            {
                message.ToRecipients.Add(new EmailAddress(toUser.DisplayName, toUser.MailAddress));
            }
            foreach (MailReceipt ccUser in ccList)
            {
                message.CcRecipients.Add(new EmailAddress(ccUser.DisplayName, ccUser.MailAddress));
            }
        }

        public MsgExtractor()
        {
            ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(RemoteCertificateValidationHandler);
        }

        public void Login()
        {
            service.Credentials = new NetworkCredential(userName, password, domain);
            service.Url = new Uri(uriString);
        }

        public void CreateReply()
        {
            StringBuilder replyHeader = new StringBuilder();

            replyHeader.Append("<p class=\"MsoNormal\"><span style=\"font-size:11.0pt;color:#1F497D\"><o:p>&nbsp;</o:p></span></p>");
            replyHeader.Append("<div>");
            replyHeader.Append("<div style=\"border:none;border-top:solid #B5C4DF 1.0pt;padding:3.0pt 0in 0in 0in\">");
            replyHeader.Append("<p class=\"MsoNormal\" align=\"left\" style=\"text-align:left\">");
            replyHeader.Append("<b><span style=\"font-size:10.0pt;font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;\">From:</span></b>");

            replyHeader.Append("<span style=\"font-size:10.0pt;font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;\"> ");
            replyHeader.Append(message.Sender.Name);
            replyHeader.Append("<br>");
            replyHeader.Append("<b>Sent:</b>");
            replyHeader.Append(deleveryTime.ToString());
            replyHeader.Append("<br>");

            replyHeader.Append("<b>To:</b>");
            foreach (EmailAddress receiptTo in message.ToRecipients)
            {
                replyHeader.Append(receiptTo.Name).Append(";");
            }
            replyHeader.Append("<br>");
            if (message.CcRecipients.Count > 0)
            {
                replyHeader.Append("<b>CC:</b>");
                foreach (EmailAddress receiptCC in message.CcRecipients)
                {
                    replyHeader.Append(receiptCC.Name).Append(";");
                }
                replyHeader.Append("<br>");
            }

            replyHeader.Append("<b>Subject:</b>");
            replyHeader.Append(message.Subject);
            replyHeader.Append("<br>");
            replyHeader.Append("</span>");
            replyHeader.Append("</p>");
            replyHeader.Append("</div>");
            replyHeader.Append("</div>");
            message.Body.Text = replyHeader + message.Body.Text;
            if (!message.Subject.StartsWith("RE:"))
            {
                message.Subject = "RE: " + message.Subject;
            }
        }

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawString">"Display name" xxxx@hp.com, "Display name2" xxxx2@hp.com</param>
        /// <returns></returns>
        private Collection<MailReceipt> ExtractReceipts(string rawString)
        {
            Collection<MailReceipt> receipts = new Collection<MailReceipt>();
            if (!string.IsNullOrEmpty(rawString))
            {
                MemoryStream memoryStream = new MemoryStream();
                byte[] bytesContent = Encoding.ASCII.GetBytes(rawString);
                memoryStream.Write(bytesContent, 0, bytesContent.Length);
                memoryStream.Position = 0;
                while (memoryStream.Position < memoryStream.Length)
                {
                    MailReceipt receipt = GetReceiptItem(memoryStream);
                    receipts.Add(receipt);
                }
                memoryStream.Close();
            }
            return receipts;
        }

        private MailReceipt GetReceiptItem(Stream stream)
        {
            MailReceipt receipt = new MailReceipt();
            receipt.DisplayName = GetDisplayName(stream);
            receipt.MailAddress = GetMailAddress(stream);

            return receipt;
        }

        private string GetDisplayName(Stream stream)
        {
            StringBuilder displayName = new StringBuilder();

            int currentByte = stream.ReadByte();
            //Skip blanks
            while (currentByte >= 0 && currentByte == ' ')
            {
                currentByte = stream.ReadByte();
            }
            if (currentByte != '"')
            {
                throw new Exception("Not start with \"");
            }

            currentByte = stream.ReadByte();
            bool isEscape = false;
            while (currentByte >= 0)
            {
                if (currentByte == '\\')
                {
                    if (isEscape)
                    {
                        displayName.Append('\\');
                        isEscape = false;
                    }
                    else
                    {
                        isEscape = true;
                    }
                }
                else if (currentByte == '"')
                {
                    if (isEscape)
                    {
                        displayName.Append('"');
                        isEscape = false;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    if (isEscape)
                    {
                        displayName.Append(Convert.ToChar(currentByte));
                        isEscape = false;
                    }
                    else
                    {
                        displayName.Append(Convert.ToChar(currentByte));
                    }
                }
                currentByte = stream.ReadByte();
            }
            return displayName.ToString();
        }

        private string GetMailAddress(Stream stream)
        {
            StringBuilder mailAddress = new StringBuilder();

            int currentByte = stream.ReadByte();
            //Skip blanks
            while (currentByte >= 0 && currentByte == ' ')
            {
                currentByte = stream.ReadByte();
            }

            while (currentByte >= 0 && currentByte != ',')
            {
                mailAddress.Append((char)currentByte);
                currentByte = stream.ReadByte();
            }
            if (mailAddress.Length > 1)
            {
                if (mailAddress[0] == '<')
                {
                    mailAddress.Remove(0, 1);
                }
                if (mailAddress[mailAddress.Length - 1] == '>')
                {
                    mailAddress.Remove(mailAddress.Length - 1, 1);
                }
            }
            return mailAddress.ToString();
        }

        private bool RemoteCertificateValidationHandler(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true; //ignore the checks and go ahead
        }

        #endregion
    }
}
