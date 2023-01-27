using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Saber.Vendor
{
    /// <summary>
    /// Define policies for CORS headers
    /// </summary>
    public interface IVendorCorsPolicy
    {
        /// <summary>
        /// Add CORS options when configuring services
        /// </summary>
        /// <param name="builder"></param>
        void AddCorsOptions(CorsOptions options);

        /// <summary>
        /// Define CORS policies for cross-domain communication
        /// </summary>
        /// <param name="builder"></param>
        void ApplyCorsPolicies(CorsPolicyBuilder builder);
    }
}


