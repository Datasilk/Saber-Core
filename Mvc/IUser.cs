using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Saber.Core
{
    public interface IUser
    {
        int UserId { get; set; }
        string Email { get; set; }
        string Name { get; set; }
        bool Photo { get; set; }
        bool IsAdmin { get; set; }
        bool PublicApi { get; set; }
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

        IUser SetContext(HttpContext context);
        void SetLanguage(string language); 
        void LogIn(int userId, string email, string name, DateTime datecreated, bool photo = false, bool isAdmin = false, bool publicApi = false);
        void LogOut();
        void Save(bool changed = false);

        string[] GetOpenTabs();
        void SaveOpenTabs(string[] tabs);
        void AddOpenTab(string filePath);
        void RemoveOpenTab(string filePath);
    }
}
