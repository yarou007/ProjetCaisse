using Microsoft.EntityFrameworkCore;

namespace CustomAth.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Category> Category { get; set; }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<LigneTicket> LigneTickets { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 
        modelBuilder.Entity<Role>()
            .HasMany(r => r.UserAccounts)
            .WithOne(u => u.Role)
            .HasForeignKey(u => u.RoleId);
    }
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseMySql(
    //         "Server=127.0.0.1;Port=3306;Database=CustomAth;User=manager;Password=manager;",
    //         ServerVersion.AutoDetect("Server=127.0.0.1;Port=3306;Database=CustomAth;User=manager;Password=manager;"));
    // }
}