using System;

namespace Saber.Models
{
    public class Notification
    {
        public Guid NotifId { get; set; }
        public DateTime DateCreated { get; set; }
        public int? UserId { get; set; }
        public int? GroupId { get; set; }
        public string SecurityKey { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
    }
}
