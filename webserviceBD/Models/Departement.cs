using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webserviceBD.Models
{
    public class Departement
    {
        public Departement()
        {
          this.Employees=new HashSet<Employee>();
        }

        public int DepartementId { get; set; }

        public String Name { get; set;}

        public ICollection<Employee> Employees { get; set; }
    }
}