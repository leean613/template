using Entities.Buffalo;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EntityFrameworkCore.Contexts
{
    public static class DbInitializer
    {
        public static void Initialize(AlcareDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            context.Users.Add(new User
            {
                Id = Guid.NewGuid(),
                IsAlive = false,
                CreatedDate = DateTime.Now,
                Email = "admin",
                Password = EncryptPassword("1"),
                UserName = "admin",
                DayOfBirth = Convert.ToDateTime("2020/12/09"),
                Gender = true,
                PhoneNumber = "0932491054",
                IsActive = true
            });

            context.SaveChanges();
        }

        public static string EncryptPassword(string passsword)
        {
            return Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(passsword)));
        }
    }
}
