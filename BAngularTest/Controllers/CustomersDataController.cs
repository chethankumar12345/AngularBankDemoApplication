using BAngularTest.Models;
using StudentWebApi.Controllers.CustomModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace BAngularTest.Controllers
{
    public class CustomersDataController : ApiController
    {
        private BankTestEntities db = new BankTestEntities();

        // GET: api/CustomersData
        public IQueryable<Customer> GettblCustomers()
        {
            return db.tblCustomers.Select(pX => new Customer() { CustomerId = pX.CustomerId, Name = pX.Name, MobileNumber = pX.MobileNumber, EmailId = pX.EmailId });
        }

        // GET: api/CustomersData/5
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> GettblCustomer(int id)
        {
            tblCustomer tblCustomer = await db.tblCustomers.FindAsync(id);
            if (tblCustomer == null)
            {
                return NotFound();
            }

            return Ok(new Customer() { CustomerId = tblCustomer.CustomerId, Name = tblCustomer.Name, MobileNumber = tblCustomer.MobileNumber, EmailId = tblCustomer.EmailId });
        }

        // PUT: api/CustomersData/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblCustomer(int id, tblCustomer tblCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblCustomer.CustomerId)
            {
                return BadRequest();
            }

            db.Entry(tblCustomer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblCustomerExists(id))
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

        // POST: api/CustomersData
        [ResponseType(typeof(tblCustomer))]
        public async Task<IHttpActionResult> PosttblCustomer(tblCustomer tblCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblCustomers.Add(tblCustomer);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblCustomer.CustomerId }, tblCustomer);
        }

        // DELETE: api/CustomersData/5
        [ResponseType(typeof(tblCustomer))]
        public async Task<IHttpActionResult> DeletetblCustomer(int id)
        {
            tblCustomer tblCustomer = await db.tblCustomers.FindAsync(id);
            if (tblCustomer == null)
            {
                return NotFound();
            }

            db.tblCustomers.Remove(tblCustomer);
            await db.SaveChangesAsync();

            return Ok(tblCustomer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblCustomerExists(int id)
        {
            return db.tblCustomers.Count(e => e.CustomerId == id) > 0;
        }
    }
}