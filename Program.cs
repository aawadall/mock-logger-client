using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;


namespace exp11
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static Random random = new Random();
        static void Main(string[] args)
        {
            client.BaseAddress = new Uri(Defaults.BaseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            for (int i = 0; i < 1000; i++)
            {
                RunAsync().GetAwaiter().GetResult();
                Thread.Sleep(random.Next(1000));
            }
        }


        static async Task<bool> Log(string path, string payload)
        {
            System.Console.WriteLine("PAYLOAD:");
            System.Console.WriteLine(payload);
            HttpResponseMessage response = await client.PostAsync(path, new StringContent(payload, System.Text.Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode) return true;
            return false;
        }
        static async Task<string> GetHealthAsync(string path)
        {
            string health = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
                Task.Run(() => health = response.Content.ReadAsStringAsync().Result).Wait();

            return health;
        }
        static async Task RunAsync()
        {


            try
            {
                await Log(Endpoints.Logs, LogEntryExtensions.MakeLogEntry().Serialize());
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }

    internal class Endpoints
    {
        public static string Health = "/api/health";
        public static string Logs = "/api/logs";
    }
}
