namespace Clothsy.DTOs.Web.Hub
{
    public class HubDashboardDto
    {
        public int PendingApprovals { get; set; }
        public int ApprovedItems { get; set; }
        public int ActiveRequests { get; set; }
        public int TotalDistributed { get; set; }
    }
}
