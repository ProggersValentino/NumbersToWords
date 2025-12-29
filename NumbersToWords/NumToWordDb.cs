using Microsoft.EntityFrameworkCore;

namespace NumbersToWords
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
