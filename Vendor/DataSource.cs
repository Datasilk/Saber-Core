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

        public enum DataType
        {
            Text = 0,
            Number = 1,
            Float = 2,
            DateTime = 3,
            Boolean = 4,
        }

        public enum FilterMatchType
        {
            StartsWith = 0,
            EndsWith = 1,
            Contains = 2,
            Equals = 3,
            GreaterThan = 4,
            GreaterEqualTo = 5,
            LessThan = 6,
            LessThanEqualTo = 7
        }

        [XmlRoot("groups")]
        public class FilterGroups: List<FilterGroup> {
            public FilterGroups(List<FilterGroup> group)
            {
                foreach(var g in group)
                {
                    Add(g);
                }
            }
        }

        [XmlRoot("group")]
        public class FilterGroup
        {
            [XmlElement("element")]
            public List<FilterElement> Elements { get; set; }
            [XmlElement("groups")]
            public List<FilterGroup> Groups { get; set; }
        }
        public class FilterElement
        {
            [XmlElement("column")]
            public string Column { get; set; }
            [XmlElement("match")]
            public FilterMatchType Match { get; set; }
            [XmlElement("value")]
            public string Value { get; set; }
            /// <summary>
            /// To map your filter to a request parameter (querystring or multi-part form data), 
            /// define the name for your querystring parameter.
            /// </summary>
            [XmlIgnore]
            public string QueryName { get; set; }
        }

        public enum OrderByDirection
        {
            Ascending = 0,
            Descending = 1
        }

        [XmlRoot("orderby")]
        public class OrderByList : List<OrderBy>
        {
            public OrderByList(List<OrderBy> orderby)
            {
                foreach (var o in orderby)
                {
                    Add(o);
                }
            }
        }

        [Serializable]
        [XmlRoot("sort")]
        public class OrderBy
        {
            [XmlElement("column")]
            public string Column { get; set; }
            [XmlElement("by")]
            public OrderByDirection Direction { get; set; }
        }

        public static string RenderFilters(IRequest request, DataSourceInfo datasource, List<FilterGroup> filters)
        {
            return Delegates.DataSources.RenderFilters(request, datasource, filters);
        }

        public static string RenderFilterGroups(IRequest request, DataSourceInfo datasource, List<FilterGroup> filters)
        {
            return Delegates.DataSources.RenderFilterGroups(request, datasource, filters);
        }
    }
}
