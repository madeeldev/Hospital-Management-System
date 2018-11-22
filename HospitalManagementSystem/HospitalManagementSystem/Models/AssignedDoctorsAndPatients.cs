using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class AssignedDoctorsAndPatients
    {
        public int Id { get; set; }


        public Patients Patients { get; set; }
        public int PatientsId { get; set; }

        public Doctors Doctors { get; set; }
        public int DoctorsId { get; set; }
    }
}