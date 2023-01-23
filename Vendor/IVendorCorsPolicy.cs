using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Saber.Vendor
{
    /// <summary>
    /// Define policies for CORS headers
    /// </summary>
    public interface IVendorCorsPolicy
    {
        /// <summary>
        /// Define CORS policies for cross-domain communication
        /// </summary>
        /// <param name="builder"></param>
        void ApplyCorsPolicies(CorsPolicyBuilder builder);
    }
}


