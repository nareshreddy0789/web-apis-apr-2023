using Microsoft.EntityFrameworkCore;

namespace HrApi.Domain;

public class HrDataContext : DbContext
{
    public HrDataContext(DbContextOptions<HrDataContext> options): base(options)
    {
        
    }

    // All of the entity classes it should track in the database
    public DbSet<DepartmentEntity> Departments { get; set; }
    public DbSet<HiringRequestEntity> HiringRequests { get; set; }

    public IQueryable<DepartmentEntity> GetActiveDepartments()
    {
        return Departments.Where(d => d.Removed == false);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DepartmentEntity>().Property(p => p.Name)
            .HasMaxLength(20);

        modelBuilder.Entity<DepartmentEntity>()
            .HasIndex(b => b.Name).IsUnique();

    }
}
