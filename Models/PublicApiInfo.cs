using System.Collections.Generic;

namespace Saber.Models
{
    public class PublicApiInfo
    {
        public string Path { get; set; } // e.g. "api/Service/Method"
        public string Description { get; set; }
        public List<PublicApiArgumentInfo> Parameters { get; set; } = new List<PublicApiArgumentInfo>();
        public bool Enabled { get; set; }
    }

    public class PublicApiArgumentInfo
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public string DataType { get; set; }
        public string Description { get; set; }
    }
}
