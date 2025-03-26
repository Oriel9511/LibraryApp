using System.Runtime.InteropServices;
using LibraryApp.Domain.Constants;
using LibraryApp.Domain.Entities;
using LibraryApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LibraryApp.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole(Roles.Administrator);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        // Default data
        // Seed, if necessary
        if (!_context.Authors.Any())
        {
            var horrorGenre = new Genre()
            {
                Name = "Horror",
                Description = "Fiction designed to frighten and unsettle"
            };

            var magicalRealismGenre = new Genre()
            {
                Name = "Magical Realism",
                Description = "A style of fiction that paints a realistic view of the world while also adding magical elements"
            };

            var fantasyGenre = new Genre()
            {
                Name = "Fantasy",
                Description = "Fiction involving magical and supernatural elements"
            };

            _context.Authors.Add(new Author()
            {
                Name = "Stephen King",
                Nationality = "American",
                Birthday = new DateTime(1947, 9, 21),
                Books = 
                {
                    new Book()
                    {
                        Title = "The Shining", 
                        Resume = "A family becomes caretakers of an isolated hotel for the winter where a sinister presence influences the father into violence.", 
                        Genre = horrorGenre,
                        PublicationYear = 1977,
                        ISBN = "978-030774",
                        Stock = 10,
                        ImageUrl = "the-shining.jpg",
                    },
                    new Book()
                    {
                        Title = "It", 
                        Resume = "Seven adults return to their hometown to confront a nightmare they first stumbled on as teenagers.", 
                        Genre = horrorGenre,
                        PublicationYear = 1986,
                        ISBN = "978-150114",
                        Stock = 8,
                        ImageUrl = "it.jpg",
                    }
                }
            });

            _context.Authors.Add(new Author()
            {
                Name = "Gabriel García Márquez",
                Nationality = "Colombian",
                Birthday = new DateTime(1927, 3, 6),
                Books = 
                {
                    new Book()
                    {
                        Title = "One Hundred Years of Solitude", 
                        Resume = "The multi-generational story of the Buendía family in the fictional town of Macondo.", 
                        Genre = magicalRealismGenre,
                        PublicationYear = 1967,
                        ISBN = "978-006088",
                        Stock = 15,
                        ImageUrl = "hundred-years-solitude.jpg",
                    }
                }
            });

            _context.Authors.Add(new Author()
            {
                Name = "J.K. Rowling",
                Nationality = "British",
                Birthday = new DateTime(1965, 7, 31),
                Books = 
                {
                    new Book()
                    {
                        Title = "Harry Potter and the Philosopher's Stone", 
                        Resume = "An orphaned boy learns he is a wizard and begins his journey at Hogwarts School of Witchcraft and Wizardry.", 
                        Genre = fantasyGenre,
                        PublicationYear = 1997,
                        ISBN = "978-074753",
                        Stock = 20,
                        ImageUrl = "harry-potter-1.jpg",
                    },
                    new Book()
                    {
                        Title = "Harry Potter and the Chamber of Secrets", 
                        Resume = "Harry's second year at Hogwarts is threatened by a mysterious force that is attacking students.", 
                        Genre = fantasyGenre,
                        PublicationYear = 1998,
                        ISBN = "978-074753",
                        Stock = 18,
                        ImageUrl = "harry-potter-2.jpg",
                    }
                }
            });
            
            await _context.SaveChangesAsync();
        }
    }
}
