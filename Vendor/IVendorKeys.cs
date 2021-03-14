using Saber.Core;

namespace Saber.Vendor
{
    /// <summary>
    /// A list of security keys that is used by the vendor plugin. 
    /// These keys will be included in the list of security keys 
    /// displayed in Saber's security role manager UI.
    /// </summary>
    public interface IVendorKeys
    {
        /// <summary>
        /// Name of the vendor to display when listing
        /// security keys in Saber's security role manager UI.
        /// </summary>
        string Vendor { get; set; }
        SecurityKey[] Keys { get; set; }
    }
}
