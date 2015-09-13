using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using SimGame.Data;
using SimGame.Website.Models;

namespace SimGame.Website.Controllers
{
    public class ProductController : Controller
    {
        private readonly GameSimContext _db = new GameSimContext();
        //
        // GET: /Product/

        public ActionResult Index()
        {
            var products = _db.Products
                                .OrderBy(x=>x.RequiredByTypeId)
                                .ThenBy(y=>y.ProductType.Name)
                                .ToArray();
            return View(products.Select(Mapper.Map<Product>)
                                .ToList());
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == id);
            if (product==null)
                return HttpNotFound();
            return View(Mapper.Map<Product>(product));
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            ViewBag.ProductTypes = _db.ProductTypes.OrderBy(x=>x.Name);
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                ViewBag.ProductTypes = _db.ProductTypes.OrderBy(x => x.Name);
                if (ModelState.IsValid)
                {
                    _db.Products.Add(Mapper.Map<SimGame.Domain.Product>(product));
                    _db.Commit();
                }
                return View(product);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(int id)
        {

            TempData["ReturnURL"] = Request.UrlReferrer.OriginalString;
            var product = _db.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
                return HttpNotFound();
            return View(Mapper.Map<Product>(product));
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                var dbProduct = _db.Products.FirstOrDefault(x => x.Id == id);
                if (dbProduct == null)
                    return HttpNotFound();
                Mapper.Map(product, dbProduct);

                return Redirect(TempData["ReturnURL"].ToString());
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Product/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, Product product)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
