namespace Saber.Core
{
    public static class PageInfo
    {
        public static string[] GetRelativePath(string path)
        {
            return Delegates.PageInfo.GetRelativePath(path);
        }

        public static string ConfigFilePath(string path)
        {
            return Delegates.PageInfo.ConfigFilePath(path);
        }

        public static Models.Page.Settings GetPageConfig(string path)
        {
            return Delegates.PageInfo.GetPageConfig(path);
        }

        public static string NameFromFile(string filename)
        {
            return Delegates.PageInfo.NameFromFile(filename);
        }

        public static void SavePageConfig(string path, Models.Page.Settings config)
        {
            Delegates.PageInfo.SavePageConfig(path, config);
        }

        /// <summary>
        /// Removes all associated cache for a specific web page
        /// </summary>
        /// <param name="path">Relative path to the page (e.g. "/Content/pages/home.html")</param>
        /// <param name="language"></param>
        public static void ClearCache(string path, string language)
        {
            Delegates.PageInfo.ClearCache(path, language);
        }
    }
}
