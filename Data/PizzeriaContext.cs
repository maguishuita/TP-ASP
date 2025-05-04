using Microsoft.EntityFrameworkCore;
using Pizzeria.Models;

namespace Pizzeria.Data
{
    public class PizzeriaContext : DbContext
    {
        public PizzeriaContext(DbContextOptions<PizzeriaContext> options)
            : base(options)
        {
        }

        public DbSet<DegueneFallPizza> DegueneFallPizzas { get; set; } 
    }
}
