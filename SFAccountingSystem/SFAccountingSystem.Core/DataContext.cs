using Microsoft.EntityFrameworkCore;
using SFAccountingSystem.Core.Models;

namespace SFAccountingSystem.Core
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // nao tinha anotado
        {
            modelBuilder
                .Entity<RecordOFX>() //classe a ser ajustado o decimal
                .Property(e => e.Value)
                .HasPrecision(18, 2);
            base.OnModelCreating(modelBuilder); // todo codigo acima disso
        }

        public DbSet<Intermediation> Intermediation { get; set; }

        public DbSet<RecordOFX> RecordOFX { get; set; }

        public DbSet<RecordOFXSubGroup> RecordOFXSubGroups { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Invoice> Invoice { get; set; }
    }
}
