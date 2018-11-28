using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital_Management_System.Models;

namespace Hospital_Management_System.CollectionViewModels
{
    public class CollectionOfAll
    {
        public IEnumerable<Ambulance> Ambulances { get; set; }
        public IEnumerable<Department> Departments { get; set; }
    }
}