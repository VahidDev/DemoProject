using Microsoft.EntityFrameworkCore;
using Project.Core.Entities.Models;
using Project.Core.Settings;
using System.Reflection;

namespace Project.Infrastructure.DAL
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(AppSettings.Settings.AppDbConnectionModel.ToString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SetNoKeyForProceduresAndFunctions(modelBuilder);
        }

        private void SetNoKeyForProceduresAndFunctions(ModelBuilder modelBuilder)
        {
            var spTypes = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Name.StartsWith("SP_")
                            || t.Name.StartsWith("FN_"))
                .ToHashSet();

            var orderedTypes = new List<Type>();

            while (spTypes.Count > 0)
            {
                foreach (var spType in spTypes)
                {
                    var isParentClass = spTypes.Any(spTypeInner => spTypeInner.IsSubclassOf(spType));

                    if (isParentClass)
                    {
                        continue;
                    }

                    orderedTypes.Add(spType);
                    spTypes.Remove(spType);
                }
            }

            foreach (var spType in orderedTypes)
            {
                modelBuilder.Entity(spType).HasNoKey();
            }
        }

        public DbSet<Loan> Loans { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
