using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoApp.Models;

namespace DemoApp.Controllers
{
    public class HomeController : Controller
    {
        DemoEntities entity = new DemoEntities();
        public ActionResult Index()
        {
            return View(entity.Students.ToList());
        }

        public ActionResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Add(StudentModel model)
        {
            if (ModelState.IsValid)
            {
                Student s = new Student();
                s.Name = model.Name;
                s.Age = model.Age;
                s.State = model.State;
                s.Country = model.Country;
                entity.Students.Add(s);
                entity.SaveChanges();
            }
            return PartialView("_Detail", entity.Students.ToList());
        }
        public ActionResult Edit(int? id)
        {
            return PartialView(entity.Students.Where(x => x.Id == id).Single());
        }

        [HttpPost]
        public ActionResult Edit(StudentModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var getdata = entity.Students.Where(x => x.Id == model.Id).First();
                getdata.Name = model.Name;
                getdata.Age = model.Age;
                getdata.State = model.State;
                getdata.Country = model.Country;
                entity.SaveChanges();
            }
            return PartialView("_Detail", entity.Students.ToList());
        }

        public ActionResult Delete(int id)
        {
            entity.Students.Remove(entity.Students.Find(id));
            entity.SaveChanges();
            return PartialView("_Detail", entity.Students.ToList());
        }
    }
}
