using ExamenBGVM.Business;
using ExamenBGVM.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExamenBGVM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EntriesController : ControllerBase
    {
        private readonly ILogger<EntriesController> _logger;
        private string url;

        public EntriesController(ILogger<EntriesController> logger, IConfiguration iConfig)
        {
            _logger = logger;
            url = iConfig.GetSection("URLExterna").Value;
        }

        [HttpGet("~/GetAllEntries")]
        public Entries GetAll()
        {
            try
            {
                _logger.LogDebug("Obteniendo registros de la liga https://api.publicapis.org/entries");
                return BusinessEntries.ReadEntries(url).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo obtener los registros: {ex}");
                return new Entries();
            }
        }

        [HttpPost("~/GetByHttps")]
        public Entries GetByHttps(bool https)
        {
            try
            {
                _logger.LogDebug("Obteniendo registros de la liga https://api.publicapis.org/entries por el filtro https");
                var ent = BusinessEntries.ReadEntries(url).Result;
                var filter = ent.entries.Where(z => z.HTTPS == https);
                ent.count = filter.Count();
                ent.entries = filter.ToList();
                return ent;
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo obtener los registros: {ex}");
                return new Entries();
            }
        }

        [HttpGet("~/GetCategories")]
        public Categories GetByCategories()
        {
            try
            {
                _logger.LogDebug("Obteniendo registros de la liga https://api.publicapis.org/categories por categorias");
                return BusinessEntries.ReadCategories(url).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo obtener los registros: {ex}");
                return new Categories();
            }
        }

        [HttpPost("~/GetByFilters")]
        public Entries GetByFilters(SearchEntries search)
        {
            try
            {
                _logger.LogDebug("Obteniendo registros de la liga https://api.publicapis.org/entries");
                var ent = BusinessEntries.ReadEntries(url).Result;
                IEnumerable<ItemEntry> items = ent.entries;
                if (!string.IsNullOrWhiteSpace(search.api))
                    items = items.Where(z => z.API != null ? z.API.Contains(search.api) : true);
                if (!string.IsNullOrWhiteSpace(search.description))
                    items = items.Where(z => z.Description != null ? z.Description.Contains(search.description) : true);
                if (!string.IsNullOrWhiteSpace(search.auth))
                    items = items.Where(z => z.Auth != null ? z.Auth.Contains(search.auth) : true);
                items = items.Where(z => z.HTTPS == search.https);
                if (!string.IsNullOrWhiteSpace(search.cors))
                    items = items.Where(z => z.Cors != null ? z.Cors.Contains(search.cors) : true);
                if (!string.IsNullOrWhiteSpace(search.link))
                    items = items.Where(z => z.Link != null ? z.Link.Contains(search.link) : true);
                if (!string.IsNullOrWhiteSpace(search.category))
                    items = items.Where(z => z.Category != null ? z.Category.Contains(search.category) : true);
                ent.entries = items.ToList();
                ent.count = ent.entries.Count;
                return ent;
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo obtener los registros: {ex}");
                return new Entries();
            }
        }
    }
}