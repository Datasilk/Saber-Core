using Saber.Models.Website;
using Saber.Vendor;
using System;
using System.Net.Mail;

namespace Saber.Core
{
    public static class Email
    {
        public static Vendor.EmailAction GetAction(string key)
        {
            return Delegates.Email.GetAction(key);
        }

        public static Models.Website.EmailAction GetActionConfig(string key)
        {
            return Delegates.Email.GetActionConfig(key);
        }

        public static EmailClient GetClientConfig(Guid Id)
        {
            return Delegates.Email.GetClientConfig(Id);
        }

        public static IVendorEmailClient GetClient(string key)
        {
            return Delegates.Email.GetClient(key);
        }

        public static IVendorEmailClient GetClientForAction(Vendor.EmailAction action)
        {
            return Delegates.Email.GetClientForAction(action.Key);
        }

        public static IVendorEmailClient GetClientForAction(string key)
        {
            return Delegates.Email.GetClientForAction(key);
        }

        #region "Send"
        /// <summary>
        /// Create a mail message used to be sent by Saber. We do not define a From address since
        /// the preferred email client (Vendor.IVendorEmailClient) will supply a parameter for the 
        /// From address.
        /// </summary>
        /// <param name="to">The email address of the user you wish to send your email to</param>
        /// <param name="subject">The email subject line. This might be replaced before sending the email
        /// if the email message type used (Vendor.EmailType) allows a user-defined subject line.</param>
        /// <param name="body">The email HTML body</param>
        /// <param name="attachments">The relative path to any file attachments you wish to include. 
        /// If you want your email body to display image attachments, include an HTML <img/> tag with an 
        /// href that matches your attachment file path</param>
        /// <returns></returns>
        public static MailMessage Create(MailAddress from, MailAddress to, string subject, string body, string[] attachments = null)
        {
            var message = new MailMessage(from, to);
            if(attachments != null && attachments.Length > 0)
            {
                //TODO: include attachment data in message
            }
            message.Subject = subject;
            message.Body = body;
            return message;
        }

        /// <summary>
        /// Create a mail message used to be sent by Saber. 
        /// </summary>
        /// <param name="action">Associated email action used to create this message from</param>
        /// <param name="client">Associated email client to send the email from</param>
        /// <param name="to">The email address of the user you wish to send your email to</param>
        /// <param name="body">The email HTML body</param>
        /// <param name="attachments">The relative path to any file attachments you wish to include. 
        /// If you want your email body to display image attachments, include an HTML <img/> tag with an 
        /// href that matches your attachment file path</param>
        /// <returns></returns>
        public static MailMessage Create(Models.Website.EmailAction action, Guid clientId, MailAddress to, string body, string[] attachments = null)
        {
            var clientParams = Delegates.Email.GetClientConfig(clientId);
            var client = Delegates.Email.GetClientForAction(action.Type);
            var from = new MailAddress(clientParams.Parameters[client.FromKey], clientParams.Parameters[client.FromNameKey]);
            return Create(from, to, action.Subject, body, attachments);
        }

        /// <summary>
        /// Send an email using the preferred email client based on the type of email being sent
        /// </summary>
        /// <param name="type">Email Action key used</param>
        /// <param name="to">The email address of the user you wish to send your email to</param>
        /// <param name="body">The email HTML body</param>
        public static void Send(string type, MailAddress to, string body)
        {
            var action = Delegates.Email.GetActionConfig(type);
            if (action != null)
            {
                var msg = Create(action, action.ClientId, to, body);
                Delegates.Email.Send(msg, type);
            }
            else
            {
                throw new Exception("Count not find Email Action config");
            }
        }

        /// <summary>
        /// Send an email using the preferred email client based on the type of email being sent
        /// </summary>
        /// <param name="message">The message being sent</param>
        /// <param name="type">The type of message to send. Known types are "signup" and "updatepass", 
        /// but vendor plugins can contain custom message types by using Vendor.IVendorEmails.</param>
        public static void Send(MailMessage message, string type)
        {
            Delegates.Email.Send(message, type);
        }
        #endregion


    }
}
