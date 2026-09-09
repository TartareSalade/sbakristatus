using System.Net;

namespace sbkrstatus;

public class MonitorResult
{

    public string Url { get; set; }
    public MonitorStatus Status { get; set; }
    public HttpStatusCode? HttpStatusCode { get; set; }
    public long ResponseTimeMs { get; set; }
    public string ErrorMessage { get; set; }

    public MonitorResult(string url, MonitorStatus status, long responseTimeMs)
    {
        this.Url = url;
        this.Status = status;
        this.HttpStatusCode = null;
        this.ResponseTimeMs = responseTimeMs;
        this.ErrorMessage = null;
    }

}