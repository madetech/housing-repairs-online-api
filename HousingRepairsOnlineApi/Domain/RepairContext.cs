using Microsoft.EntityFrameworkCore;

namespace HousingRepairsOnlineApi.Domain;

public class RepairContext : DbContext
{
    public RepairContext(DbContextOptions options) : base(options) {}

    public DbSet<Repair> Repairs { get; set; }

    // TODO: Do we really want to create the tables like this???
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Repair>().ToTable("repair");
        }
}
