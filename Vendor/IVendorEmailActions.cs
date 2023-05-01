namespace Saber.Vendor
{
    /// <summary>
    /// Define a list of unique email actions that your vendor plugin will be sending out to Saber users
    /// </summary>
    public interface IVendorEmailActions
    {
        EmailAction[] Actions { get; set; }
    }
}


