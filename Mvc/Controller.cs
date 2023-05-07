using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datasilk.Core.Web;

namespace Saber.Core
{

    public abstract class Controller : Request, IRequest, IController
    {
        public StringBuilder Scripts { get; set; } = new StringBuilder();
        public StringBuilder Css { get; set; } = new StringBuilder();
        private List<string> Resources = new List<string>();
        public bool UsePlatform { get; set; } = true;
        public string Title { get; set; } = "Datasilk";
        public string Description { get; set; } = "";
        public string Theme { get; set; } = "dark";
        public StringBuilder Footer { get; set; } = new StringBuilder();

        public EditorType EditorUsed
        {
            get { return EditorType.Monaco; }
        }

        protected IUser user;
        public virtual IUser User { 
            get
            {
                if(user == null)
                {
                    user = Delegates.Controller.GetUser(this);
                }
                return user;
            }
            set { user = value; }
        }

        protected Session session;
        public Session Session
        {
            get
            {
                if (session == null)
                {
                    session = Session.Create(Context);
                }
                return session;
            }
            set
            {
                session = value;
            }
        }

        public virtual void Init() { }

        public override void Dispose()
        {
            base.Dispose();
            if(user != null)
            {
                User.Save();
            }
            Session.Dispose();
        }

        public virtual bool CheckSecurity(string key = "")
        {
            return Delegates.Controller.CheckSecurity(this, key);
        }

        public string AccessDenied<T>() where T : Datasilk.Core.Web.IController
        {
            return Datasilk.Core.Web.IController.AccessDenied<T>(this);
        }

        public virtual string AccessDenied() {
            throw new System.NotImplementedException();
        }

        public string Error<T>() where T : Datasilk.Core.Web.IController
        {
            Context.Response.StatusCode = 500;
            return Datasilk.Core.Web.IController.Error<T>(this);
        }

        public string Error(string message = "Error 500")
        {
            Context.Response.StatusCode = 500;
            return message;
        }

        public string Error404<T>() where T : Datasilk.Core.Web.IController
        {
            Context.Response.StatusCode = 404;
            return Datasilk.Core.Web.IController.Error404<T>(this);
        }

        public string Error404(string message = "Error 404")
        {
            Context.Response.StatusCode = 404;
            return message;
        }

        public string Redirect(string url)
        {
            Context.Response.Redirect(url);
            return "<script language=\"javascript\">window.location.href = '" + url + "';</script>";
        }

        public void AddScript(string url, string id = "", string callback = "")
        {
            if (ContainsResource(url)) { return; }
            Scripts.Append("<script language=\"javascript\"" + (id != "" ? " id=\"" + id + "\"" : "") + " src=\"" + url + (url.Contains("?") ? "&" : "?") + "v=" + App.Version + "\"" +
                (callback != "" ? " onload=\"" + callback + "\"" : "") + "></script>");
        }

        public void AddCSS(string url, string id = "")
        {
            if (ContainsResource(url)) { return; }
            Css.Append("<link rel=\"stylesheet\" type=\"text/css\"" + (id != "" ? " id=\"" + id + "\"" : "") + " href=\"" + url + (url.Contains("?") ? "&" : "?") + "v=" + App.Version + "\"></link>");
        }

        public bool ContainsResource(string url)
        {
            if (Resources.Contains(url)) { return true; }
            Resources.Add(url);
            return false;
        }

        string IRequest.AlterUrl(Dictionary<string, string> parameters)
        {
            var query = Context.Request.Query.Join(parameters, a => a.Key, a => a.Key, (a, b) => b).ToDictionary(a => a.Key, a => a.Value);
            return "/" + string.Join("/", PathParts) + (query.Keys.Count > 0 ? "?" + string.Join("&", query.Select(a => a.Key + "=" + a.Value)) : "");
        }

        public virtual string Render(string body = "") { return body; }

        public void AddScriptBlock(string javascript = "", string id = "")
        {
            if (ContainsResource(id)) { return; }
            Scripts.Append("<script language=\"javascript\"" + (id != "" ? " id=\"" + id + "\"" : "") + " >" + javascript + "</script>");
        }
    }
}