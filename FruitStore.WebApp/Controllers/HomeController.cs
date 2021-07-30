using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

using FruitStore.DataAccess.Services;
using Hangfire;

namespace FruitStore.WebApp.Controllers
{
    [Filters.SessionFilter]
    public class HomeController : Controller
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IFruitSeller _fruitSeller;

        public HomeController(IDistributedCache distributedCache, IFruitSeller seller)
        {
            this._distributedCache = distributedCache;
            this._fruitSeller = seller;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllFruits()
        {
            List<Entity.Fruit> result = null;

            var cache = await _distributedCache.GetStringAsync("fruits");
            if (!string.IsNullOrWhiteSpace(cache))
            {
                result = JsonConvert.DeserializeObject<List<Entity.Fruit>>(cache);
            }
            else
            {
                result = _fruitSeller.GetFruits();

                var json = JsonConvert.SerializeObject(result);
                _distributedCache.SetString("fruits", json);
            }
            return Ok(result);
        }

        //[FromForm], [FromQuery],[FromBody],[FromHeader],[FromQuery],[FromRoute]
        public async Task<IActionResult> Create(Models.FruitModel model, [FromHeader(Name = "Accept-Language")] string lang)
        {
            if (!ModelState.IsValid)
            {
                return NoContent();
            }
            return Ok();
        }
    }
}