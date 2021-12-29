using Microsoft.AspNetCore.Routing;

namespace Saber.Vendor
{
    public interface IVendorSignalR
    {
        void RegisterHubs(IEndpointRouteBuilder endpoints);
    }
}
