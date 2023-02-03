namespace Saber.Core
{
    public static class DataSources
    {
        /// <summary>
        /// Adds a new datasource at runtime. 
        /// </summary>
        /// <param name="datasource">Make sure to create a new instance of your Vendor's DataSources class
        /// and set datasource.Helper as the instance. Also, your datasource.Key should use your DataSources.Key
        /// as a prefix (with a dash to separate prefix from suffix), then use the datasource.Name 
        /// (replacing all spaces with underscores) as the Key's suffix.
        /// </param>
        public static void Add(DataSourceInfo datasource)
        {
            Delegates.DataSources.Add(datasource);
        }
    }
}
