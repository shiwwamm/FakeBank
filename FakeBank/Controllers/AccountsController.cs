using DataModel;
using FakeBank.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FakeBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController(UserAccountDbContext context) : ControllerBase
    {
        private readonly UserAccountDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IList<AccountDTO>>> GetAccounts()
        {
            IQueryable<AccountDTO> x = _context.Accounts.Select(a => new AccountDTO
            {
                UserId = a.User.UserId,
                AccountId = a.AccountId,
                AccountNumber = a.AccountNumber,
                RoutingNumber = a.RoutingNumber,
                Status = a.Status,
                Type = a.Type,
                Balance = a.Balance,
                UserEmail = a.User.Email

            });

            return await x.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(long id)
        {
            Account? account = await _context.Accounts.FindAsync(id);

            if (account == null) { return NotFound(); }

            return account;
        }

        [HttpGet("user/{userid}")]
        public async Task<IEnumerable<Account>> GetUserAccounts(long userid)
        {

            return await _context.Accounts.Where(a => a.UserId == userid).ToListAsync();
        }

        [HttpPut]
        public async Task<IActionResult> EditAccount(int id, Account account)
        {
            if (id != account.AccountId)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { id = account.AccountId }, account);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAccount(long id)
        {
            Account? account = await _context.Accounts.FindAsync(id);

            if (account == null) { return NotFound(); }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }
    }
}
