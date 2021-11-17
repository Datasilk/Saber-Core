using System.Collections.Generic;

namespace Saber.Core
{
    public static class Security
    {
        public static List<SecurityKey> Keys { get; set; } = new List<SecurityKey>()
        {
            new SecurityKey(){
                Value = "code-editor",
                Label = "Code Editor",
                Description = "Edit HTML, LESS, CSS, & JavaScript files using the code editor"
            },
            new SecurityKey()
            {
                Value = "upload",
                Label = "Upload Files",
                Description = "Upload files to /wwwroot & upload page resources"
            },
            new SecurityKey()
            {
                Value = "delete-files",
                Label = "Delete Files",
                Description = "Delete files under /wwwroot and within page resources"
            },
            new SecurityKey()
            {
                Value = "delete-pages",
                Label = "Delete Pages",
                Description = "Delete files under the /pages & /partials folders"
            },
            new SecurityKey()
            {
                Value = "edit-content",
                Label = "Edit Content",
                Description = "Edit multilingual content for web pages"
            },
            new SecurityKey()
            {
                Value = "edit-datasources",
                Label = "Edit Data Sources",
                Description = "Add & edit records for supported data sources"
            },
            new SecurityKey()
            {
                Value = "page-settings",
                Label = "Page Settings",
                Description = "Update settings for web pages, such as title, description & loaded scripts"
            },
            new SecurityKey()
            {
                Value = "website-settings",
                Label = "Website Settings",
                Description = "Update website settings, such as icons and plugins"
            },
            new SecurityKey()
            {
                Value = "website-analytics",
                Label = "Website Analytics",
                Description = "Update application settings, such as website icons"
            },
            new SecurityKey()
            {
                Value = "manage-users",
                Label = "Manage Users",
                Description = "View, update, and create users and alter their security permissions."
            },
            new SecurityKey()
            {
                Value = "manage-security",
                Label = "Manage Security",
                Description = "View, update, and create security groups."
            }
        };
    }
}
