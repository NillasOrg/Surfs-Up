using Surfs_Up.Models;

namespace Surfs_Up.ViewModels
{
    public class AdminDashboard
    {
        public List<Booking> Bookings { get; set; }
        public List<APIRequestLog> ApiRequestLogs { get; set; }
    }
}
