using System;
using System.Net.Mail;

namespace Saber.Core
{
    public static class Email
    {
        /// <summary>
        /// The external method used to handle sending emails through Saber via the preferred email client
        /// </summary>
        public static Action<MailMessage, string> Handle { get; set; }

        /// <summary>
        /// Send an email using the preferred email client based on the type of email being sent
        /// </summary>
        /// <param name="message">The message being sent</param>
        /// <param name="type">The type of message to send. Known types are "signup", "forgotpass", and "newsletter", 
        /// but vendor plugins can contain custom message types.</param>
        public static void Send(MailMessage message, string type)
        {
            Handle(message, type);
        }
    }
}
