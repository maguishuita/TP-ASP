using Pizzeria.Models;

namespace Pizzeria.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PizzeriaContext context)
        {
            // S'assurer que la base de données existe
            context.Database.EnsureCreated();

            // Vérifier s'il y a déjà des pizzas
            if (context.DegueneFallPizzas.Any())
            {
                return;   // La base de données a déjà été initialisée
            }

            // Ajouter quelques pizzas de test
            var pizzas = new DegueneFallPizza[]
            {
                new DegueneFallPizza { Nom = "Margherita", Description = "Sauce tomate, mozzarella, basilic" },
                new DegueneFallPizza { Nom = "Reine", Description = "Sauce tomate, mozzarella, jambon, champignons" },
                new DegueneFallPizza { Nom = "4 Fromages", Description = "Sauce tomate, mozzarella, chèvre, roquefort, emmental" },
                new DegueneFallPizza { Nom = "Végétarienne", Description = "Sauce tomate, mozzarella, légumes grillés, olives" }
            };

            foreach (var pizza in pizzas)
            {
                context.DegueneFallPizzas.Add(pizza);
            }

            context.SaveChanges();
        }
    }
}