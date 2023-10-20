using System;
using System.Collections.Generic;
using System.Linq;
using SceletonAPI.Domain.Entities;

namespace SceletonAPI.Infrastructure.Persistences
{
    public class WAInitializer
    {

        public static void Initialize(DBContext context)
        {
            var isDevelopment = string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), "development", StringComparison.InvariantCultureIgnoreCase);
            if (isDevelopment)
            {
                var initalizer = new WAInitializer();
                initalizer.Seed(context);
            }
        }

        public void Seed(DBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return; // Db has been seeded
            }   
            //SeedUsers(context);
        }

        // private void SeedUsers(WADbContext context)
        // {
        //     var user = new User
        //     {
        //         Email = "123@bsi.co.id",
        //         Phone = "085648721439",
        //         Company = "BSI",
        //         Name = "HAYOLOOO"
        //     };

        //     context.Users.Add(user);
        //     context.SaveChanges();
        // }
    }
}
