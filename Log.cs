using System;

namespace Saber.Core
{
    public static class Log
    {
        /// <summary>
        /// Log an error into the Saber Sql Server database
        /// </summary>
        /// <param name="ex">The provided Exception object</param>
        /// <param name="request">The request this error belongs to</param>
        /// <param name="area">The area in which the error happened, such as a class name or user action</param>
        /// <param name="data">Any relevant data that should be traced</param>
        public static void Error(Exception ex = null, IRequest request = null, string area = "", string data = "")
        {
            Delegates.Log.Error(request?.User.UserId ?? 0, request?.Path ?? "", area, ex?.Message ?? "", ex?.StackTrace ?? "", data);
        }
    }
}
