using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Infrastructure.DataAcces;

public class MyRecipeBookDbContext : DbContext
{
    public MyRecipeBookDbContext(DbContextOptions options) : base(options) { } // construtor que recebe as opções de configuração do DbContext
    
    public DbSet<User> Users { get; set; } // existe uma tabela Users no banco de dados

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyRecipeBookDbContext).Assembly);
    } 
}