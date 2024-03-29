﻿using System.Collections.Generic;
using System.IO;

namespace Saber
{
    public enum Environment
    {
        development = 0,
        staging = 1,
        production = 2
    }

    public static class App
    {
        public static Environment Environment { get; set; } = Environment.development;
        public static string Name { get; set; }
        public static string[] Host { get; set; }
        /// <summary>
        /// public domain found in config.json hostUri property
        /// </summary>
        public static string HostUri { get; set; }
        public static bool IsDocker { get; set; }
        public static bool CookiesUseSameSiteNone { get; set; }
        public static Dictionary<string, string> Languages { get; set; }
        public static string[] ServicePaths { get; set; }
        public static Models.Website.Settings Website { get; set; }
        public static string Version { get; set; }

        private static string _rootPath { get; set; }

        public static string RootPath
        {
            get
            {
                if (string.IsNullOrEmpty(_rootPath))
                {
                    _rootPath = Path.GetFullPath(".").Replace("\\", "/");
                }
                return _rootPath;
            }
        }

        public static string MapPath(string path = "")
        {
            path = path.Replace("\\", "/");
            if(path.IndexOf(RootPath) == 0) { return path; }
            if (path.Substring(0, 1) == "/") { path = path.Substring(1); } //remove slash at beginning of string
            if (IsDocker)
            {
                return Path.Join(RootPath, path).Replace("\\", "/");
            }
            else
            {
                return Path.Join(RootPath.Replace("/", "\\"), path.Replace("/", "\\"));
            }
        }
    }
}
