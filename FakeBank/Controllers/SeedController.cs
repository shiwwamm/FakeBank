using CsvHelper;
using CsvHelper.Configuration;
using DataModel;
using FakeBank.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FakeBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController ( UserAccountDbContext db, IHostEnvironment environment ) : ControllerBase
    {
        private readonly string _pathName = Path.Combine(environment.ContentRootPath, "./Data/fake_bank_data.csv");

        [HttpPost("Users")]
        public async Task<IActionResult> ImportAccountUsersAsync()
        {
            Dictionary<string, User> usersByEmail = db.Users.AsNoTracking().ToDictionary(e => e.Email, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            using StreamReader reader = new(_pathName);
            using CsvReader csv = new(reader, config);

            List<AllUserAndAccounts> records = csv.GetRecords<AllUserAndAccounts>().ToList();

            foreach (AllUserAndAccounts record in records)
            {

                if (usersByEmail.ContainsKey(record.email))
                {
                    continue;
                }

                User user = new()
                {
                    FirstName = record.first_name,
                    LastName = record.last_name,
                    Email = record.email,
                    Number = record.phone_number,
                };

                await db.Users.AddAsync(user);
                usersByEmail.Add(record.email, user);
            }

            await db.SaveChangesAsync();

            return new JsonResult(usersByEmail.Count);
        }

        public async Task<IActionResult> ImportAccountsAsync()
        {
            Dictionary<String, User> accountUsers = await db.Users.ToDictionaryAsync(e => e.Email);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            int accountsCount = 0;
            using (StreamReader reader = new(_pathName))
            using (CsvReader csv = new(reader, config)) {

                IEnumerable<AllUserAndAccounts>? records = csv.GetRecords<AllUserAndAccounts>();
                
                foreach (AllUserAndAccounts record in records) {

            }


        }

    }
}
