using System;
using System.Collections.Generic;
using Saber.Core;

namespace Saber.Vendor
{
    public class HtmlComponentModel
    {
        /// <summary>
        /// Arguments: view, request, args, data, prefix, key. 
        /// These arguments represent the HTML view, page request, mustache variable properties, serialized user data, 
        /// variable prefix (e.g. "header-"), and variable name (which may contain user artifacts).
        /// Returns a list of Key/Value pairs to be injected into the view, the key being the variable name (including prefix), 
        /// and the value being the rendered HTML to replace the mustache variable with.
        /// </summary>
        public virtual Func<View, IRequest, Dictionary<string, string>, Dictionary<string, object>, string, string, List<KeyValuePair<string, string>>> Render { get; set; }

        /// <summary>
        /// key that is used to identify the html mustache variable (e.g. "my-plugin" would be used for variable {{my-plugin}} )
        /// </summary>
        public virtual string Key { get; set; }

        /// <summary>
        /// If true, key is a prefix and so the associated mustache variable must either use the key or use the key with a dash
        /// and a suffix (e.g. {{list}} or {{list-users}} will generate a list component)
        /// </summary>
        public virtual bool KeyIsPrefix { get; set; } = false;

        /// <summary>
        /// human-readable name of html variable
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// human-readable description of the HTML component's purpose. (Please keep it under 80 characters)
        /// </summary>
        public virtual string Description { get; set; } = "";

        /// <summary>
        /// An svg file used for the icon displayed for this HTML component within Saber's Editor UI. Your icon should be 64x64 pixels 
        /// in size and named icon.svg (e.g. "vendors/my-plugin/icon.svg")
        /// </summary>
        public virtual string Icon { get; set; } = "";

        /// <summary>
        /// An external website that contains documentation for your HTML component
        /// </summary>
        public virtual string Website { get; set; } = "";

        /// <summary>
        /// Determines if your HTML component acts as a mustache variable block, set Block = true
        /// </summary>
        public virtual bool Block { get; set; } = false;

        /// <summary>
        /// Determines if your HTML component can include an ID
        /// </summary>
        public virtual bool NoID { get; set; } = false;

        /// <summary>
        /// Determines if your HTML component will display a content field in the Page Content tab
        /// </summary>
        public virtual bool ContentField { get; set; } = true;

        /// <summary>
        /// When generating a special variable using Saber's HTML Component dropdown menu, HtmlHead will be included before the mustache variable is generated
        /// </summary>
        public virtual string HtmlHead { get; set; } = "";

        /// <summary>
        /// When generating a special variable using Saber's HTML Component dropdown menu, HtmlFoot will be included after the mustache variable is generated
        /// </summary>
        public virtual string HtmlFoot { get; set; } = "";

        //parameter list with human-readable information about each required & optional parameter
        public virtual Dictionary<string, HtmlComponentParameter> Parameters { get; set; } = new Dictionary<string, HtmlComponentParameter>();

    }

    public enum HtmlComponentParameterDataType
    {
        Text = 0,
        Number = 1,
        Boolean = 2,
        List = 3,
        Date = 4,
        DateTime = 5,
        Currency = 6,
        /// <summary>
        /// Allows the user to select an image from the page resources popup modal
        /// </summary>
        Image = 7,
        /// <summary>
        /// Allows the user to select a web page from their website by exploring their website structure via a popup modal
        /// </summary>
        WebPage = 8,
        /// <summary>
        /// Allows the user to select an HTML file from the partials folder & sub-folders
        /// </summary>
        PartialView = 9
    }

    public enum HtmlComponentCategory
    {
        UI = 0,
        Media = 1,
        Commerce = 2,
        Social = 3,
        Form = 4,
        Application = 5,
        Advertisment = 6
    }

    public class HtmlComponentParameter
    {
        /// <summary>
        /// Based on the data type you select, Saber will display the appropriate form field to the user
        /// </summary>
        public HtmlComponentParameterDataType DataType { get; set; }

        /// <summary>
        /// Human-readable name of your parameter to display when configuring a new instance of your HTML component
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// If true, the user cannot create an instance of the HTML component until the parameter contains a value
        /// </summary>
        public bool Required { get; set; } = false;

        /// <summary>
        /// If true, allows the user to add mutliple values to this parameter (when generating mustache code from the component menu)
        /// </summary>
        public bool List { get; set; } = false;

        /// <summary>
        /// If List = true, include an optional JavaScript function to execute when the user clicks the plus button located in the 
        /// list accordion title bar. This will override the default javascript execution for the plus button.
        /// </summary>
        public string AddItemJs { get; set; }

        /// <summary>
        /// Default value to display when configuring a new instance of your HTML component
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// If DataType = List, provide an array of strings for the user to choose from
        /// within a drop down list
        /// </summary>
        public KeyValuePair<string, string>[] ListOptions { get; set; } 

        /// <summary>
        /// A brief summary of what the parameter is used for
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Used to organize HTML components within Saber's Editor UI
        /// </summary>
        public HtmlComponentCategory Category { get; set; }

        /// <summary>
        /// The order in which to display the parameters when configuring a new instance of your HTML component
        /// </summary>
        public int Sort { get; set; }
    }
}
