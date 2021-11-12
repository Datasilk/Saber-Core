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
        /// <param name="key">Data Source key</param>
        /// <returns></returns>
        DataSource Get(string key);

        /// <summary>
        /// Create a new record in your data source
        /// </summary>
        /// <param name="Request">Current request context</param>
        /// <param name="key">Data Source key</param>
        /// <param name="columns"></param>
        void Create(IRequest Request, string key, Dictionary<string, string> columns);

        /// <summary>
        /// Update an existing record in your data source
        /// </summary>
        /// <param name="Request">Current request context</param>
        /// <param name="key">Data Source key</param>
        /// <param name="id">ID of the record we will be updating</param>
        /// <param name="columns"></param>
        void Update(IRequest Request, string key, string id, Dictionary<string, string> columns);

        /// <summary>
        /// Returns a list of filtered data from a data source
        /// </summary>
        /// <param name="Request">Current request context</param>
        /// <param name="key">Data Source key</param>
        /// <param name="start">The starting row to get</param>
        /// <param name="length">The amount of rows to get. Make sure to return 1 extra row of data to signify that there is at least 1 more page of rows in the data source filter that can be viewed (e.g. length + 1 = 10 + 1 = 11 rows)</param>
        /// <param name="lang">The language to use when accessing your data source</param>
        /// <param name="filter">The filter to apply to the data source</param>
        /// <returns>List of data, each item in the list contains a Dictionary of key/value pairs that represent the fields and associated data for a given row within the data source</returns>
        List<Dictionary<string, string>> Filter(IRequest Request, string key, int start = 1, int length = 0, string lang = "en", List<DataSource.FilterGroup> filter = null, List<DataSource.OrderBy> orderBy = null);
    }
}
