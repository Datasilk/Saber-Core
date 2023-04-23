namespace Saber.Core.Vendor
{
    /// <summary>
    /// A list of notification types that your plugin will utilize
    /// </summary>
    public interface IVendorNotificationTypes
    {

        NotificationType[] NotificationType { get; set; }
    }
}
