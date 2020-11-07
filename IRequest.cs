namespace Saber.Core
{
    public interface IRequest: Datasilk.Core.Web.IRequest
    {
        IUser User { get; set; }
        bool CheckSecurity();
        void AddScript(string url, string id = "", string callback = "");
        void AddCSS(string url, string id = "");
    }
}
