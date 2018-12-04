using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Hospital_Management_System.CollectionViewModels;
using Hospital_Management_System.Models;

namespace Hospital_Management_System.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db;
        private ApplicationUserManager _userManager;
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

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {
            var model = new CollectionOfAll
            {
                Ambulances = db.Ambulances.ToList(),
                Departments = db.Department.ToList()
            };
            return View(model);
        }

        //Department Section

        //Department List
        [Authorize]
        public ActionResult DepartmentList()
        {
            var model = db.Department.ToList();
            return View(model);
        }

        //Add Department
        [Authorize]
        public ActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDepartment(Department model)
        {
            if (db.Department.Any(c => c.Name == model.Name))
            {
                ModelState.AddModelError("Name", "Name already present!");
                return View(model);
            }
            db.Department.Add(model);
            db.SaveChanges();
            return RedirectToAction("DepartmentList");
        }

        //Edit Department
        [Authorize]
        public ActionResult EditDepartment(int id)
        {
            var model = db.Department.SingleOrDefault(c => c.Id == id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDepartment(int id, Department model)
        {
            var department = db.Department.Single(c => c.Id == id);
            department.Name = model.Name;
            department.Description = model.Description;
            department.Status = model.Status;
            db.SaveChanges();
            return RedirectToAction("DepartmentList");
        }

        [Authorize]
        public ActionResult DeleteDepartment(int? id)
        {
            var department = db.Department.Single(c => c.Id == id);
            return View(department);
        }

        [HttpPost, ActionName("DeleteDepartment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDepartment(int id)
        {
            var department = db.Department.SingleOrDefault(c => c.Id == id);
            db.Department.Remove(department);
            db.SaveChanges();
            return RedirectToAction("DepartmentList");
        }

        //End Department Section

        //Start Ambulance Section
        //Ambulance Driver Section

        //Add Ambulance Driver
        [Authorize]
        public ActionResult AddAmbulanceDriver()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAmbulanceDriver(AmbulanceDriver model)
        {
            db.AmbulanceDrivers.Add(model);
            db.SaveChanges();
            return View();
        }

        //Edit Ambulance Driver
        [Authorize]
        public ActionResult EditAmbulanceDriver(int id)
        {
            var viewmodel = db.AmbulanceDrivers.SingleOrDefault(c => c.Id == id);
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAmbulanceDriver(int id, AmbulanceDriver model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var driver = db.AmbulanceDrivers.Single(c => c.Id == id);
            driver.Name = model.Name;
            driver.Contact = model.Contact;
            driver.Address = model.Address;
            driver.Cnic = model.Cnic;
            db.SaveChanges();
            return RedirectToAction("ListOfAmbulanceDrivers");
        }
        //List of Ambulance Drivers
        [Authorize]
        public ActionResult ListOfAmbulanceDrivers()
        {
            var model = db.AmbulanceDrivers.ToList();
            return View(model);
        }

        //Ambulance Section
        //Add Ambulance
        [Authorize]
        public ActionResult AddAmbulance()
        {
            var model = new AmbulanceCollection
            {
                Ambulance = new Ambulance(),
                AmbulanceDrivers = db.AmbulanceDrivers.ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAmbulance(AmbulanceCollection model)
        {
            if (!ModelState.IsValid)
            {
                var viewmodel = new AmbulanceCollection
                {
                    Ambulance = model.Ambulance,
                    AmbulanceDrivers = db.AmbulanceDrivers.ToList()
                };
                return View(viewmodel);
            }

            db.Ambulances.Add(model.Ambulance);
            db.SaveChanges();
            return RedirectToAction("ListOfAmbulances");
        }

        //Edit Ambulance
        [Authorize]
        public ActionResult EditAmbulance(int id)
        {
            var viewmodel = new AmbulanceCollection
            {
                Ambulance = db.Ambulances.Single(c => c.Id == id),
                AmbulanceDrivers = db.AmbulanceDrivers.ToList()
            };
        return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAmbulance(int id, AmbulanceCollection model)
        {
            if (!ModelState.IsValid)
            {
                var viewmodel = new AmbulanceCollection
                {
                    Ambulance = model.Ambulance,
                    AmbulanceDrivers = db.AmbulanceDrivers.ToList()
                };
                return View(viewmodel);
            }
            else
            {
                var ambulance = db.Ambulances.Single(c => c.Id == id);
                ambulance.Name = model.Ambulance.Name;
                ambulance.AmbulanceId = model.Ambulance.AmbulanceId;
                ambulance.AmbulanceStatus = model.Ambulance.AmbulanceStatus;
                ambulance.AmbulanceDriverId = model.Ambulance.AmbulanceDriverId;
            }

            db.SaveChanges();
            return RedirectToAction("ListOfAmbulances");
        }

        //List of Ambulances
        [Authorize]
        public ActionResult ListOfAmbulances()
        {
            var model = db.Ambulances.Include(c => c.AmbulanceDriver).ToList();
            return View(model);
        }

        //Delete Ambulance
        [Authorize]
        public ActionResult DeleteAmbulance(int? id)
        {
            var ambulance = db.Ambulances.Single(c => c.Id == id);
            return View(ambulance);
        }

        [HttpPost, ActionName("DeleteAmbulance")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAmbulance(int id)
        {
            var ambulance = db.Ambulances.SingleOrDefault(c => c.Id == id);
            var driver = db.AmbulanceDrivers.Single(c => c.Id == ambulance.AmbulanceDriverId);
            db.Ambulances.Remove(ambulance);
            db.AmbulanceDrivers.Remove(driver);
            db.SaveChanges();
            return RedirectToAction("ListOfAmbulances");
        }

        //End Ambulance Section

        //Start Medicine Section

        //Add Medicine
        [Authorize]
        public ActionResult AddMedicine()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMedicine(Medicine model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            db.Medicines.Add(model);
            db.SaveChanges();
            return RedirectToAction("ListOfMedicine");
        }

        //List of Medicines
        [Authorize]
        public ActionResult ListOfMedicine()
        {
            var medicine = db.Medicines.ToList();
            return View(medicine);
        }

        //Edit Medicine
        [Authorize]
        public ActionResult EditMedicine(int id)
        {
            var medicine = db.Medicines.Single(c => c.Id == id);
            return View(medicine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMedicine(int id, Medicine model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var medicine = db.Medicines.Single(c => c.Id == id);
            medicine.Name = model.Name;
            medicine.Description = model.Description;
            medicine.Price = model.Price;
            medicine.Quantity = model.Quantity;

            db.SaveChanges();
            return RedirectToAction("ListOfMedicine");
        }

        //Delete Medicine
        [Authorize]
        public ActionResult DeleteMedicine(int? id)
        {
            return View();
        }

        [HttpPost, ActionName("DeleteMedicine")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMedicine(int id)
        {
            var medicine = db.Medicines.Single(c => c.Id == id);
            db.Medicines.Remove(medicine);
            db.SaveChanges();
            return RedirectToAction("ListOfMedicine");
        }

        //End Medicine Section

        //Start Doctor Section

        //Add Doctor 
        [Authorize]
        public ActionResult AddDoctor()
        {
            var collection = new DoctorCollection
            {
                ApplicationUser = new RegisterViewModel(),
                Doctor = new Doctor(),
                Departments = db.Department.ToList()
            };
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddDoctor(DoctorCollection model)
        {
            var user = new ApplicationUser
            {
                UserName = model.ApplicationUser.UserName,
                Email = model.ApplicationUser.Email,
                UserRole = "Doctor",
                RegisteredDate = DateTime.Now.Date
            };
            var result = await UserManager.CreateAsync(user, model.ApplicationUser.Password);
            if (result.Succeeded)
            {
                await UserManager.AddToRoleAsync(user.Id, "Doctor");
                var doctor = new Doctor
                {
                    FirstName = model.Doctor.FirstName,
                    LastName = model.Doctor.LastName,
                    FullName = "Dr. " + model.Doctor.FirstName + " " + model.Doctor.LastName,
                    EmailAddress = model.ApplicationUser.Email,
                    ContactNo = model.Doctor.ContactNo,
                    PhoneNo = model.Doctor.PhoneNo,
                    Designation = model.Doctor.Designation,
                    Education = model.Doctor.Education,
                    DepartmentId = model.Doctor.DepartmentId,
                    Specialization = model.Doctor.Specialization,
                    Gender = model.Doctor.Gender,
                    BloodGroup = model.Doctor.BloodGroup,
                    ApplicationUserId = user.Id,
                    DateOfBirth = model.Doctor.DateOfBirth,
                    Address = model.Doctor.Address
                };
                db.Doctors.Add(doctor);
                db.SaveChanges();
                return RedirectToAction("ListOfDoctors");
            }

            return HttpNotFound();

        }

        //List Of Doctors
        [Authorize]
        public ActionResult ListOfDoctors()
        {
            var doctor = db.Doctors.Include(c => c.Department).ToList();
            return View(doctor);
        }

        //Detail of Doctor
        [Authorize]
        public ActionResult DoctorDetail(int id)
        {
            var doctor = db.Doctors.Include(c => c.Department).SingleOrDefault(c => c.Id == id);
            return View(doctor);
        }

        //Edit Doctors
        [Authorize]
        public ActionResult EditDoctors(int id)
        {
            var collection = new DoctorCollection
            {
                Departments = db.Department.ToList(),
                Doctor = db.Doctors.Single(c => c.Id == id)
            };
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDoctors(int id, DoctorCollection model)
        {
            var doctor = db.Doctors.Single(c => c.Id == id);
            doctor.FirstName = model.Doctor.FirstName;
            doctor.LastName = model.Doctor.LastName;
            doctor.FullName = "Dr. " + model.Doctor.FirstName + " " + model.Doctor.LastName;
            doctor.ContactNo = model.Doctor.ContactNo;
            doctor.PhoneNo = model.Doctor.PhoneNo;
            doctor.Designation = model.Doctor.Designation;
            doctor.Education = model.Doctor.Education;
            doctor.DepartmentId = model.Doctor.DepartmentId;
            doctor.Specialization = model.Doctor.Specialization;
            doctor.Gender = model.Doctor.Gender;
            doctor.BloodGroup = model.Doctor.BloodGroup;
            doctor.DateOfBirth = model.Doctor.DateOfBirth;
            doctor.Address = model.Doctor.Address;
            db.SaveChanges();

            return RedirectToAction("ListOfDoctors");
        }

        //Delete Doctor pending
        public ActionResult DeleteDoctor(string id)
        {
            var UserId = db.Doctors.Single(c => c.ApplicationUserId == id);
            return View(UserId);
        }

        [HttpPost, ActionName("DeleteDoctor")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDoctor(string id, Doctor model)
        {
            var doctor = db.Doctors.Single(c => c.ApplicationUserId == id);
            var user = db.Users.Single(c => c.Id == id);
            if (db.Schedules.Where(c => c.DoctorId == doctor.Id).Equals(null))
            {
                var schedule = db.Schedules.Single(c => c.DoctorId == doctor.Id);
                db.Schedules.Remove(schedule);
            }
            db.Users.Remove(user);
            db.Doctors.Remove(doctor);
            db.SaveChanges();
            return RedirectToAction("ListOfDoctors");
        }

        //End Doctor Section

        //Start Schedule Section
        //Add Schedule
        [Authorize]
        public ActionResult AddSchedule()
        {
            var collection = new ScheduleCollection
            {
                Schedule = new Schedule(),
                Doctors = db.Doctors.ToList()
            };
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSchedule(ScheduleCollection model)
        {
            if (!ModelState.IsValid)
            {
                var collection = new ScheduleCollection
                {
                    Schedule = model.Schedule,
                    Doctors = db.Doctors.ToList()
                };
                return View(collection);
            }

            db.Schedules.Add(model.Schedule);
            db.SaveChanges();
            return RedirectToAction("ListOfSchedules");
        }

        //List Of Schedules
        [Authorize]
        public ActionResult ListOfSchedules()
        {
            var schedule = db.Schedules.Include(c => c.Doctor).ToList();
            return View(schedule);
        }

        //Edit Schedule
        [Authorize]
        public ActionResult EditSchedule(int id)
        {
            var collection = new ScheduleCollection
            {
                Schedule = db.Schedules.Single(c => c.Id == id),
                Doctors = db.Doctors.ToList()
            };
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSchedule(int id, ScheduleCollection model)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var schedule = db.Schedules.Single(c => c.Id == id);
            schedule.DoctorId = model.Schedule.DoctorId;
            schedule.AvailableEndDay = model.Schedule.AvailableEndDay;
            schedule.AvailableEndTime = model.Schedule.AvailableEndTime;
            schedule.AvailableStartDay = model.Schedule.AvailableStartDay;
            schedule.AvailableStartTime = model.Schedule.AvailableStartTime;
            schedule.Status = model.Schedule.Status;
            schedule.TimePerPatient = model.Schedule.TimePerPatient;
            db.SaveChanges();
            return RedirectToAction("ListOfSchedules");
        }

        //Delete Schedule
        [Authorize]
        public ActionResult DeleteSchedule(int? id)
        {
            return View();
        }

        [HttpPost, ActionName("DeleteSchedule")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSchedule(int id)
        {
            var schedule = db.Schedules.Single(c => c.Id == id);
            db.Schedules.Remove(schedule);
            return RedirectToAction("ListOfSchedules");
        }

        //End Schedule Section

        //Start Patient Section
        [Authorize]
        public ActionResult ListOfPatients()
        {
            var patients = db.Patients.ToList();
            return View(patients);
        }
       
    }
}