using Microsoft.EntityFrameworkCore;
using bugHolder.Models;

namespace bugHolder.Data
{
    public class MvcBugContext : DbContext
    {
        public MvcBugContext(DbContextOptions<MvcBugContext> options)
            : base(options)
        {
        }

        public DbSet<Bug> Bug { get; set; }
    }
}