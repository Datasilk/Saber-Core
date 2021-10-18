using System.Collections.Generic;
using Saber.Core;

namespace Saber.Vendor
{
    public interface IVendorDataSources
    {
        /// <summary>
        /// Name used to prefix data source names when displaying a list of all data sources in Saber (e.g. "My Data Source")
        /// </summary>
        string Vendor { get; set; }

        /// <summary>
        /// Name used to prefix data source keys when displaying a list of all data sources in Saber (e.g. "my-data-source")
        /// </summary>
        string Prefix { get; set; }

        /// <summary>
        /// A short summary of what the data sources are used for
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Get a list of available data sources
        /// </summary>
        /// <returns>Returns a list of key/value pairs. Keys should be alpha-numeric so they can be used within HTML element class attributes.</returns>
        List<KeyValuePair<string, string>> List();

        /// <summary>
        /// Get information about a specific data source
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        DataSource Get(string key);

        /// <summary>
        /// Returns a list of filtered data from a data source
        /// </summary>
        /// <param name="key">The name of the data source to get</param>
        /// <param name="start">The starting row to get</param>
        /// <param name="length">The amount of rows to get. Make sure to return 1 extra row of data to signify that there is at least 1 more page of rows in the data source filter that can be viewed (e.g. length + 1 = 10 + 1 = 11 rows)</param>
        /// <param name="lang">The language to use when accessing your data source</param>
        /// <param name="filter">The filter to apply to the data source</param>
        /// <returns>List of data, each item in the list contains a Dictionary of key/value pairs that represent the fields and associated data for a given row within the data source</returns>
        List<Dictionary<string, string>> Filter(string key, int start = 1, int length = 0, string lang = "en", Dictionary<string, object> filter = null);

        /// <summary>
        /// Display an HTML form used to filter data from the selected data source
        /// </summary>
        /// <param name="key">The name of the data source to display an HTML form for</param>
        /// <param name="request">The current request, used to include JavaScript & CSS for the HTML form. Use request.AddScript or request.AddCSS to include a file or and script tags to request.Scripts or append style tags to request.CSS.</param>
        /// <returns></returns>
        DataSourceFilterForm RenderFilters(string key, IRequest request, Dictionary<string, object> filter = null);
    }

    public class DataSourceFilterForm
    {
        /// <summary>
        /// HTML output of the filter form to inject onto the page
        /// </summary>
        public string HTML { get; set; }

        /// <summary>
        /// The name of a JavaScript function to call when initializing the filter form. Arguments for the callback function include (inputfield, a jQuery object representing the hidden input field used to store serialized filter settings).
        /// Use this callback function to add event listeners to your filter form fields onKeyUp & onChange JavaScript events that when triggered, will update the inputfield value). Make sure to keep the first part of the 
        /// inputfield value, which may look like "data-src=my-data-source|!|". To preserve this first part, set inputfield.val(inputfield.val().split('|!|')[0] + '|!|' + myfilters.join(',')) for example.
        /// </summary>
        public string OnInit { get; set; }

        /// <summary>
        /// Text to display on the anchor link that will navigate the user to the selected data source details. If left blank, Saber will not show a link to your data source details.
        /// </summary>
        public string LinkLabel { get; set; }

        /// <summary>
        /// The name of a JavaScript function to call when the user clicks the link label, which should navigate the user to the selected data source details. Saber will execute the provided JavaScript function 
        /// with the "key" argument that can be used to load a tab for the data source details based on the given key.
        /// </summary>
        public string OnLinkClick { get; set; }
    }
}
