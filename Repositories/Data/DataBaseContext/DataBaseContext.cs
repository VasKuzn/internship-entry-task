using System.Reflection;

namespace internship_entry_task.Repositories.Data.DataBaseContext
{
    public class DataBaseContext : DbContext
    {
        public DbSet<GameModel> Games { get; set; }

        public DataBaseContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}