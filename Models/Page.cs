using Saber.Core;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Saber.Models.Page
{
    public class Settings
    {
        [JsonPropertyName("title")]
        public Title Title { get; set; } = new Title();
        [JsonPropertyName("description")]
        public string Description { get; set; } = "";
        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; } = "";
        [JsonPropertyName("datecreated")]
        public DateTime DateCreated { get; set; }
        [JsonPropertyName("security")]
        public Security Security { get; set; } = new Security();
        [JsonPropertyName("header")]
        public string Header { get; set; } = "";
        [JsonPropertyName("footer")]
        public string Footer { get; set; } = "";
        [JsonPropertyName("stylesheets")]
        public List<string> Stylesheets { get; set; } = new List<string>();
        [JsonPropertyName("scripts")]
        public List<string> Scripts { get; set; } = new List<string>();
        [JsonPropertyName("from_lt")]
        public bool FromLiveTemplate { get; set; } = false;
        [JsonPropertyName("use_lt")]
        public bool UsesLiveTemplate { get; set; } = false;
        [JsonPropertyName("is_lt")]
        public bool IsLiveTemplate { get; set; } = false;
        [JsonIgnore]
        public bool IsFromTemplate { get; set; } = false;
        [JsonIgnore]
        public bool HtmlExists { get; set; } = false;
        [JsonIgnore]
        public string[] Paths { get; set; } = Array.Empty<string>();
        [JsonIgnore]
        public string TemplatePath { get; set; } = "";
        [JsonIgnore]
        public List<string> LiveStylesheets { get; set; } = new List<string>();
        [JsonIgnore]
        public List<string> LiveScripts { get; set; } = new List<string>();

        public Settings()
        {
            DateCreated = DateTime.Now;
            Header = "header.html";
            Footer = "footer.html";
        }

        public void Save()
        {
            Delegates.PageInfo.SavePageConfig(string.Join("/", Paths), this);
        }
    }

    public class Title
    {
        public string prefix { get; set; } = "";
        public string body { get; set; } = "";
        public string suffix { get; set; } = "";
    }

    public class Security
    {
        public int[] groups { get; set; } = new int[] { };
    }
}
