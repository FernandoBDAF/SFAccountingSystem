using Microsoft.EntityFrameworkCore;

namespace SFAccountingSystem
{
	public class DataContext : DbContext
	{
		DataContext(DbContextOptions<DataContext> options) : base(options) 
		{
			// Add variables for each table public DbSet<class> Class { get; set; }
		}
	}
}
