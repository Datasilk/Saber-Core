using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


namespace Saber.Core
{
    public static class Website
    {
        public static List<string> AllFiles(string[] include = null)
        {
            var list = new List<string>();
            RecurseDirectories(list, "/Content/pages");
            RecurseDirectories(list, "/Content/partials");
            list.Add(App.MapPath("/Content/website.less"));
            list.Add(App.MapPath("/Content/website.json"));
            RecurseDirectories(list, "/wwwroot", new string[] { App.IsDocker ? "/content/" : "\\content\\", App.IsDocker ? "/editor/" : "\\editor\\", "web.config", "website.css" });
            RecurseDirectories(list, "/wwwroot/content", new string[] { ".js", ".css" });
            if (include != null && include.Length > 0)
            {
                foreach (var i in include)
                {
                    RecurseDirectories(list, i);
                }
            }
            return list;
        }

        public static List<string> SystemPages { get; set; } = new List<string>()
        {
            "access-denied", "forgotpass", "forgotpass-complete", "home", "login", "passwordreset", 
            "resetpass", "resetpass-complete", "signup", "signup-complete"
        };

        public static List<string> AllFolders()
        {
            var list = new List<string>();
            list.AddRange(Directory.GetDirectories(App.MapPath("/Content/"))
                .Where(a => !a.Replace("\\", "/").Contains("/Content/temp")));
            list.AddRange(Directory.GetDirectories(App.MapPath("/wwwroot/"))
                .Where(a => !a.Replace("\\", "/").Contains("/wwwroot/editor")));
            return list;
        }

        public static List<string> AllRootFolders()
        {
            var root = App.MapPath("/");
            return Directory.GetDirectories(App.MapPath("/Content/"), "", SearchOption.TopDirectoryOnly)
                .Where(a => !a.Replace("\\", "/").Contains("/Content/temp"))
                .Select(a => a.Replace(root, "").Replace("\\", "/")).ToList();
        }

        private static void RecurseDirectories(List<string> list, string path, string[] ignore = null)
        {
            var parent = new DirectoryInfo(App.MapPath(path));
            if (!parent.Exists) { return; }
            var dirs = parent.GetDirectories().Where(a => ignore != null ? ignore.Where(b => a.FullName.IndexOf(b) >= 0).Count() == 0 : true);
            list.AddRange(parent.GetFiles().Select(a => a.FullName).Where(a => ignore != null ? ignore.Where(b => a.IndexOf(b) >= 0).Count() == 0 : true));
            foreach (var dir in dirs)
            {
                var subpath = dir.FullName;
                if (App.IsDocker)
                {
                    subpath = "/" + subpath.Replace(App.RootPath, "");
                }
                else
                {
                    subpath = "\\" + subpath.Replace(App.RootPath, "");
                }
                RecurseDirectories(list, subpath, ignore);
            }
        }

        public static void ResetCache(string path, string language = "en")
        {
            var paths = PageInfo.GetRelativePath(path);
            var filepath = string.Join("/", paths);
            var filename = ContentFields.ContentFile(path, language);
            Console.WriteLine("Reset cache for " + filename);
            Cache.Remove(filepath + ".json");
            Cache.Remove(filename);
            Console.WriteLine("Reset View cache for " + filepath + ".html");
            ViewCache.Remove(filepath + ".html");
            ViewCache.Remove(filename);
        }

        /// <summary>
        /// Generate a CSS file from LESS code
        /// </summary>
        /// <param name="content">LESS code</param>
        /// <param name="outputFile">CSS output file</param>
        public static void SaveLessFile(string content, string outputFile, string workingDir)
        {
            Delegates.Website.SaveLessFile(content, outputFile, workingDir);
        }

        /// <summary>
        /// This will copy all files from /Content/temp to their appropriate folders and initialize the default website.
        /// This only works if /Content/pages/home.html is missing. Do not use this method unless you wish to overwrite
        /// the existing website.
        /// </summary>
        public static void CopyTempWebsite()
        {
            Delegates.Website.CopyTempWebsite();
        }

        public static class Settings
        {
            /// <summary>
            /// Load website settings from /Content/website.json
            /// </summary>
            /// <returns></returns>
            public static Models.Website.Settings Load()
            {
                return Delegates.Website.Settings.Load();
            }

            /// <summary>
            /// Saves website settings to /Content/website.json
            /// </summary>
            public static void Save(Models.Website.Settings settings)
            {
                Delegates.Website.Settings.Save(settings);
            }
        }
    }
}
