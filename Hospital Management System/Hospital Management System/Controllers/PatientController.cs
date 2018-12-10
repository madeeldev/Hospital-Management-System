using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hospital_Management_System.CollectionViewModels;
using Hospital_Management_System.Models;
using Microsoft.AspNet.Identity;

namespace Hospital_Management_System.Controllers
{
    public class PatientController : Controller
    {
        private ApplicationDbContext db;

        //Constructor
        public PatientController()
        {
            db = new ApplicationDbContext();
        }

        //Destructor
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        [Authorize(Roles = "Patient")]
        public ActionResult Index()
        {
            var date = DateTime.Now.Date;
            var model = new CollectionOfAll
            {
                Ambulances = db.Ambulances.ToList(),
                Departments = db.Department.ToList(),
                Doctors = db.Doctors.ToList(),
                Patients = db.Patients.ToList(),
                Medicines = db.Medicines.ToList(),
                ActiveAppointments = db.Appointments.Where(c => c.Status).Where(c => c.AppointmentDate >= date).ToList(),
                PendingAppointments = db.Appointments.Where(c => c.Status == false).Where(c => c.AppointmentDate >= date).ToList(),
                AmbulanceDrivers = db.AmbulanceDrivers.ToList(),
                Announcements = db.Announcements.Where(c => c.AnnouncementFor == "Patient").ToList()
            };
            return View(model);
        }

        //Update Patient profile
        [Authorize(Roles = "Patient")]
        public ActionResult UpdateProfile(string id)
        {
            var patient = db.Patients.Single(c => c.ApplicationUserId == id);
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(string id, Patient model)
        {
            var patient = db.Patients.Single(c => c.ApplicationUserId == id);
            patient.FirstName = model.FirstName;
            patient.LastName = model.LastName;
            patient.FullName = model.FirstName + " " + model.LastName;
            patient.Contact = model.Contact;
            patient.Address = model.Address;
            patient.BloodGroup = model.BloodGroup;
            patient.DateOfBirth = model.DateOfBirth;
            patient.Gender = model.Gender;
            patient.PhoneNo = model.PhoneNo;
            db.SaveChanges();
            return View();
        }


        //Start Appointment Section

        //Add Appointment
        [Authorize(Roles = "Patient")]
        public ActionResult AddAppointment()
        {
            var collection = new AppointmentCollection
            {
                Appointment = new Appointment(),
                Doctors = db.Doctors.ToList()
            };
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAppointment(AppointmentCollection model)
        {
            string user = User.Identity.GetUserId();
            var patient = db.Patients.Single(c => c.ApplicationUserId == user);
            var appointment = new Appointment();
            appointment.PatientId = patient.Id;
            appointment.DoctorId = model.Appointment.DoctorId;
            appointment.AppointmentDate = model.Appointment.AppointmentDate;
            appointment.Problem = model.Appointment.Problem;
            appointment.Status = false;

            db.Appointments.Add(appointment);
            db.SaveChanges();
            return RedirectToAction("ListOfAppointments");

        }

        //List of Appointments
        [Authorize(Roles = "Patient")]
        public ActionResult ListOfAppointments()
        {
            string user = User.Identity.GetUserId();
            var patient = db.Patients.Single(c => c.ApplicationUserId == user);
            var appointment = db.Appointments.Include(c => c.Doctor).Where(c => c.PatientId == patient.Id).ToList();
            return View(appointment);
        }

        //Delete Appointment
        [Authorize(Roles = "Patient")]
        public ActionResult DeleteAppointment(int? id)
        {
            var appointment = db.Appointments.Single(c => c.Id == id);
            return View(appointment);
        }

        [HttpPost, ActionName("DeleteAppointment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAppointment(int id)
        {
            var appointment = db.Appointments.Single(c => c.Id == id);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("ListOfAppointments");
        }
    }
}