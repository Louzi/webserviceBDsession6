using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using webserviceBD.Models;

namespace webserviceBD.Controllers
{
    public class EmployeesController : ApiController
    {
        private webserviceBDContext db = new webserviceBDContext();

        // GET: api/Employees
        public IQueryable<Employee> GetEmployees()
        {
            return db.Employees.Include(a=>a.Departement);
        }

        // GET: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [Route("api/Employees/GetEmployeeByName/{name}")]
        [ResponseType(typeof(List<Employee>))]
        public IHttpActionResult GetEmployeeByName(string name)
        {
            List<Employee> Employees = db.Employees.Where(a => a.Name.Equals(name)).ToList();
            return Ok(Employees);
        }
        [Route("api/Employees/a/{name}")]
        [ResponseType(typeof(List<Employee>))]
        public IHttpActionResult GetEmployeeByName2(string name)
        {
            List<Employee> Employees = db.Employees.Where(a => a.Name.Contains(name)).ToList();
            return Ok(Employees);
        }

        [Route("api/Employees/a/{name}/{age}")]
        [ResponseType(typeof(List<Employee>))]
        public IHttpActionResult GetEmployeeByNameandage(string name,int age)
        {
            List<Employee> Employees = db.Employees.Where(a => a.Name.Contains(name) && a.Age==age).ToList();
            return Ok(Employees);
        }

        [Route("api/Employees/age/{name}/{age}")]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployeeByNameandage2(string name, int age)
        {
            Employee Employee = db.Employees.Where(a => a.Name.Contains(name) && a.Age == age).FirstOrDefault();
            return Ok(Employee);
        }
        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employees
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employee.EmployeeId }, employee);
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeId == id) > 0;
        }
    }
}