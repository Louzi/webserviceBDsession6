using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webserviceBD.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public String Name { get; set; }

        public int  Age { get; set; }


        //OneToOne(Employee-->Departement)
        public int DepartementId { get; set; }
        [ForeignKey("DepartementId")]
        public Departement Departement { get; set; }

    }
}