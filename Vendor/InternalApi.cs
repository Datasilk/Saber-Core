using System;
using System.Collections.Generic;
using System.Linq;
using Saber.Core;

namespace Saber.Vendor
{
    public class InternalApi
    {
        public string Key { get; set; }
        public Func<IRequest, Dictionary<string, object>, string> Method { get; set; }

        public static string Call(string key, IRequest request, Dictionary<string, object> data)
        {
            var api = Vendors.InternalApis.Where(a => a.Key == key).FirstOrDefault();
            if(api != null)
            {
                return api.Method(request, data);
            }
            return "";
        }
    }
}
