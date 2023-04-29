using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace Saber.Vendor
{
    /// <summary>
    /// Specify a custom email client that can be used to send emails to Saber users, 
    /// such as signup authentication & forgotten password emails.
    /// </summary>
    public interface IVendorEmailClient
    {
        /// <summary>
        /// Used to identify this email client (e.g. "send-grid")
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Human-readable name for your email client (e.g. "Send Grid")
        /// </summary>
        string Name { get; }

        /// <summary>
        /// A list of parameters used to configure the email client with. Include a parameter for your From address (if applicable)
        /// since emails sent using Saber's email platform does not provide a way to include a From address.
        /// </summary>
        Dictionary<string, EmailClientParameter> Parameters { get; set; }

        /// <summary>
        /// The Parameter key used to find the email address to send emails from.
        /// </summary>
        string FromKey { get; }

        /// <summary>
        /// The Parameter key used to find the name of the user to send emails from.
        /// </summary>
        string FromNameKey { get; }

        /// <summary>
        /// Executed when Saber starts running to give the email client a chance to load a config file into memory
        /// </summary>
        void Init();

        /// <summary>
        /// Load email client config JSON file into memory
        /// </summary>
        /// <returns>Key/value pairs of configuration data for email client parameters</returns>
        Dictionary<string, string> GetConfig();

        /// <summary>
        /// Executed when Saber saves the config data for your email client parameters. 
        /// You should save your parameters to a JSON config file using this method.
        /// </summary>
        /// <param name="parameters"></param>
        void SaveConfig(Dictionary<string, string> parameters);

        /// <summary>
        /// Check to see if email client is properly configured (for notification purposes)
        /// </summary>
        /// <returns></returns>
        bool IsConfigured();

        /// <summary>
        /// Executed when Saber is requesting to send an email via your email client
        /// </summary>
        /// <param name="message">Mail Message which includes all information about the email; to, from, subject, body, etc.</param>
        /// <param name="GetRFC2822">The RFC 2822 formatted email. Some clients require that it be Base64 URL encoded.</param>
        void Send(MailMessage message, Func<string> GetRFC2822);
    }
}
