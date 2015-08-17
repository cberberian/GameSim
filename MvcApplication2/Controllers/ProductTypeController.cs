using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using MvcApplication2.Models;
using SimGame.Data;


namespace MvcApplication2.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly GameSimContext _db = new GameSimContext();

        //
        // GET: /ProductType/

        public ActionResult Index()
        {
            return View(_db.ProductTypes.OrderBy(x=>x.Name).ToList());
        }

        //
        // GET: /ProductType/Details/5

        public ActionResult Details(int id = 0)
        {
            var producttype = _db.ProductTypes.Find(id);
            if (producttype == null)
            {
                return HttpNotFound();
            }
            return View(producttype);
        }

        //
        // GET: /ProductType/Create

        public ActionResult Create()
        {
            ViewBag.ManufacturerTypes = _db.ManufacturerTypes;
            return View();
        }

        //
        // POST: /ProductType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductType producttype)
        {
            if (ModelState.IsValid)
            {
                var newId = _db.ProductTypes.Max(x => x.Id) + 1;
                producttype.Id = newId; 
                _db.ProductTypes.Add(Mapper.Map<SimGame.Domain.ProductType>(producttype));
                var product = new SimGame.Domain.Product
                {
                    IsCityStorage = true,
                    ProductTypeId = newId
                };
                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(producttype);
        }

        //
        // GET: /ProductType/Edit/5

        public ActionResult Edit(int id)
        {
            var producttype = _db.ProductTypes.Find(id);
            ViewBag.ManufacturerTypes = _db.ManufacturerTypes;
            if (producttype == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<ProductType>(producttype));
        }

        //
        // POST: /ProductType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductType producttype)
        {
            if (ModelState.IsValid)
            {
                var modified = EntityState.Modified;
                _db.Entry(producttype).State = modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producttype);
        }

        //
        // GET: /ProductType/Delete/5

        public ActionResult Delete(int id = -1)
        {
            var producttype = _db.ProductTypes.Find(id);
            if (producttype == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<ProductType>(producttype));
        }

        //
        // POST: /ProductType/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var producttype = _db.ProductTypes.Find(id);
            _db.ProductTypes.Remove(producttype);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}