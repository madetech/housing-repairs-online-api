using Microsoft.EntityFrameworkCore;

namespace HousingRepairsOnlineApi.Domain;

public class RepairContext : DbContext
{
    public RepairContext(DbContextOptions options) : base(options) { }

    public DbSet<Repair> Repairs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Repair>().ToTable("repair").Property(r => r.CreatedAt).HasDefaultValueSql("now()");
    }
}
