using System.Diagnostics;

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
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            var response = await client.GetAsync("https://gitbbbbb.sbkr.fr/");
            response.EnsureSuccessStatusCode(); // interet du try catch si execption on le catch (status != 2xx)
            string responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine(response.StatusCode);
            Console.WriteLine("Temps de la requete : " + stopwatch.ElapsedMilliseconds + " ms");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught !");
            Console.WriteLine(e.StatusCode);
            Console.WriteLine(e.Message);
        }
        finally
        {
            stopwatch.Stop();
        }
    }

    public string MakeRequest(string url)
    {
        try
        {
            var response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            return response.StatusCode.ToString();
        }
        catch (HttpRequestException e)
        {
            return e.StatusCode.ToString();
        }
    }
    
}