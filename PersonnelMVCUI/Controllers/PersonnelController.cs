using PersonnelMVCUI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PersonnelMVCUI.ViewModels;

namespace PersonnelMVCUI.Controllers
{
    public class PersonnelController : Controller
    {
        DbPersonnelEntities db = new DbPersonnelEntities();
        // GET: Personnel

        [OutputCache(Duration =30)]
        public ActionResult Index()
        {
            var model = db.Personnel.Include(m => m.Department).ToList();
            return View(model);
        }

        [Authorize(Roles = "A,IT")]
        public ActionResult Add()
        {
            var model = new PersonnelFormViewModel()
            {
                Departments = db.Department.ToList(),
                Personnel = new Personnel()
                
            };
            return View("PersonnelForm", model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Save(PersonnelFormViewModel per)
        {
            if (!ModelState.IsValid)
            {
                //var model = new PersonnelFormViewModel()
                //{
                //    Departments = db.Department.ToList(),
                //    Personnel = per
                //};
                return View("PersonnelForm",per);
            }

            if (per.Personnel.Id == 0) //Addition operation 
            {
                db.Personnel.Add(per.Personnel);
            }
            else //update operation 
            {
                db.Entry(per).State = System.Data.Entity.EntityState.Modified;

                //var personnel = db.Personnel.Find(per.Personnel.Id);
                //personnel.FirstName = per.Personnel.FirstName;
                //personnel.LastName = per.Personnel.LastName;
                //personnel.Salary = per.Personnel.Salary;
                //personnel.DateOfBirth = per.Personnel.DateOfBirth;
                //personnel.Married = per.Personnel.Married;
                //personnel.Gender = per.Personnel.Gender;
                //personnel.DepartmentId = per.Personnel.DepartmentId;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            var model = new PersonnelFormViewModel()
            {
                Departments = db.Department.ToList(),
                Personnel = db.Personnel.Find(id)
            };
            return View("PersonnelForm", model);
        }

        public ActionResult Delete(int id)
        {
            var personnel = db.Personnel.Find(id);
            
            if (personnel==null)
            {
                return HttpNotFound();
            }
            db.Personnel.Remove(personnel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DisplayPersonnels(int id)
        {
            var model = db.Personnel.Where(m => m.DepartmentId == id).ToList();
            return PartialView(model);
        }

        public ActionResult TotalSalary()
        {
            ViewBag.Salary = db.Personnel.Sum(x => x.Salary);
            return PartialView();
        }
    }
}