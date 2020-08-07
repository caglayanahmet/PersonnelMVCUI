using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonnelMVCUI.Models.EntityFramework;

namespace PersonnelMVCUI.Controllers
{
    public class DepartmentController : Controller
    {
        DbPersonnelEntities db = new DbPersonnelEntities();  
        // GET: Department
        public ActionResult Index()
        {
            var model = db.Department.ToList();
            return View(model);
        }

        
        public ActionResult Add()
        {
            return View("DepartmentForm",new Department());
        }

        [HttpPost]
        public ActionResult Save(Department dep)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmentForm");
            }
            if (dep.Id==0)
            {
                db.Department.Add(dep);
            }
            else
            {
                var department = db.Department.Find(dep.Id);
                if (department==null)
                {
                    return HttpNotFound();
                }
                department.Name = dep.Name;
            }
            db.SaveChanges();
            return RedirectToAction("Index","Department");
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