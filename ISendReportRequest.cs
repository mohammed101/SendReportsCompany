namespace SendReportsCompany
{
    public interface ISendReportRequest
    {
        Guid ReportId { get; set; }
        string Provider { get; set; } // "email" or "fax"
        string Target { get; set; }
    }
}
