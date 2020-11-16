namespace Saber.Core
{
    public interface IRequest: Datasilk.Core.Web.IRequest
    {
        IUser User { get; set; }
        bool CheckSecurity(string key = "");
        void AddScript(string url, string id = "", string callback = "");
        void AddCSS(string url, string id = "");
    }
}
