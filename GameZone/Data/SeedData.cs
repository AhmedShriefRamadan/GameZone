using GameZone.Models;

namespace GameZone.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        bool stateChanged = false;

        // Ensure DB is created
        context.Database.EnsureCreated();

        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Category { Name = "Sports" },
                new Category { Name = "Action" },
                new Category { Name = "Adventure" },
                new Category { Name = "Racing" },
                new Category { Name = "Fighting" },
                new Category { Name = "Film" }
            );
            stateChanged = true;
        }

        if (!context.Devices.Any())
        {
            context.Devices.AddRange(
                new Device { Name = "PlayStation", Icon = "bi bi-playstation" },
                new Device { Name = "Xbox", Icon = "bi bi-xbox" },
                new Device { Name = "Nintendo Switch", Icon = "bi bi-nintendo-switch" },
                new Device { Name = "PC", Icon = "bi bi-pc-display" }
            );
            stateChanged = true;
        }

        if (stateChanged)
        {
            context.SaveChanges();
            Console.WriteLine("The DB has been seeded.");
        }
    }
}
