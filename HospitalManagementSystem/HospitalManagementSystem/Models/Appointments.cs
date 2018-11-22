using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class Appointments
    {
        public int Id { get; set; }
        public List<Doctors> Doctors { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfDaysSick { get; set; }

        public int DoctorsId { get; set; }
    }
}