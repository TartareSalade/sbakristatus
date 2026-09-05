using System.Diagnostics;

namespace sbkrstatus;

// Le client http doit être instancier une fois par appli au lieu de chauqe usage d'ou le static
class Program
{
    static readonly HttpClient client = new HttpClient();
    static async Task Main(string[] args)
    {
        
        try
        {
            var response = await client.GetAsync("http://www.google.com");
            response.EnsureSuccessStatusCode(); // interet du try catch si execption on le catch (status != 2xx)
            string responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine(response.StatusCode);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught !");
            Console.WriteLine(e.StatusCode);
            Console.WriteLine(e.Message);
        }
    }
}