using System.Collections.Generic;
using Saber.Core;

namespace Saber.Vendor
{
    public interface IVendorDataSources
    {
        /// <summary>
        /// Name used when displaying a list of all data sources in Saber (e.g. "My Data Source")
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
        /// Executed after all Data Sources are instantiated on Startup
        /// </summary>
        void Init();

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
        void Create(IRequest request, string key, Dictionary<string, string> columns);

        /// <summary>
        /// Update an existing record in your data source
        /// </summary>
        /// <param name="Request">Current request context</param>
        /// <param name="key">Data Source key</param>
        /// <param name="id">ID of the record we will be updating</param>
        /// <param name="columns"></param>
        void Update(IRequest request, string key, string id, Dictionary<string, string> columns);

        /// <summary>
        /// Returns a list of filtered records from a data source
        /// </summary>
        /// <param name="Request">Current request context</param>
        /// <param name="key">Data Source key</param>
        /// <param name="start">The starting row to get</param>
        /// <param name="length">The amount of rows to get. Make sure to return 1 extra row of data to signify that there is at least 1 more page of rows in the data source filter that can be viewed (e.g. length + 1 = 10 + 1 = 11 rows)</param>
        /// <param name="lang">The language to use when accessing your data source</param>
        /// <param name="filter">The filter to apply to the data source</param>
        /// <param name="orderBy">The columns used to sort the records by</param>
        /// <returns>List of data, each item in the list contains a Dictionary of key/value pairs that represent the fields and associated data for a given row within the data source</returns>
        List<Dictionary<string, string>> Filter(IRequest request, string key, int start = 1, int length = 0, string lang = "en", List<DataSource.FilterGroup> filter = null, List<DataSource.OrderBy> orderBy = null);

        /// <summary>
        /// Returns a Dictionary of data source keys and associated records based on a parent data set and it's relationships with other datasets
        /// </summary>
        /// <param name="Request">Current request context</param>
        /// <param name="key">Data Source key</param>
        /// <param name="start">The starting row to get</param>
        /// <param name="length">The amount of rows to get. Make sure to return 1 extra row of data to signify that there is at least 1 more page of rows in the data source filter that can be viewed (e.g. length + 1 = 10 + 1 = 11 rows)</param>
        /// <param name="lang">The language to use when accessing your data source</param>
        /// <param name="filter">The filter to apply to the data source</param>
        /// <param name="orderBy">The columns used to sort the records by</param>
        /// <param name="childKeys">Specify which child data sets you'd like to query. If null, query all matching child data sets found in the relationships list</param>
        /// <returns>List of data, each item in the list contains a Dictionary of key/value pairs that represent the fields and associated data for a given row within the data source</returns>
        Dictionary<string, List<Dictionary<string, string>>> Filter(IRequest request, string key, string lang = "en", Dictionary<string, DataSource.PositionSettings> positions = null, Dictionary<string, List<DataSource.FilterGroup>> filters = null, Dictionary<string, List<DataSource.OrderBy>> orderBy = null, string[] childKeys = null);

        /// <summary>
        /// Returns total number of filtered records
        /// </summary>
        /// <param name="Request">Current request context</param>
        /// <param name="key">Data Source key</param>
        /// <param name="lang">The language to use when accessing your data source</param>
        /// <param name="filter">The filter to apply to the data source</param>
        /// <param name="orderBy">The columns used to sort the records by</param>
        /// <returns>List of data, each item in the list contains a Dictionary of key/value pairs that represent the fields and associated data for a given row within the data source</returns>
        int FilterTotal(IRequest request, string key, string lang = "en", List<DataSource.FilterGroup> filter = null);

        /// <summary>
        /// Returns a Dictionary of data source keys and associated total number of filtered records
        /// </summary>
        /// <param name="Request">Current request context</param>
        /// <param name="key">Data Source key</param>
        /// <param name="lang">The language to use when accessing your data source</param>
        /// <param name="filter">The filter to apply to the data source</param>
        /// <param name="orderBy">The columns used to sort the records by</param>
        /// <param name="childKeys">Specify which child data sets you'd like to query. If null, query all matching child data sets found in the relationships list</param>
        /// <returns>List of data, each item in the list contains a Dictionary of key/value pairs that represent the fields and associated data for a given row within the data source</returns>
        Dictionary<string, int> FilterTotal(IRequest request, string key, string lang = "en", Dictionary<string, List<DataSource.FilterGroup>> filters = null, string[] childKeys = null);
    }
}
