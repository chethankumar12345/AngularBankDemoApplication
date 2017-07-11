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
    public class BanksDataController : ApiController
    {
        private BankTestEntities db = new BankTestEntities();

        // GET: api/BanksData
        public IQueryable<Bank> GettblBanks()
        {
            return db.tblBanks.Select(pX => new Bank() { BankId = pX.BankId, Name = pX.Name, IFSC = pX.IFSC });
        }

        // GET: api/BanksData/5
        [ResponseType(typeof(Bank))]
        public async Task<IHttpActionResult> GettblBank(int id)
        {
            tblBank tblBank = await db.tblBanks.FindAsync(id);
            if (tblBank == null)
            {
                return NotFound();
            }

            return Ok(new Bank() { BankId = tblBank.BankId, Name = tblBank.Name, IFSC = tblBank.IFSC });
        }

        [HttpGet]
        [Route("api/BanksData/CheckBankUniqueness")]
        [ResponseType(typeof(bool))]
        public bool CheckBankUniqueness(string bankName, int BankId = 0)
        {
            if (BankId != 0)
            {
                return db.tblBanks.Where(pX => pX.BankId != BankId).Where(pX => pX.Name == bankName).Count() > 0;
            }
            return db.tblBanks.Where(pX => pX.Name == bankName).Count() > 0;
        }

        // PUT: api/BanksData/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblBank(int id, tblBank tblBank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblBank.BankId)
            {
                return BadRequest();
            }
            if (CheckBankUniqueness(tblBank.Name, id))
                return Conflict();

            db.Entry(tblBank).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblBankExists(id))
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

        // POST: api/BanksData
        [ResponseType(typeof(tblBank))]
        public async Task<IHttpActionResult> PosttblBank(tblBank tblBank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (CheckBankUniqueness(tblBank.Name))
                return Conflict();

            db.tblBanks.Add(tblBank);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblBank.BankId }, tblBank);
        }

        // DELETE: api/BanksData/5
        [ResponseType(typeof(tblBank))]
        public async Task<IHttpActionResult> DeletetblBank(int id)
        {
            tblBank tblBank = await db.tblBanks.FindAsync(id);
            if (tblBank == null)
            {
                return NotFound();
            }

            db.tblBanks.Remove(tblBank);
            await db.SaveChangesAsync();

            return Ok(tblBank);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblBankExists(int id)
        {
            return db.tblBanks.Count(e => e.BankId == id) > 0;
        }
    }
}