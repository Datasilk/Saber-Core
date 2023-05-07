using System.IO;
using System.Collections.Generic;


namespace Saber.Core
{
    public static class Website
    {
        /// <summary>
        /// Get a list of files belonging to the website
        /// </summary>
        /// <param name="include"></param>
        /// <returns></returns>
        public static List<string> AllFiles(string[]? include = null)
        {
            return Delegates.Website.AllFiles(include);
        }

        /// <summary>
        /// Get a list of folders within the website
        /// </summary>
        /// <returns></returns>
        public static List<string> AllFolders()
        {
            return Delegates.Website.AllFolders();
        }

        /// <summary>
        /// Get a list of all root folders within the website
        /// </summary>
        /// <returns></returns>
        public static List<string> AllRootFolders()
        {
            return Delegates.Website.AllRootFolders();
        }

        /// <summary>
        /// Reset Cached objects for the entire website
        /// </summary>
        /// <param name="path"></param>
        /// <param name="language"></param>
        public static void ResetCache(string path, string language = "en")
        {
            Delegates.Website.ResetCache(path, language);
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

        /// <summary>
        /// Import contents of a zip file into the current Saber website folders (Content & wwwroot folders). The zip file must
        /// adhere to the requirements of a Saber website folder structure
        /// </summary>
        /// <param name="zip"></param>
        public static void ImportWebsite(Stream zip, bool clean = false, string[] protectedFiles = null)
        {
            Delegates.Website.ImportWebsite(zip, clean, protectedFiles);
        }

        /// <summary>
        /// Gets the current file version for a user-generated file. So far, Saber supports *.js & *.css files for version caching
        /// </summary>
        /// <param name="file">relative path to file (e.g. "/wwwroot/content/pages/home.js")</param>
        /// <returns></returns>
        public static int GetFileVersion(string file)
        {
            return Delegates.Website.GetFileVersion(file);
        }

        /// <summary>
        /// Restarts the Saber web application by updating the root web.config
        /// </summary>
        public static void Restart()
        {
            Delegates.Website.Restart();
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
