using DataModel;
using FakeBank.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FakeBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserAccountDbContext _context;

        public UsersController(UserAccountDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser( long id )
        {
            User? user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpGet("totalbalance/{id}")]
        public async Task<ActionResult<UserBalance>> GetUserBalance( long id )
        {
            User? user = await _context.Users.FindAsync(id);

            if (user == null) { return NotFound(); }

            decimal totalBalance = await _context.Accounts.Where(x => x.UserId == id).Select(x => x.Balance).SumAsync();

            UserBalance userBalance = new()
            {
                UserId = user.UserId,
                UserEmail = user.Email,
                Balance = totalBalance
            };

            return userBalance;

        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser( User user )
        {
            _context.Users.Add( user );
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new {id = user.UserId}, user);
        }

        [HttpPut]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            {
                if( !UserExists(id))
                {
                    return NotFound();
                } else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser( long id )
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }




    }
}
