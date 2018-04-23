using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        NorthwindEntities NorthWind = new NorthwindEntities();
        public ActionResult Index()
        {
            var model = NorthWind.Categories.ToList();
            return View(model);
        }
    
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {
            var model = NorthWind.Categories.First(c => c.CategoryID == id);
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id,FormCollection form)
        {
            var model = NorthWind.Categories.First(c => c.CategoryID == id);
            UpdateModel(model, new[] { "CategoryName", "Description" });
            NorthWind.SaveChanges();
            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Details(int id)
        {
            var model = NorthWind.Categories.First(c => c.CategoryID == id);
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create(){
            Categories category=new Categories();
            return View(category);
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(int CategoryID, FormCollection form)
        {
            var model = NorthWind.Categories.FirstOrDefault(c => c.CategoryID == CategoryID);
            if (model == null)
            {
                Categories category = new Categories();

                UpdateModel(category, new[] { "CategoryName", "Description" });

                NorthWind.AddToCategories(category);
                NorthWind.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Create");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Delete(int id)
        {
            var model = NorthWind.Categories.First(c => c.CategoryID == id);
            NorthWind.Categories.Remove(model);
            NorthWind.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
