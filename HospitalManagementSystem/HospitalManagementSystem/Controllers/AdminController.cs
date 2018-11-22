using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db;

        //Constructor
        public AdminController()
        {
            db = new ApplicationDbContext();
        }

        //Destructor
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        // GET: Admin
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Index()
        {
            var model = db.Departments.ToList();
            return View(model);
        }

        //Department Section

        //Department List
        [Authorize(Roles = RoleName.Doctor + "," + RoleName.Admin)]
        public ActionResult DepartmentList()
        {
            var model = db.Departments.ToList();
            return View(model);
        }

        //Add Department
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDepartment(Departments model)
        {
            if (db.Departments.Any(c => c.Name == model.Name))
            {
                ModelState.AddModelError("Name", "Name already present!");
                return View(model);
            }
            db.Departments.Add(model);
            db.SaveChanges();
            return RedirectToAction("DepartmentList");
        }

        [Authorize(Roles = RoleName.Admin)]
        //Edit Department
        public ActionResult EditDepartment(int id)
        {
            var model = db.Departments.SingleOrDefault(c => c.Id == id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDepartment(int id, Departments model)
        {
            var department = db.Departments.Single(c => c.Id == id);
            department.Name = model.Name;
            department.Description = model.Description;
            department.Status = model.Status;
            db.SaveChanges();
            return RedirectToAction("DepartmentList");
        }


        [Authorize(Roles = RoleName.Admin)]
        public ActionResult DeleteDepartment(int? id)
        {
            var department = db.Departments.Single(c => c.Id == id);
            return View(department);
        }

        [HttpPost, ActionName("DeleteDepartment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDepartment(int id)
        {
            var department = db.Departments.SingleOrDefault(c => c.Id == id);
            db.Departments.Remove(department);
            db.SaveChanges();
            return RedirectToAction("DepartmentList");
        }

        //End Department Section

        //Start Patient Section
        
        //Add Patient
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult AddPatient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPatient(Patients model)
        {
            //if (db.Departments.Any(c => c.Id == model.Id))
            //{
            //    ModelState.AddModelError("Id", "Id already present!");
            //    return View(model);
            //}
            db.Patients.Add(model);
            db.SaveChanges();
            return RedirectToAction("ViewPatientsList", "Doctor");
        }

        //End Patient Section

        //public ActionResult Appointment()
        //{
        //    var doctors = new List<Doctors>();
        //    doctors = db.Doctors.ToList();
        //    ViewBag.Data = doctors;
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Appointment(Appointment model)
        //{
        //    db.Appointments.Add(model);
        //    db.SaveChanges();
        //    return RedirectToAction("DepartmentList");
        //}

        //public ActionResult AppontmentList()
        //{
        //    var model = db.Appointments.ToList();
        //    return View(model);
        //}




        //Start Ambulance Section



    }
}