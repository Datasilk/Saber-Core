using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Saber.Core;

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
            public List<FilterGroup> Group { get; set; }
        }

        [Serializable]
        [XmlRoot("group")]
        public class FilterGroup
        {
            [XmlElement("element")]
            public List<FilterElement> Elements { get; set; }
            [XmlElement("groups")]
            public List<FilterGroup> Groups { get; set; }
            [XmlAttribute("match")]
            public GroupMatchType Match { get; set; }
        }
        public class FilterElement
        {
            [XmlAttribute("column")]
            public string Column { get; set; }
            [XmlAttribute("match")]
            public FilterMatchType Match { get; set; }
            [XmlAttribute("value")]
            public string Value { get; set; }
            /// <summary>
            /// To map your filter to a request parameter (querystring or multi-part form data), 
            /// define the name for your querystring parameter.
            /// </summary>
            [XmlIgnore]
            public string QueryName { get; set; }
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
            public List<OrderBy> OrderBy { get; set; }
        }

        [Serializable]
        [XmlRoot("sort")]
        public class OrderBy
        {
            [XmlAttribute("column")]
            public string Column { get; set; }
            [XmlAttribute("by")]
            public OrderByDirection Direction { get; set; }
        }

        public class PositionSettings
        {
            public int Start { get; set; }
            public string StartQuery { get; set; }
            public int Length { get; set; }
            public string LengthQuery { get; set; }
            public bool Paging { get; set; }
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
