using Microsoft.EntityFrameworkCore;

public class BankContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }

    public BankContext(DbContextOptions<BankContext> options)
        : base(options)
    { }
}
