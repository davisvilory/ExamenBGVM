using ExamenBGVM.Models;
using Newtonsoft.Json;

namespace ExamenBGVM.Business
{
    public class BusinessEntries
    {
        private static ILogger<BusinessEntries> _logger = Startup.LogFactory.CreateLogger<BusinessEntries>();

        public static async Task<Entries> ReadEntries(string url)
        {
            var entries = new Entries();
            try
            {
                using var httpClient = new HttpClient();
                using var response = await httpClient.GetAsync(url + "/entries");
                string apiResponse = await response.Content.ReadAsStringAsync();
                entries = JsonConvert.DeserializeObject<Entries>(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error al consumir el servicio entries: {ex}");
            }
            return entries ?? new Entries();
        }

        public static async Task<Categories> ReadCategories(string url)
        {
            var entries = new Categories();
            try
            {
                using var httpClient = new HttpClient();
                using var response = await httpClient.GetAsync(url + "/categories");
                string apiResponse = await response.Content.ReadAsStringAsync();
                entries = JsonConvert.DeserializeObject<Categories>(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error al consumir el servicio categories: {ex}");
            }
            return entries ?? new Categories();
        }

        public static async Task<Entries> ReadRandom(string url)
        {
            var entries = new Entries();
            try
            {
                using var httpClient = new HttpClient();
                using var response = await httpClient.GetAsync(url + "/random");
                string apiResponse = await response.Content.ReadAsStringAsync();
                entries = JsonConvert.DeserializeObject<Entries>(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error al consumir el servicio random: {ex}");
            }
            return entries ?? new Entries();
        }

        internal static class ApplicationLogging
        {
            internal static ILoggerFactory LoggerFactory { get; set; }// = new LoggerFactory();
            internal static ILogger CreateLogger<T>() => LoggerFactory.CreateLogger<T>();
            internal static ILogger CreateLogger(string categoryName) => LoggerFactory.CreateLogger(categoryName);
        }
    }
}
