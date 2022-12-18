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
using TestApi123.Models;
using TestApi123.model;

namespace TestApi123.Controllers
{
    public class CustomersController : ApiController
    {
        private TestingEntities db = new TestingEntities();

        // GET: api/Customers
        public IQueryable<Customers> GetCustomers()
        {
            return db.Customers;
        }
        [Route("api/login")]
        public IHttpActionResult Login([FromBody] userdata userdata)
        {
            var user = db.Customers.ToList().Where(i => i.Name == userdata.Name && i.PhoneNumber == userdata.PhoneNumber)
                .FirstOrDefault();
            if(user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(new ResponceCustomer(user));
            }
        }
        public class userdata
        {
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
        }

        // GET: api/Customers/5
        [ResponseType(typeof(Customers))]
        public IHttpActionResult GetCustomers(int id)
        {
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return NotFound();
            }

            return Ok(customers);
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomers(int id, Customers customers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customers.id)
            {
                return BadRequest();
            }

            db.Entry(customers).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomersExists(id))
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

        // POST: api/Customers
        [ResponseType(typeof(Customers))]
        public IHttpActionResult PostCustomers(Customers customers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(customers);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomersExists(customers.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = customers.id }, customers);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customers))]
        public IHttpActionResult DeleteCustomers(int id)
        {
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customers);
            db.SaveChanges();

            return Ok(customers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomersExists(int id)
        {
            return db.Customers.Count(e => e.id == id) > 0;
        }
    }
}