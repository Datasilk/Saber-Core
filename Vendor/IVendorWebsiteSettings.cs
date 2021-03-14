using Saber.Core;

namespace Saber.Vendor
{
    /// <summary>
    /// Render an accordion within Saber's website settings tab so you can display settings for your vendor plugin.
    /// </summary>
    public interface IVendorWebsiteSettings
    {
        /// <summary>
        /// Human-readable name for the accordion that will be displayed in the website settings tab within Saber.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Used to render website settings for your plugin inside an accordion when an administrator requests 
        /// to view their website settings tab.
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        string Render(IRequest Request);
    }
}
