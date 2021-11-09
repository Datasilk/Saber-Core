namespace Saber.Vendor
{
    public class DataSource
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public Column[] Columns { get; set; }

        public class Column
        {
            public string Name { get; set; }
            public DataType DataType { get; set; }
        }

        public enum DataType
        {
            Text = 0,
            Number = 1,
            Float = 2,
            DateTime = 3,
            Boolean = 4,
        }
    }
}
