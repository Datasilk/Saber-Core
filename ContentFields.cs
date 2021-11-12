using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Saber.Core
{
    public static class ContentFields
    {
        public enum FieldType
        {
            vendor = 0,
            block = 1,
            text = 2,
            number = 3,
            image = 4,
            partial = 5,
            linebreak = 6,
            list = 7,
            datetime = 8
        }

        public static string ContentFile(string path, string language)
        {
            var paths = PageInfo.GetRelativePath(path);
            var relpath = string.Join("/", paths);
            var file = paths[paths.Length - 1];
            var fileparts = file.Split(".", 2);
            return relpath.Replace(file, fileparts[0] + "_" + language + ".json");
        }

        public static Dictionary<string, string> GetPageContent(string path, string language = "")
        {
            if (language == "") { language = "en"; }
            var contentfile = App.MapPath(ContentFile(path, language));
            var exists = true;
            if (!File.Exists(contentfile))
            {
                exists = false;
            }
            var json = Cache.LoadFile(contentfile);
            if(json == "") { json = "{}"; exists = false; }
            var content = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            if (content != null) {
                if(exists == true)
                {
                    //fill in gaps of data with English content
                    var json_en = Cache.LoadFile(ContentFile(path, "en"));
                    if(json_en != "")
                    {
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
                                    if (string.IsNullOrEmpty(content[d.Key]))
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

        public static FieldType GetFieldType(View view, int index, List<Models.ContentFieldElementInfo> elemsInfo)
        {
            return Delegates.ContentFields.GetFieldType(view, index, elemsInfo);
        }

        /// <summary>
        /// Generate an HTML form for all content fields within a partial view.
        /// </summary>
        /// <param name="request">The currente IRequest object</param>
        /// <param name="title">Title of the form</param>
        /// <param name="view">Partial view to collect mustache variables from</param>
        /// <param name="language">Selected language used to pass into all vendor HTML Components found in the partial view</param>
        /// <param name="container">CSS selector of the HTML container that this form will be injected into. This field is passed into all vendor HTML Components found in the partial view.</param>
        /// <param name="fields">The values associated with each mustache variable in the partial view.</param>
        /// <returns>An HTML string representing the content fields form</returns>
        public static string RenderForm(IRequest request, string title, View view, string language, string container, Dictionary<string, string> fields, string[] excludedFields = null, Dictionary<string, ContentFields.FieldType> fieldTypes = null)
        {
            return Delegates.ContentFields.RenderForm(request, title, view, language, container, fields, excludedFields, fieldTypes);
        }
    }
}
