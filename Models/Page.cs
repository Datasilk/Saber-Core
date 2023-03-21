using System;
using System.Collections.Generic;

namespace Saber.Models.Page
{
    public class Settings
    {
        public Title title { get; set; } = new Title();
        public string description { get; set; } = "";
        public string thumbnail { get; set; } = "";
        public DateTime datecreated { get; set; }
        public Security security { get; set; } = new Security();
        public string header { get; set; } = "";
        public string footer { get; set; } = "";
        public List<string> stylesheets { get; set; } = new List<string>();
        public List<string> scripts { get; set; } = new List<string>();
        public bool livetemplate { get; set; } = false;

        public Settings()
        {
            datecreated = DateTime.Now;
            header = "header.html";
            footer = "footer.html";
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
