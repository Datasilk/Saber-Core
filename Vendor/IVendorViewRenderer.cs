using Saber.Core;

namespace Saber.Vendor
{
    public interface IVendorViewRenderer
    {
        string Render(IRequest request, View view);
    }
}
