using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace Saber.Core
{
    /// <summary>
    /// Class used by Saber to handle Core functionality
    /// </summary>
    public static class Delegates
    {
        public static class Controller
        {
            public static Func<Core.Controller, string, bool> CheckSecurity { get; set; }
            
            public static Func<Core.Controller, IUser> GetUser { get; set; }
        }

        public static class Service
        {
            public static Func<Core.Service, string, bool> CheckSecurity { get; set; }

            public static Func<Core.Service, IUser> GetUser { get; set; }
        }

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

        public static class Log
        {
            public static Action<int, string, string, string, string> Error { get; set; }
        }

        public static class ContentFields
        {
            public static Func<View, int, Core.ContentFields.FieldType> GetFieldType { get; set; }
            public static Func<IRequest, string, View, string, string, Dictionary<string, string>, string> RenderForm {get;set;}
        }
    }
}
