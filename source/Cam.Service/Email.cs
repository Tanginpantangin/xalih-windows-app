using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace Cam.Service
{
    /// <summary>
    /// Email service
    /// </summary>
    public class Email
    {

        #region Constant

        private const int EMAIL_COUNT = 10;
        private const string EMAIL_FROM_ADDRESS_FIRST = "tanginpantangin";
        private const string EMAIL_FROM_ADDRESS_LAST = "@gmail.com";
        private const string EMAIL_FROM_NAME = "Kawom Tanginpantangi";
        private const string EMAIL_FROM_PASS = "baigaor@2012";

        private const string EMAIL_TO_ADDRESS = "tanginpantangin@gmail.com";
        private const string EMAIL_TO_NAME = "Kawom Tanginpantangin";

        private const string EMAIL_MES_SUBJECT = "Log file from client";
        private const string EMAIL_MES_BODY = "Program auto send email with attach log file.";
        private const string EMAIL_HOST_NAME = "smtp.gmail.com";

        #endregion

        #region Public Method

        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="fileName">file name</param>
        public void SendMail(string fileName)
        {
            //Get random from addess
            Random random = new Random();
            int randNum = random.Next(1, EMAIL_COUNT);

            //Create info email
            string fromEmail = EMAIL_FROM_ADDRESS_FIRST + randNum.ToString() + EMAIL_FROM_ADDRESS_LAST;
            MailAddress fromAddress = new MailAddress(fromEmail, EMAIL_FROM_NAME);
            MailAddress toAddress = new MailAddress(EMAIL_TO_ADDRESS, EMAIL_TO_NAME);
            Attachment attach = new Attachment(fileName);

            //Create SmtpClient
            SmtpClient smtp = new SmtpClient()
            {
                Host = EMAIL_HOST_NAME,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, EMAIL_FROM_PASS)
            };

            //Send email
            using (MailMessage message = new MailMessage(fromAddress, toAddress))
            {
                message.Subject = EMAIL_MES_SUBJECT;
                message.Body = EMAIL_MES_BODY;
                message.Attachments.Add(attach);

                smtp.Send(message);
            }
            
        }
        #endregion

        
    }
}
