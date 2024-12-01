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
        public async Task<IActionResult> ImportUsersAsync()
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


        [HttpPost("Accounts")]
        public async Task<IActionResult> ImportAccountsAsync()
        {
            Dictionary<string, User> accountUsers = await db.Users.ToDictionaryAsync(e => e.Email);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            int accountsCount = 0;
            using (StreamReader reader = new(_pathName))
            using (CsvReader csv = new(reader, config))
            {

                IEnumerable<AllUserAndAccounts>? records = csv.GetRecords<AllUserAndAccounts>();

                foreach (AllUserAndAccounts record in records)
                {
                    if (!accountUsers.TryGetValue(record.email, out User? user))
                    {
                        Console.WriteLine($"Not found user for {record.account_number}");
                        return NotFound(record);
                    }

                    Account account = new()
                    {
                        AccountNumber = record.account_number,
                        RoutingNumber = record.routing_number,
                        Status = record.account_status,
                        Type = record.account_type,
                        Balance = record.account_balance,
                        UserId = user.UserId,
                    };

                    db.Accounts.Add(account);
                    accountsCount++;
                }
                await db.SaveChangesAsync();
            }

            return new JsonResult(accountsCount);
        }


        [HttpPost("Cards")]
        public async Task<IActionResult> ImportCardsAsync()
        {
            Dictionary<long, Account> AccountsByAccountNumber = await db.Accounts.ToDictionaryAsync(e => e.AccountNumber);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            int accountsCount = 0;
            using (StreamReader reader = new(_pathName))
            using (CsvReader csv = new(reader, config))
            {
                IEnumerable<AllUserAndAccounts>? records = csv.GetRecords<AllUserAndAccounts>();

                foreach (AllUserAndAccounts record in records)
                {
                    if (!AccountsByAccountNumber.TryGetValue(record.account_number, out Account? account))
                    {
                        Console.WriteLine($"Not found account for {record.card_number}");
                        return NotFound(record);
                    }

                    Card card = new()
                    {
                        UserId = account.UserId,
                        AccountId = account.AccountId,
                        Number = record.card_number,
                        Expiration = record.card_expiration,
                        Cvv = record.card_cvv,
                        Status = record.card_status

                    };

                    db.Cards.Add(card);
                }
                await db.SaveChangesAsync();
            }

            return new JsonResult("Updated Cards");
        }

    }
}
