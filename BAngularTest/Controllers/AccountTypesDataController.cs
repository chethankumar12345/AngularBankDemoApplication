using BAngularTest.Models;
using StudentWebApi.Controllers.CustomModel;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
namespace BAngularTest.Controllers
{
    [Authorize]
    public class AccountTypesDataController : ApiController
    {
        private BankTestEntities db = new BankTestEntities();


        // GET: api/AccountTypesData
        public IQueryable<AccountType> GettblAccountTypes()
        {
            return db.tblAccountTypes.ToList().Join(db.tblBanks.ToList(), pX => pX.BankId, pY => pY.BankId, (pX, pY) => new { pX.AccountTypeId, pX.AccountType, pY.Name }).Select(pX => new AccountType() { AccountTypeId = pX.AccountTypeId, AccountTypeName = pX.AccountType, BankName = pX.Name }).AsQueryable();
        }

        // GET: api/AccountTypesData/5
        [ResponseType(typeof(AccountType))]
        public async Task<IHttpActionResult> GettblAccountType(int id)
        {
            tblAccountType tblAccountType = await db.tblAccountTypes.FindAsync(id);
            if (tblAccountType == null)
            {
                return NotFound();
            }

            return Ok(new AccountType() { AccountTypeId = tblAccountType.AccountTypeId, AccountTypeName = tblAccountType.AccountType, BankId = tblAccountType.BankId });
        }
        [HttpGet]
        [Route("api/AccountTypesData/CheckAccountTypeUniqueness")]
        [ResponseType(typeof(bool))]
        public bool CheckAccountTypeUniqueness(string accountTypeName, int accountTypeId = 0)
        {
            if (accountTypeId == 0)
            {
                return db.tblAccountTypes.Where(pX => pX.AccountType == accountTypeName).Count() > 0;
            }
            else
            {
                return db.tblAccountTypes.Where(pX => pX.AccountTypeId != accountTypeId).Where(pX => pX.AccountType == accountTypeName).Count() > 0;
            }

        }

        // PUT: api/AccountTypesData/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblAccountType(int id, tblAccountType tblAccountType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (CheckAccountTypeUniqueness(tblAccountType.AccountType, id))
                return Conflict();

            if (id != tblAccountType.AccountTypeId)
            {
                return BadRequest();
            }

            db.Entry(tblAccountType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblAccountTypeExists(id))
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

        // POST: api/AccountTypesData
        [ResponseType(typeof(tblAccountType))]
        public async Task<IHttpActionResult> PosttblAccountType(tblAccountType tblAccountType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (CheckAccountTypeUniqueness(tblAccountType.AccountType))
                return Conflict();

            db.tblAccountTypes.Add(tblAccountType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblAccountType.AccountTypeId }, tblAccountType);
        }

        // DELETE: api/AccountTypesData/5
        [ResponseType(typeof(tblAccountType))]
        public async Task<IHttpActionResult> DeletetblAccountType(int id)
        {
            try
            {
                tblAccountType tblAccountType = await db.tblAccountTypes.FindAsync(id);
                if (tblAccountType == null)
                {
                    return NotFound();
                }

                db.tblAccountTypes.Remove(tblAccountType);
                await db.SaveChangesAsync();

                return Ok(tblAccountType);
            }
            catch (Exception exp)
            {
                return InternalServerError(exp);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblAccountTypeExists(int id)
        {
            return db.tblAccountTypes.Count(e => e.AccountTypeId == id) > 0;
        }
    }
}