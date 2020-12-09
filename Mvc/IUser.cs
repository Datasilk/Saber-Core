using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Saber.Core
{
    public interface IUser
    {
        int UserId { get; set; }
        short UserType { get; set; }
        string VisitorId { get; set; }
        string Email { get; set; }
        string Name { get; set; }
        string DisplayName { get; set; }
        bool Photo { get; set; }
        DateTime DateCreated { get; set; }

        /// <summary>
        /// User's prefered content display language
        /// </summary>
        string Language { get; set; }

        /// <summary>
        /// A list of security keys that the user was given to use with CheckSecurity method
        /// </summary>
        List<KeyValuePair<string, bool>> Keys { get; set; }

        /// <summary>
        /// A list of security groups that the user belongs to
        /// </summary>
        int[] Groups { get; set; }

        /// <summary>
        /// determines whether or not the user must reset their password
        /// </summary>
        bool ResetPass { get; set; }

        IUser SetContext(HttpContext context);
        void SetLanguage(string language); 
        void LogIn(int userId, string email, string name, DateTime datecreated, string displayName = "", short userType = 1, bool photo = false);
        void LogOut();
        void Save(bool changed = false);

        string[] GetOpenTabs();
        void SaveOpenTabs(string[] tabs);
        void AddOpenTab(string filePath);
        void RemoveOpenTab(string filePath);
    }
}
