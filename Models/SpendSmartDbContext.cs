
//using Microsoft.EntityFrameworkCore;

//namespace ExpenseTracker.Models
//{
//    public class SpendSmartDbContext : DbContext
//    {
//        public SpendSmartDbContext(DbContextOptions<SpendSmartDbContext> options) : base(options) { }

//        public DbSet<Expense> Expenses { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Expense>().ToTable("Expenses");
//        }
//    }
//}
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Models
{
    public class SpendSmartDbContext : DbContext
    {
        public SpendSmartDbContext(DbContextOptions<SpendSmartDbContext> options) : base(options) { }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExpenseView> ExpenseViews { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>().ToTable("Expenses");
            modelBuilder.Entity<Currency>().HasNoKey().ToTable("Master");
            modelBuilder.Entity<ExpenseView>().HasNoKey().ToView("ExpenseView"); 
        }
    }
}
