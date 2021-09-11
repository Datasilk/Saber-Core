using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Saber.Core
{
    public class Session : IDisposable
    {
        public static int ExpiresInMinutes { get; set; } = 20;
        public static string CookieName { get; set; } = "Saber";
        private HttpContext _context { get; set; }
        private string _key { get; set; }
        private Dictionary<string, string> _data { get; set; }
        private bool _changed { get; set; }  = false;
        private bool _tryget { get; set; } = false;
        private bool _isnew { get; set; } = false;

        public static Session Create(HttpContext context)
        {
            var key = context.Request.Cookies.ContainsKey(CookieName) ? 
                context.Request.Cookies[CookieName] : "";
            return new Session(context, key);
        }

        public Session(HttpContext context, string key = "")
        {
            _context = context;
            if(key == "")
            {
                //generate new key
                _key = NewId(64);
            }
            else
            {
                _key = key;
            }
        }

        public string Get(string key)
        {
            if(_data == null)
            {
                //load data from SQL
                if(_tryget == false)
                {
                    try
                    {
                        _data = Delegates.Session.Get(_key, ExpiresInMinutes);
                    }
                    catch (Exception) { }
                    _tryget = true;
                }
            }
            if (_data != null && _data.ContainsKey(key))
            {
                return _data[key];
            }
            return null;
        }

        public void Set(string key, string value)
        {
            //load data from SQL
            if (_data == null)
            {
                if (_tryget == false)
                {
                    try
                    {
                        _data = Delegates.Session.Get(_key, ExpiresInMinutes);
                    }
                    catch (Exception) { }
                    _tryget = true;
                }
                if(_data == null){
                    _data = new Dictionary<string, string>();
                    _isnew = true;
                }
            }
            if (_data.ContainsKey(key))
            {
                if(_data[key] != value)
                {
                    _data[key] = value;
                    _changed = true;
                }
            }
            else
            {
                _data.Add(key, value);
                _changed = true;
            }
        }

        public void Remove(string key)
        {
            if (_data.ContainsKey(key)) 
            { 
                _data.Remove(key);
            }
        }

        public void Dispose()
        {
            if(_changed == true)
            {
                //save session to SQL
                Delegates.Session.Set(_key, Serialize, ExpiresInMinutes);

                //update cookie
                if (_isnew)
                {
                    var options = new CookieOptions()
                    {
                        Expires = DateTime.Now.AddMinutes(ExpiresInMinutes)
                    };
                    _context.Response.Cookies.Append(CookieName, _key, options);
                }
            }
        }

        private string Serialize
        {
            get
            {
                return JsonSerializer.Serialize(_data).Replace("\\u0022", "\\\"");
            }
        }

        private static string NewId(int length = 3)
        {
            string result = "";
            for (var x = 0; x <= length - 1; x++)
            {
                int type = new Random().Next(1, 3);
                int num;
                switch (type)
                {
                    case 1: //a-z
                        num = new Random().Next(0, 26);
                        result += (char)('a' + num);
                        break;

                    case 2: //A-Z
                        num = new Random().Next(0, 26);
                        result += (char)('A' + num);
                        break;

                    case 3: //0-9
                        num = new Random().Next(0, 9);
                        result += (char)('1' + num);
                        break;

                }

            }
            return result;
        }
    }
}
