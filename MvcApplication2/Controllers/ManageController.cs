using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using SimGame.Data;
using SimGame.Website.Models;

namespace SimGame.Website.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        //
        // GET: /Manage/

        private readonly GameSimContext _db = new GameSimContext();
        [MyExpirePageActionFilter]
        public ActionResult Index()
        {
            return View();
        }
        [MyExpirePageActionFilter]
        public ActionResult Index2()
        {
            var manufacturerTypes = _db.ManufacturerTypes
                .ToArray();
                
            var currentInventoryProducts = _db.Products
                .Where(x=>x.IsCityStorage)
                .OrderBy(x=>x.ProductType.ManufacturerTypeId)
                .ThenBy(x=>x.ProductTypeId)
                .ToArray();
            var buildingUpgrades = _db.BuildingUpgrades
                .Where(x=>!x.Completed)
                .OrderBy(y=>y.Name)
                .AsEnumerable()
                .ToArray();
            var city = new City
            {
                ManufacturerTypes = manufacturerTypes.Select(Mapper.Map<ManufacturerType>).ToArray(),
                CurrentCityStorage = new CityStorage
                {
                    CurrentInventory = currentInventoryProducts
                        .Select(Mapper.Map<Product>).ToArray()
                },
                BuildingUpgrades = buildingUpgrades.Select(Mapper.Map<BuildingUpgrade>).ToArray()
            };
            return View(city);
        }
        [MyExpirePageActionFilter]        
        public ActionResult Index3()
        {
            return View();
        }
    }
}
