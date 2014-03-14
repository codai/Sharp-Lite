using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WebApp.Domain.Entities;
using WebApp.EntityFrameworkProvider.Mapping;

namespace WebApp.EntityFrameworkProvider
{
    public partial class WebAppDbContext : DbContext
    {
        static WebAppDbContext()
        {
            Database.SetInitializer<WebAppDbContext>(null);
        }

        public WebAppDbContext()
            : base("Name=WebAppDb")
        {
        }

        public DbSet<FileType> FileTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FileTypeMap());
        }
    }
}
