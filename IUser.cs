using System;
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
        bool ResetPass { get; set; }
        DateTime DateCreated { get; set; }
        string Language { get; set; }
        IUser SetContext(HttpContext context);
        void Save(bool changed = false);
        void LogIn(int userId, string email, string name, DateTime datecreated, string displayName = "", short userType = 1, bool photo = false);
        void LogOut();
        void SetLanguage(string language);
        string[] GetOpenTabs();
        void SaveOpenTabs(string[] tabs);
        void AddOpenTab(string filePath);
        void RemoveOpenTab(string filePath);
    }
}
