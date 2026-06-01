using ColdStart.Api.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ColdStart.Api.Data;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Brand> Brands { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Engine> Engines { get; set; }
    public DbSet<Magazine> Magazines { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Enum>().HaveConversion<string>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (
            var relationship in modelBuilder
                .Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())
        )
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        modelBuilder
            .Entity<Country>()
            .HasData(
                new Country
                {
                    Id = 1,
                    Name = "Avenia",
                    Description =
                        "The \"domestic\" market in this game, Avenia is the largest and richest consumer market in the world. A long history of many iconic car brands across many segments, some of which have not stood the test of time or are showing signs of falling behind. Still, Avenian brands have some of the most loyal customers and continue to push automotive technology further, although not as much in the past.",
                    PrimaryColor = "#1b3a6b",
                    SecondaryColor = "#c8102e",
                },
                new Country
                {
                    Id = 2,
                    Name = "Gallapio",
                    Description =
                        "Dotting the sunny coastline and sprawling hills of Gallapio are some of the most iconic, revered luxury sport sedans of all time. Many have tried, but many say none have been able to best the responsive feel and handling of Gallapian engineering, which has been at the apex of motorsport for decades. A Gallapian car and its driver are here for a good time, not a long time.",
                    PrimaryColor = "#c8102e",
                    SecondaryColor = "#d4af37",
                },
                new Country
                {
                    Id = 3,
                    Name = "Sivoe",
                    Description =
                        "A tiny but mighty manufacturing powerhouse, Sivoe can do a lot more than mass-produce cars, but it's still what they're best known for. You will see quite a few Sivoen cars on Avenian roads thanks to their long history of inexpensive, value-focused, practical vehicles. Sure, some of them may look cheap and have a reputation as beater cars that just won't quit, but recent models from the biggest Sivoen brands show enhanced, modern styling and premium interiors while still keeping the prices competitive, and some compelling luxury brands have even started popping up.",
                    PrimaryColor = "#cc0000",
                    SecondaryColor = "#c0c0c0",
                }
            );
    }
}
