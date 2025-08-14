public class ATMContext : DbContext {
    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public ATMContext(DbContextOptions<ATMContext> options) : base(options) { }
}

