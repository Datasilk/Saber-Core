namespace Saber.Vendor
{
    /// <summary>
    /// Define a list of unique email messages that your vendor plugin will be sending out to Saber users
    /// </summary>
    public interface IVendorEmails
    {
        EmailType[] Types { get; set; }
    }
}


