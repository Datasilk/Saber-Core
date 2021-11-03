namespace Saber.Models
{
    public class ApiKey
    {
        public string Client_ID { get; set; }
        public string Key { get; set; }
        public int? UserId { get; set; }
        public string Host { get; set; }
        public string Redirect_URI { get; set; }
    }
}
