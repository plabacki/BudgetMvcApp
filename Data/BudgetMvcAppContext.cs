using BudgetMvcApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetMvcApp.Data;

public class BudgetMvcAppContext : DbContext
{
    public BudgetMvcAppContext(DbContextOptions<BudgetMvcAppContext> options)
        :base(options)
    {}

    public DbSet<Models.Expense> Expenses {get; set;}
    public DbSet<Models.Transaction> Transaction {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>()
            .HasMany(e => e.Expenses)
            .WithOne(t => t.Transaction)
            .HasForeignKey(t => t.TransactionId);
    }
}