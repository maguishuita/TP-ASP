using Pizzeria.Models;

namespace Pizzeria.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PizzeriaContext context)
        {
            context.Database.EnsureCreated(); // Création si pas encore existante

            if (context.DegueneFallPizzas.Any())
                return; // Données déjà insérées

            var pizzas = new DegueneFallPizza[]
            {
                new DegueneFallPizza { Nom = "Margherita", Description = "Tomate, mozzarella, basilic" },
                new DegueneFallPizza { Nom = "Reine", Description = "Jambon, champignons, fromage" }
            };

            context.DegueneFallPizzas.AddRange(pizzas);
            context.SaveChanges();
        }
    }
}
