using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class Attendence
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public bool MarkAttendence { get; set; }
    }
}