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
            RecurseDirectories(list, "/wwwroot", new string[] {App.IsDocker ? "/content/" : "\\content\\", App.IsDocker ? "/editor/" : "\\editor\\", "web.config", "website.css" });
            RecurseDirectories(list, "/wwwroot/content", new string[] { ".js", ".css" });
            if (include != null && include.Length > 0)
            {
                foreach(var i in include)
                {
                    RecurseDirectories(list, i);
                }
            }
            return list;
        }

        private static void RecurseDirectories(List<string> list, string path, string[] ignore = null)
        {
            var parent = new DirectoryInfo(App.MapPath(path));
            var dirs = parent.GetDirectories().Where(a => ignore != null ? ignore.Where(b => a.FullName.IndexOf(b) >= 0).Count() == 0 : true);
            list.AddRange(parent.GetFiles().Select(a => a.FullName).Where(a => ignore != null ? ignore.Where(b => a.IndexOf(b) >= 0).Count() == 0  : true));
            foreach(var dir in dirs)
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
            Cache.Remove(ContentFields.ContentFile(path, language));
            ViewCache.Remove(filepath + ".html");
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
    }
}
