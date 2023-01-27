
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Saber.Vendor
{
    /// <summary>
    /// Intercept the response before the page is finished rendering in order to modify response headers
    /// </summary>
    public interface IVendorPageResponse
    {
        /// <summary>
        /// Intercept the response before the page is finished rendering in order to modify response headers
        /// </summary>
        void Intercept(Core.IRequest request, HttpResponse response);
    }
}


