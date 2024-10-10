namespace Surfs_Up.Models
{
    public class APIRequestLog
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public int SuccessfulRequests { get; set; }
        public int FailedRequests { get; set; }

        //public User User { get; set; }
    }
}
