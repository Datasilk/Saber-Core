using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text.Json;
using Saber.Core;
using System.Text.Json.Serialization;

namespace Saber.Vendor
{
    public class DataSource
    {
        public string Key { get; set; } = "";
        public string Name { get; set; } = "";
        public Column[] Columns { get; set; } = new Column[0];
        public Relationship[] Relationships { get; set; } = new Relationship[0];

        public class Column
        {
            public string Name { get; set; } = "";
            public DataType DataType { get; set; } = DataType.Text;
        }

        public class Relationship
        {
            /// <summary>
            /// The parent Data Source key
            /// </summary>
            public string Key { get; set; } = "";

            public string ChildKey { get; set; } = "";

            public DataSource Child { get; set; } = new DataSource();
            /// <summary>
            /// Name of the List Component found in the Parent HTML View
            /// </summary>
            public string ListComponent { get; set; } = "";
            /// <summary>
            /// The column name in the child Data Source that is used to store the related parent ID
            /// </summary>
            public string ChildColumn { get; set; } = "";

            public string ParentTable { get; set; } = "";
        }

        [Serializable]
        public enum DataType
        {
            [XmlEnum("0")]
            Text = 0,
            [XmlEnum("1")]
            Number = 1,
            [XmlEnum("2")]
            Float = 2,
            [XmlEnum("3")]
            DateTime = 3,
            [XmlEnum("4")]
            Boolean = 4,
        }

        [Serializable]
        public enum FilterMatchType
        {
            [XmlEnum("0")]
            StartsWith = 0,
            [XmlEnum("1")]
            EndsWith = 1,
            [XmlEnum("2")]
            Contains = 2,
            [XmlEnum("3")]
            Equals = 3,
            [XmlEnum("4")]
            GreaterThan = 4,
            [XmlEnum("5")]
            GreaterEqualTo = 5,
            [XmlEnum("6")]
            LessThan = 6,
            [XmlEnum("7")]
            LessThanEqualTo = 7
        }

        [Serializable]
        public enum GroupMatchType
        {
            [XmlEnum("0")]
            All = 0,
            [XmlEnum("1")]
            Any = 1
        }

        /// <summary>
        /// Use to serialize groups into XML for SQL
        /// </summary>
        [Serializable]
        [XmlRoot("groups")]
        public class XmlFilterGroups
        {
            [XmlElement("group")]
            public List<FilterGroup> Group { get; set; } = new List<FilterGroup>();
        }

        [Serializable]
        [XmlRoot("group")]
        public class FilterGroup
        {
            [XmlElement("element")]
            [JsonPropertyName("e")]
            public List<FilterElement> Elements { get; set; } = new List<FilterElement>();
            [XmlElement("groups")]
            [JsonPropertyName("g")]
            public List<FilterGroup> Groups { get; set; } = new List<FilterGroup>();
            [XmlAttribute("match")]
            [JsonPropertyName("m")]
            public GroupMatchType Match { get; set; } = GroupMatchType.Any;
        }
        public class FilterElement
        {
            [XmlAttribute("column")]
            [JsonPropertyName("c")]
            public string Column { get; set; } = "";
            [XmlAttribute("match")]
            [JsonPropertyName("m")]
            public FilterMatchType Match { get; set; } = FilterMatchType.Contains;
            [XmlAttribute("value")]
            [JsonPropertyName("v")]
            public string Value { get; set; } = "";
            /// <summary>
            /// To map your filter to a request parameter (querystring or multi-part form data), 
            /// define the name for your querystring parameter.
            /// </summary>
            [XmlAttribute("queryname")]
            [JsonPropertyName("qn")]
            public string QueryName { get; set; } = "";
        }

        [Serializable]
        public enum OrderByDirection
        {
            Ascending = 0,
            Descending = 1
        }

        /// <summary>
        /// Use to serialize orderby into XML for SQL
        /// </summary>
        [XmlRoot("orderby")]
        public class OrderByList
        {
            [XmlElement("sort")]
            [JsonPropertyName("o")]
            public List<OrderBy> OrderBy { get; set; } = new List<OrderBy>();
        }

        [Serializable]
        [XmlRoot("sort")]
        public class OrderBy
        {
            [XmlAttribute("column")]
            [JsonPropertyName("c")]
            public string Column { get; set; } = "";
            [XmlAttribute("by")]
            [JsonPropertyName("d")]
            public OrderByDirection Direction { get; set; } = OrderByDirection.Ascending;
        }

        public class PositionSettings
        {
            [JsonPropertyName("s")]
            public int Start { get; set; } = 0;
            [JsonPropertyName("sq")]
            public string StartQuery { get; set; } = "";
            [JsonPropertyName("l")]
            public int Length { get; set; } = 10;
            [JsonPropertyName("lq")]
            public string LengthQuery { get; set; } = "";
            [JsonPropertyName("p")]
            public bool Paging { get; set; } = true;
        }

        public static string RenderFilters(IRequest request, DataSourceInfo datasource, List<FilterGroup> filters)
        {
            return Delegates.DataSources.RenderFilters(request, datasource, filters);
        }

        public static string RenderFilterGroups(IRequest request, DataSourceInfo datasource, List<FilterGroup> filters, int depth = 0)
        {
            return Delegates.DataSources.RenderFilterGroups(request, datasource, filters, depth);
        }

        public static string RenderFilter(IRequest request, DataSource datasource, FilterElement filter)
        {
            return Delegates.DataSources.RenderFilter(request, datasource, filter);
        }

        public static string RenderOrderBy(OrderBy orderby)
        {
            return Delegates.DataSources.RenderOrderBy(orderby);
        }

        public static string RenderOrderByList(DataSourceInfo datasource, List<OrderBy> orderbyList)
        {
            return Delegates.DataSources.RenderOrderByList(datasource, orderbyList);
        }

        public static string RenderPositionSettings(DataSourceInfo datasource, PositionSettings settings)
        {
            return Delegates.DataSources.RenderPositionSettings(datasource, settings);
        }
    }
}
