using System;
using System.Collections.Generic;

namespace Saber.Vendor
{
    public abstract class NotificationType
    {
        /// <summary>
        /// An unique 8 character string that represents the type of notification being displayed
        /// </summary>
        public abstract string Type { get; set; }
        
        /// <summary>
        /// The name of the icon to use for this notification (e.g. "icon-users")
        /// </summary>
        public abstract string Icon { get; set; }

        /// <summary>
        /// Used to render the notification
        /// </summary>
        /// <param name="notification">Details about the notification</param>
        /// <param name="defaultView">The generic notification item view</param>
        /// <returns></returns>
        public virtual string Render(View defaultView, Guid id, string text, string url, DateTime? dateCreated = null)
        {
            defaultView["url"] = url;
            defaultView["icon"] = Icon;
            defaultView["notification"] = text;
            return defaultView.Render();
        }

        /// <summary>
        /// Generate a list of dynamic notifications that aren't saved in the database,
        /// such as notifications related to tasks that the user needs to complete in order
        /// for the plugin to work properly
        /// </summary>
        /// <returns></returns>
        public virtual Models.Notification[] GetDynamicList(Core.IUser user)
        {
            var list = new List<Models.Notification>();
            return list.ToArray();
        }

        /// <summary>
        /// Creates a new notification in the database using the associated notification type
        /// </summary>
        /// <param name="text">Text body of the notification</param>
        /// <param name="url">URL to navigate to when the user clicks the notification. Typically uses a "javascript:" URL so that the user is not directed away from the Saber Editor interface</param>
        /// <param name="userId">Optional. Used when the notification is created for a specific user to see.</param>
        /// <param name="groupId">Optional. Used when the notification is created for a specific security group to see.</param>
        /// <param name="securityKey">Optional. Used when the notification is created for anyone who has access to a specific security key to see.</param>
        public void CreateNotification(string text, string url, int? userId = null, int? groupId = null, string securityKey = "")
        {
            Core.Delegates.Notifications.CreateNotification(text, url, Type, userId, groupId, securityKey);
        }
    }
}
