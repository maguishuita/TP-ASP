using Pizzeria.Models;

namespace Pizzeria.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PizzeriaContext context)
        {
            // S'assurer que la base de donn�es existe
            context.Database.EnsureCreated();

            // V�rifier s'il y a d�j� des pizzas
            if (context.DegueneFallPizzas.Any())
            {
                return;   // La base de donn�es a d�j� �t� initialis�e
            }

            // Ajouter quelques pizzas de test
            var pizzas = new DegueneFallPizza[]
            {
                new DegueneFallPizza { Nom = "Margherita", Description = "Sauce tomate, mozzarella, basilic" },
                new DegueneFallPizza { Nom = "Reine", Description = "Sauce tomate, mozzarella, jambon, champignons" },
                new DegueneFallPizza { Nom = "4 Fromages", Description = "Sauce tomate, mozzarella, ch�vre, roquefort, emmental" },
                new DegueneFallPizza { Nom = "V�g�tarienne", Description = "Sauce tomate, mozzarella, l�gumes grill�s, olives" }
            };

            foreach (var pizza in pizzas)
            {
                context.DegueneFallPizzas.Add(pizza);
            }

            context.SaveChanges();
        }
    }
}