using System.Net;

namespace sbkrstatus;

public class MonitorResult
{

    public string Url { get; set; }
    public MonitorStatus Status { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }
    public TimeSpan ResponseTimeMs { get; set; }
    public string ErrorMssage { get; set; }

    public MonitorResult(string url, MonitorStatus status, HttpStatusCode statusCode, TimeSpan responseTimeMs,
        string errorMssage)
    {
        this.Url = url;
        this.Status = status;
        this.HttpStatusCode = statusCode;
        this.ResponseTimeMs = responseTimeMs;
        this.ErrorMssage = errorMssage;
    }

}