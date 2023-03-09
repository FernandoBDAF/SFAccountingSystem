using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SFAccountingSystem.Models;

namespace SFAccountingSystem
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) 
		{
		}

		public DbSet<Intermediation> Intermediation { get; set; }

		public DbSet<RecordOFX> RecordOFX { get; set; }

		public DbSet<RecordOFXSubGroup> RecordOFXSubGroups { get; set; }

		public DbSet<User> Users { get; set; }

	}
}
