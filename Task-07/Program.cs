using System;
using System.Threading.Tasks;

namespace Task07
{
    class Program
    {
        // Simulates fetching data from an API source
        static async Task<string> FetchDataAsync(string source, int delay)
        {
            Console.WriteLine($"Fetching data from {source}...");
            await Task.Delay(delay); // Simulate delay
            return $"{source} responded after {delay}ms";
        }

        static async Task Main()
        {
            Console.WriteLine("Starting data fetch from multiple APIs...\n");

            try
            {
                var fetchTasks = new Task<string>[]
                {
                    FetchDataAsync("Source 1", 1500),
                    FetchDataAsync("Source 2", 2000),
                    FetchDataAsync("Source 3", 3000)
                };

                // Wait for all tasks to complete
                string[] results = await Task.WhenAll(fetchTasks);

                Console.WriteLine("\nAll tasks completed. Results:");
                foreach (var result in results)
                {
                    Console.WriteLine(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }

            Console.WriteLine("\nDone.");
        }
    }
}
