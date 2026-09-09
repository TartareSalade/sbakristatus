using System.Diagnostics;
using System.Net;

namespace sbkrstatus;

// Le client http doit être instancier une fois par appli au lieu de chauqe usage d'ou le static

/*  200-299 : UP
 *  300-399 : UP (redireciton)
 *  400-499 : DEGRADED Serveur joignable mais problème coté requête / acces
 *  500-599: DOWN
 *  Timeout : DOWN 
 *  DNS Error : DOWN
 *  Connection refused : DOWN
 * 
 * 
 */
class Program
{
    static readonly HttpClient client = new HttpClient();
    static async Task Main(string[] args)
    {
        var result = await Checkurlsync("https://www.google.com");
        DisplayResult(result);
        
    }

    public static async Task<MonitorResult> Checkurlsync(string url)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            var response = await client.GetAsync(url);
            // On récupere le status code au lieu de ensureSuccessStatusCode pour pouvoir gérer les codes 4xx et 5xx
            var statusCode = response.StatusCode;
            var responseTime = stopwatch.Elapsed;
            MonitorStatus status = GetMonitorStatus(statusCode);
            return new MonitorResult(url, status, (long)responseTime.TotalMilliseconds)
            {
                HttpStatusCode = statusCode
            };

        }
        
        
        catch (HttpRequestException e)
        {
            var responseTime = stopwatch.Elapsed;
            // Gestion des exceptions de requête HTTP
            Console.WriteLine("\nException Caught !");
            Console.WriteLine(e.StatusCode);
            Console.WriteLine(e.Message);
            return new MonitorResult(url, MonitorStatus.DOWN, 0)
            {
                ErrorMessage = e.Message
            };
        }
        finally
        {
            stopwatch.Stop();
        }
    }

    public static MonitorStatus GetMonitorStatus(HttpStatusCode statusCode)
    {
        MonitorStatus status;
        if ((int)statusCode >= 200 && (int)statusCode <= 299)
        {
            status = MonitorStatus.UP;
        }
        else if ((int)statusCode >= 300 && (int)statusCode <= 399)
        {
            status = MonitorStatus.UP;
        }
        else if ((int)statusCode >= 400 && (int)statusCode <= 499)
        {
            status = MonitorStatus.DEGRAGED;
        }
        else
        {
            status = MonitorStatus.DOWN;
        }

        return status;
    }
    
    public static void DisplayResult(MonitorResult result)
    {
        Console.WriteLine($"Url: {result.Url}");
        Console.WriteLine($"Status: {result.Status}");
        Console.WriteLine($"HttpStatusCode: {result.HttpStatusCode}");
        Console.WriteLine($"ResponseTimeMs: {result.ResponseTimeMs} ms");
        if (result.ErrorMessage != null)
        {
            Console.WriteLine($"ErrorMessage: {result.ErrorMessage}");
        }
    }
}