using System;
using System.Collections.Generic;
using Saber.Vendor;

namespace Saber.Core
{
    public static class Vendors
    {
        public static List<VendorInfo> Details { get; set; } = new List<VendorInfo>();
        public static Dictionary<string, List<IVendorViewRenderer>> ViewRenderers { get; set; } = new Dictionary<string, List<IVendorViewRenderer>>();
        public static Dictionary<string, Models.VendorContentFieldInfo> ContentFields { get; set; } = new Dictionary<string, Models.VendorContentFieldInfo>();
        public static Dictionary<string, Type> Controllers { get; set; } = new Dictionary<string, Type>();
        public static Dictionary<string, Type> Services { get; set; } = new Dictionary<string, Type>();
        public static Dictionary<string, Type> Startups { get; set; } = new Dictionary<string, Type>();
        public static List<IVendorKeys> Keys { get; set; } = new List<IVendorKeys>();
        public static Dictionary<string, HtmlComponentModel> HtmlComponents { get; set; } = new Dictionary<string, HtmlComponentModel>();
        public static string[] HtmlComponentKeys { get; set; }
        public static Dictionary<string, HtmlComponentModel> SpecialVars { get; set; } = new Dictionary<string, HtmlComponentModel>();
        public static Dictionary<string, IVendorEmailClient> EmailClients { get; set; } = new Dictionary<string, IVendorEmailClient>();
        public static Dictionary<string, EmailType> EmailTypes { get; set; } = new Dictionary<string, EmailType>();
        public static List<IVendorWebsiteSettings> WebsiteSettings { get; set; } = new List<IVendorWebsiteSettings>();
        public static List<DataSourceInfo> DataSources { get; set; } = new List<DataSourceInfo>();
        public static List<SaberEvents> EventHandlers { get; set; } = new List<SaberEvents>();
        public static List<InternalApi> InternalApis { get; set; } = new List<InternalApi>();
        public static List<IVendorSignalR> SignalR { get; set; } = new List<IVendorSignalR>();
        public static List<IVendorCorsPolicy> CorsPolicies { get; set; } = new List<IVendorCorsPolicy>();
    }

    public class VendorInfo : IVendorInfo
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public Vendor.Version Version { get; set; }
        public string DLL { get; set; }
        public string Assembly { get; set; }
        public string Path { get; set; }
        public Type Type { get; set; }
        public Dictionary<string, List<IVendorViewRenderer>> ViewRenderers { get; set; } = new Dictionary<string, List<IVendorViewRenderer>>();
        public Dictionary<string, Models.VendorContentFieldInfo> ContentFields { get; set; } = new Dictionary<string, Models.VendorContentFieldInfo>();
        public Dictionary<string, Type> Controllers { get; set; } = new Dictionary<string, Type>();
        public Dictionary<string, Type> Services { get; set; } = new Dictionary<string, Type>();
        public Dictionary<string, Type> Startups { get; set; } = new Dictionary<string, Type>();
        public List<IVendorKeys> Keys { get; set; } = new List<IVendorKeys>();
        public Dictionary<string, HtmlComponentModel> HtmlComponents { get; set; } = new Dictionary<string, HtmlComponentModel>();
        public string[] HtmlComponentKeys { get; set; }
        public Dictionary<string, HtmlComponentModel> SpecialVars { get; set; } = new Dictionary<string, HtmlComponentModel>();
        public Dictionary<string, IVendorEmailClient> EmailClients { get; set; } = new Dictionary<string, IVendorEmailClient>();
        public Dictionary<string, EmailType> EmailTypes { get; set; } = new Dictionary<string, EmailType>();
        public List<IVendorWebsiteSettings> WebsiteSettings { get; set; } = new List<IVendorWebsiteSettings>();
        public List<Models.PublicApiInfo> PublicApis { get; set; } = new List<Models.PublicApiInfo>();
        public List<IVendorSignalR> SignalR { get; set; } = new List<IVendorSignalR>();
        public List<IVendorCorsPolicy> CorsPolicies { get; set; } = new List<IVendorCorsPolicy>();
    }

    public class DataSourceInfo
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IVendorDataSources Helper { get; set; }
    }
}
