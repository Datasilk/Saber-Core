﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Datasilk.Core.Web;

namespace Saber.Core
{
    public class Service : Request, IRequest, IService
    {
        protected StringBuilder Scripts = new StringBuilder();
        protected StringBuilder Css = new StringBuilder();
        protected List<string> Resources = new List<string>();
        protected bool IsPublicApiRequest { get; set; } = false;

        public EditorType EditorUsed
        {
            get { return EditorType.Monaco; }
        }

        protected IUser user;
        public virtual IUser User
        {
            get
            {
                return Delegates.Service.GetUser(this);
            }
            set { user = value; }
        }

        public virtual void Init()
        {
            
        }

        public override void Dispose()
        {
            base.Dispose();
            if (user != null)
            {
                User.Save();
            }
        }

        public string JsonResponse(dynamic obj)
        {
            Context.Response.ContentType = "text/json";
            return JsonSerializer.Serialize(obj);
        }

        protected string Response(string html)
        {
            return JsonResponse(new Response(html, Css.ToString() + Scripts.ToString()));
        }

        public virtual bool CheckSecurity(string key = "") 
        {
            return Delegates.Service.CheckSecurity(this, key);
        }

        public string Success()
        {
            return "success";
        }

        public string Empty() { return "{}"; }

        public string AccessDenied(string message = "Error 403")
        {
            Context.Response.StatusCode = 403;
            return message;
        }

        public string Error(string message = "Error 500")
        {
            Context.Response.StatusCode = 500;
            return message;
        }

        public string BadRequest(string message = "Bad Request 400")
        {
            Context.Response.StatusCode = 400;
            return message;
        }

        public void AddScript(string url, string id = "", string callback = "")
        {
            if (ContainsResource(url)) { return; }
            Scripts.Append("S.util.js.load('" + url + "', '" + id + "', " + (callback != "" ? callback : "null") + ");");
        }

        public void AddCSS(string url, string id = "")
        {
            if (ContainsResource(url)) { return; }
            Scripts.Append("S.util.css.load('" + url + "', '" + id + "');");
        }

        protected bool ContainsResource(string url)
        {
            if (Resources.Contains(url)) { return true; }
            Resources.Add(url);
            return false;
        }

        #region "Public API"

        private static List<Models.ApiKey> apikeys;

        public static List<Models.ApiKey> ApiKeys
        {
            set
            {
                //securely set the Public API key so that vendors do not have access to it
                apikeys = value;
            }
        }

        public static bool CheckApiKey(string key)
        {
            return apikeys.Any(a => a.Key == key);
        }

        public static string GetClientIDFromKey(string key)
        {
            return apikeys.Where(a => a.Key == key).FirstOrDefault()?.Client_ID ?? "";
        }

        #endregion
    }
}