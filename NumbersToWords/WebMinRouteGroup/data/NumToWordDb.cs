using Microsoft.EntityFrameworkCore;

namespace NumbersToWords.WebMinRouteGroup.data
{
    public class NumToWordDb : DbContext
    {
        public NumToWordDb(DbContextOptions options) : base(options)
        {
        }

        public DbSet<NumToWord> NumToWords { get; set; }

        protected NumToWordDb()
        {
        }
    }
}
