using System;
using System.Collections.Generic;
using System.IO;
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
            public static Action<Core.Service> Init { get; set; }

            public static Func<IUser, string, bool> CheckSecurity { get; set; }

            public static Func<Core.Service, IUser> GetUser { get; set; }
        }

        public static class Session
        {
            public static Func<string, int, Dictionary<string, string>> Get { get; set; }
            public static Action<string, string, int> Set { get; set; }
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
            /// Used by Saber to delegate execution of Core.Website.AllFiles. Please do not modify this field.
            /// </summary>
            public static Func<string[]?, List<string>> AllFiles { get; set; }
            
            /// <summary>
            /// Used by Saber to delegate execution of Core.Website.AllFolders. Please do not modify this field.
            /// </summary>
            public static Func<List<string>> AllFolders { get; set; }

            /// <summary>
            /// Used by Saber to delegate execution of Core.Website.AllRootFolders. Please do not modify this field.
            /// </summary>
            public static Func<List<string>> AllRootFolders { get; set; }

            /// <summary>
            /// Used by Saber to delegate execution of Core.Website.ResetCache. Please do not modify this field.
            /// </summary>
            public static Action<string, string> ResetCache { get; set; }

            /// <summary>
            /// Used by Saber to delegate execution of Core.Website.SaveLessFile. Please do not modify this field.
            /// </summary>
            public static Action<string, string, string> SaveLessFile { get; set; }

            /// <summary>
            /// Used by Saber to delegate execution of Core.Website.CopyTempWebsite. Please do not modify this field.
            /// </summary>
            public static Action CopyTempWebsite { get; set; }

            /// <summary>
            /// Used by Saber to delegate execution of Core.Website.ImportWebsite. Please do not modify this field.
            /// </summary>
            public static Action<Stream, bool, string[]> ImportWebsite { get; set; }

            /// <summary>
            /// Used by Saber to delegate execution of Core.Website.ExportWebsite. Please do not modify this field.
            /// </summary>
            public static Func<bool, bool, bool, DateTime?, byte[]> ExportWebsite { get; set; }

            /// <summary>
            /// Used by Saber to delegate execution of Core.Website.Restart. Please do not modify this field.
            /// </summary>
            public static Action Restart { get; set; }

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
            public static Func<string, string> GetFieldId { get; set; }
            public static Func<View, int, List<Models.ContentFieldElementInfo>, Core.ContentFields.FieldType> GetFieldType { get; set; }
            public static Func<IRequest, string, View, string, string, Dictionary<string, string>, string[], Dictionary<string, Core.ContentFields.FieldType>, Dictionary<string, Dictionary<string, string>>, string> RenderForm {get;set;}
        }

        public static class DataSources
        {
            public static Action<DataSourceInfo> Add { get; set; }
            public static Func<IRequest, Vendor.DataSource, Vendor.DataSource.FilterElement, string> RenderFilter { get; set; }
            public static Func<IRequest, DataSourceInfo, List<Vendor.DataSource.FilterGroup>, string> RenderFilters { get; set; }
            public static Func<IRequest, DataSourceInfo, List<Vendor.DataSource.FilterGroup>, int, string> RenderFilterGroups { get; set; }
            
            public static Func<Vendor.DataSource.OrderBy, string> RenderOrderBy { get; set; }
            public static Func<DataSourceInfo, List<Vendor.DataSource.OrderBy>, string> RenderOrderByList { get; set; }

            public static Func<DataSourceInfo, Vendor.DataSource.PositionSettings, string> RenderPositionSettings { get; set; }
        }

        public static class Image
        {
            public static Action<string, string, int> Shrink { get; set; }
            public static Action<string, string, int> ConvertPngToJpg { get; set; }
        }

        public static class PageInfo
        {
            public static Func<string, string[]> GetRelativePath { get; set; }
            public static Func<string, string> ConfigFilePath { get; set; }
            public static Func<string, Models.Page.Settings> GetPageConfig { get; set; }
            public static Func<string, string> NameFromFile { get; set; }
            public static Action<string, Models.Page.Settings> SavePageConfig { get; set; }
            public static Action<string, string> ClearCache { get; set; }
        }
    }
}
