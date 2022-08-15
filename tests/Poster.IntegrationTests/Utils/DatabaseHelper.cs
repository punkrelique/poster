using Poster.Domain;
using Poster.Infrastructure;

namespace Poster.IntegrationTests.Utils;

public static class DatabaseHelper
{
    public static void SeedData(IApplicationDbContext context)
    {
        AddUser(context);

        context.SaveChanges();
    }

    private static void AddUser(IApplicationDbContext context)
    {
        context.Users.Add(new User
        {
            UserName = "testUser",
            Email = "test@test.ru",
            PasswordHash = "test"
        });
    }
}