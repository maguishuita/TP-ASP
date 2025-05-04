using Microsoft.EntityFrameworkCore;
using Pizzeria.Data;
using Pizzeria.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuration pour SQLite uniquement
var connectionString = builder.Configuration.GetConnectionString("PizzeriaContext") ?? "Data Source=Pizzas.db";

// Ajouter les services au conteneur
builder.Services.AddControllersWithViews();

// SQLite uniquement
builder.Services.AddDbContext<PizzeriaContext>(options =>
    options.UseSqlite(connectionString));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Ajout du modèle personnalisé
builder.Services.AddScoped<PizzeriaContext>();

var app = builder.Build();

// Initialiser la base de données
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<PizzeriaContext>();
        context.Database.EnsureCreated();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erreur DB init: {ex.Message}");
    }
}

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// API minimale
app.MapGet("/api/pizzas", async (PizzeriaContext db) =>
    await db.DegueneFallPizzas.ToListAsync());

app.MapGet("/api/pizzas/{id}", async (PizzeriaContext db, int id) =>
    await db.DegueneFallPizzas.FindAsync(id) is DegueneFallPizza pizza
        ? Results.Ok(pizza)
        : Results.NotFound());

app.MapPost("/api/pizzas", async (PizzeriaContext db, DegueneFallPizza pizza) =>
{
    db.DegueneFallPizzas.Add(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/api/pizzas/{pizza.Id}", pizza);
});

app.MapPut("/api/pizzas/{id}", async (PizzeriaContext db, int id, DegueneFallPizza inputPizza) =>
{
    var pizza = await db.DegueneFallPizzas.FindAsync(id);
    if (pizza == null) return Results.NotFound();

    pizza.Nom = inputPizza.Nom;
    pizza.Description = inputPizza.Description;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/api/pizzas/{id}", async (PizzeriaContext db, int id) =>
{
    var pizza = await db.DegueneFallPizzas.FindAsync(id);
    if (pizza == null) return Results.NotFound();

    db.DegueneFallPizzas.Remove(pizza);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();