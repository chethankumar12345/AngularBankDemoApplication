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
    public class TransactionsDataController : ApiController
    {
        private BankTestEntities db = new BankTestEntities();

        // GET: api/tblTransactions
        public IQueryable<tblTransaction> GettblTransactions()
        {
            return db.tblTransactions;
        }

        // GET: api/tblTransactions/5
        [ResponseType(typeof(tblTransaction))]
        public async Task<IHttpActionResult> GettblTransaction(int id)
        {
            tblTransaction tblTransaction = await db.tblTransactions.FindAsync(id);
            if (tblTransaction == null)
            {
                return NotFound();
            }

            return Ok(tblTransaction);
        }

        // PUT: api/tblTransactions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblTransaction(int id, tblTransaction tblTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblTransaction.TransationId)
            {
                return BadRequest();
            }

            db.Entry(tblTransaction).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblTransactionExists(id))
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

        // POST: api/tblTransactions
        [ResponseType(typeof(tblTransaction))]
        public async Task<IHttpActionResult> PosttblTransaction(tblTransaction tblTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblTransactions.Add(tblTransaction);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblTransaction.TransationId }, tblTransaction);
        }

        // DELETE: api/tblTransactions/5
        [ResponseType(typeof(tblTransaction))]
        public async Task<IHttpActionResult> DeletetblTransaction(int id)
        {
            tblTransaction tblTransaction = await db.tblTransactions.FindAsync(id);
            if (tblTransaction == null)
            {
                return NotFound();
            }

            db.tblTransactions.Remove(tblTransaction);
            await db.SaveChangesAsync();

            return Ok(tblTransaction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblTransactionExists(int id)
        {
            return db.tblTransactions.Count(e => e.TransationId == id) > 0;
        }
    }
}