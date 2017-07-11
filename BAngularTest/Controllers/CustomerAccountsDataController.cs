using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BAngularTest.Models;

namespace BAngularTest.Controllers
{
    public class CustomerAccountsDataController : ApiController
    {
        private BankTestEntities db = new BankTestEntities();

        // GET: api/tblCustomerAccounts
        public IQueryable<tblCustomerAccount> GettblCustomerAccounts()
        {
            return db.tblCustomerAccounts;
        }

        // GET: api/tblCustomerAccounts/5
        [ResponseType(typeof(tblCustomerAccount))]
        public async Task<IHttpActionResult> GettblCustomerAccount(int id)
        {
            tblCustomerAccount tblCustomerAccount = await db.tblCustomerAccounts.FindAsync(id);
            if (tblCustomerAccount == null)
            {
                return NotFound();
            }

            return Ok(tblCustomerAccount);
        }

        // PUT: api/tblCustomerAccounts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblCustomerAccount(int id, tblCustomerAccount tblCustomerAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblCustomerAccount.CustomerAccountId)
            {
                return BadRequest();
            }

            db.Entry(tblCustomerAccount).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblCustomerAccountExists(id))
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

        // POST: api/tblCustomerAccounts
        [ResponseType(typeof(tblCustomerAccount))]
        public async Task<IHttpActionResult> PosttblCustomerAccount(tblCustomerAccount tblCustomerAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblCustomerAccounts.Add(tblCustomerAccount);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblCustomerAccount.CustomerAccountId }, tblCustomerAccount);
        }

        // DELETE: api/tblCustomerAccounts/5
        [ResponseType(typeof(tblCustomerAccount))]
        public async Task<IHttpActionResult> DeletetblCustomerAccount(int id)
        {
            tblCustomerAccount tblCustomerAccount = await db.tblCustomerAccounts.FindAsync(id);
            if (tblCustomerAccount == null)
            {
                return NotFound();
            }

            db.tblCustomerAccounts.Remove(tblCustomerAccount);
            await db.SaveChangesAsync();

            return Ok(tblCustomerAccount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblCustomerAccountExists(int id)
        {
            return db.tblCustomerAccounts.Count(e => e.CustomerAccountId == id) > 0;
        }
    }
}