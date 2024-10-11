namespace SendReportsCompany.Models
{
    public class ReportRequestModel
    {
        Guid ReportId { get; set; }
        string Provider { get; set; } // "email" or "fax"
        string Target { get; set; }
    }
}
