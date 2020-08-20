using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonnelMVCUI.Models.EntityFramework;
using PersonnelMVCUI.ViewModels;

namespace PersonnelMVCUI.Controllers
{
    [Authorize(Roles = "A,IT")]
    public class DepartmentController : Controller
    {
        DbPersonnelEntities db = new DbPersonnelEntities();  

        
        public ActionResult Index()
        {
            var model = db.Department.ToList();
            int a = 10, b = 0;
            int c = a / b;
            return View(model);
        }

        
        public ActionResult Add()
        {
            return View("DepartmentForm",new Department());
        }

       [ValidateAntiForgeryToken]
        public ActionResult Save(Department dep)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmentForm");
            }
            MessageViewModel model = new MessageViewModel();
            if (dep.Id==0)
            {
                db.Department.Add(dep);
                model.Message = dep.Name + " added successfully... ";
            }
            else
            {
                var department = db.Department.Find(dep.Id);
                if (department==null)
                {
                    return HttpNotFound();
                }
                department.Name = dep.Name;
                model.Message = dep.Name + " updated successfully... ";
            }
            db.SaveChanges();

            model.Status = true;
            model.LinkText = "Department List";
            model.Url = "/Department";
            return View("_Message",model);
        }

        public ActionResult Update(int id)
        {
            var model = db.Department.Find(id);
            if (model==null)
            {
                return HttpNotFound();
            }
            return View("DepartmentForm",model);
        }

        public ActionResult Delete(int id)
        {
            var department = db.Department.Find(id);
            if (department==null)
            {
                return HttpNotFound();
            }
            db.Department.Remove(department);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}