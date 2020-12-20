using System;
using System.Net.Mail;

namespace Saber.Core
{
    /// <summary>
    /// Class used by Saber to handle Core functionality
    /// </summary>
    public static class Delegates
    {
        public static class Email
        {
            /// <summary>
            /// Used by Saber to delegate execution of Core.Email.Send. Please do not modify this field.
            /// </summary>
            public static Action<MailMessage, string> Send { get; set; }
        }

        public static class Website
        {
            /// <summary>
            /// Used by Saber to delegate execution of Core.Website.SaveLessFile. Please do not modify this field.
            /// </summary>
            public static Action<string, string, string> SaveLessFile { get; set; }

            /// <summary>
            /// Used by Saber to delegate execution of Core.Website.CopyTempWebsite. Please do not modify this field.
            /// </summary>
            public static Action CopyTempWebsite { get; set; }

            public static class Settings
            {
                public static Func<Models.Website.Settings> Load { get; set; }

                public static Action<Models.Website.Settings> Save{ get; set; }
            }
        }
    }
}
