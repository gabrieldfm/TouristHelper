using Microsoft.EntityFrameworkCore;
using TouristHelper.Domain.Entities;

namespace TouristHelper.Infrastructure.ApplicationContext;

public class TouristHelperDbContext(DbContextOptions<TouristHelperDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}
