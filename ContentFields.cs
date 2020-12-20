using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Saber.Core
{
    public class ContentFields
    {
        public static string ContentFile(string path, string language)
        {
            var paths = PageInfo.GetRelativePath(path);
            var relpath = string.Join("/", paths);
            var file = paths[paths.Length - 1];
            var fileparts = file.Split(".", 2);
            return relpath.Replace(file, fileparts[0] + "_" + language + ".json");
        }

        public static Dictionary<string, string> GetPageContent(string path, string language)
        {
            if (language == "") { language = "en"; }
            var contentfile = App.MapPath(ContentFile(path, language));
            var exists = true;
            if (!File.Exists(contentfile))
            {
                contentfile = App.MapPath(ContentFile(path, "en"));
                exists = false;
            }
            var json = Cache.LoadFile(contentfile);
            if(json == "") { json = "{}"; exists = false; }
            var content = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            if (content != null) {
                if(exists == true && language != "en")
                {
                    //fill in gaps of data with English content
                    var json_en = Cache.LoadFile(ContentFile(path, "en"));
                    if(json_en != ""){
                        var content_en = JsonSerializer.Deserialize<Dictionary<string, string>>(json_en);
                        if (content_en != null)
                        {
                            foreach (var d in content_en)
                            {
                                if (!content.ContainsKey(d.Key))
                                {
                                    content.Add(d.Key, d.Value);
                                }
                                else
                                {
                                    if (content[d.Key] == "")
                                    {
                                        content[d.Key] = d.Value;
                                    }
                                }
                            }
                        }
                    }
                }
                return content;
            }
            return new Dictionary<string, string>();
        }
    }
}
