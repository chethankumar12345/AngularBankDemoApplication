using BAngularTest.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace BAngularTest.Controllers
{
    public class TransactionTypesDataController : ApiController
    {
        private BankTestEntities db = new BankTestEntities();

        // GET: api/TransactionTypesData
        public IQueryable<tblTransactionType> GettblTransactionTypes()
        {
            return db.tblTransactionTypes;
        }

        // GET: api/TransactionTypesData/5
        [ResponseType(typeof(tblTransactionType))]
        public async Task<IHttpActionResult> GettblTransactionType(int id)
        {
            tblTransactionType tblTransactionType = await db.tblTransactionTypes.FindAsync(id);
            if (tblTransactionType == null)
            {
                return NotFound();
            }

            return Ok(tblTransactionType);
        }

        // PUT: api/TransactionTypesData/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblTransactionType(int id, tblTransactionType tblTransactionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblTransactionType.TransactionTypeId)
            {
                return BadRequest();
            }

            db.Entry(tblTransactionType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblTransactionTypeExists(id))
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

        // POST: api/TransactionTypesData
        [ResponseType(typeof(tblTransactionType))]
        public async Task<IHttpActionResult> PosttblTransactionType(tblTransactionType tblTransactionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblTransactionTypes.Add(tblTransactionType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tblTransactionTypeExists(tblTransactionType.TransactionTypeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tblTransactionType.TransactionTypeId }, tblTransactionType);
        }

        // DELETE: api/TransactionTypesData/5
        [ResponseType(typeof(tblTransactionType))]
        public async Task<IHttpActionResult> DeletetblTransactionType(int id)
        {
            tblTransactionType tblTransactionType = await db.tblTransactionTypes.FindAsync(id);
            if (tblTransactionType == null)
            {
                return NotFound();
            }

            db.tblTransactionTypes.Remove(tblTransactionType);
            await db.SaveChangesAsync();

            return Ok(tblTransactionType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblTransactionTypeExists(int id)
        {
            return db.tblTransactionTypes.Count(e => e.TransactionTypeId == id) > 0;
        }
    }
}