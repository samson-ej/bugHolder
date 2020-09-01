using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using bugHolder.Data;


namespace bugHolder.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcBugContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcBugContext>>()))
            {
                // Look for any movies.
                if (context.Bug.Any())
                {
                    return;   // DB has been seeded
                }

                context.Bug.AddRange(
                    new Bug
                    {
                        Id = 1,
                        Title = "Create bug functionality",
                        Status = "Assigned",
                        Date = DateTime.Parse("08-31-2020")                      
                    },

                    new Bug
                    {
                        Id = 2,
                        Title = "Read or Details bug functionality",
                        Status = "Reported",
                        Date = DateTime.Parse("08-31-2020")
                    },

                    new Bug
                    {
                        Id = 3,
                        Title = "Update or Edit bug functionality",
                        Status = "Reported",
                        Date = DateTime.Parse("08-31-2020")
                    },

                    new Bug
                    {
                        Id = 4,
                        Title = "Delete bug functionality",
                        Status = "Reported",
                        Date = DateTime.Parse("08-31-2020")
                    }
                );
                context.SaveChanges();
            }
        }
    }


}

