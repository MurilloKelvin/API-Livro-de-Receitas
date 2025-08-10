using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecipeBook.Infrastructure.DataAcces.Repositories;

public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
{
    private readonly MyRecipeBookDbContext _dbContext;
    private IUserReadOnlyRepository _userReadOnlyRepositoryImplementation;

    public UserRepository(MyRecipeBookDbContext dbContext) => _dbContext = dbContext;
    
    // adiciona um usuário ao banco de dados assíncronamente
    public async Task Add(User user) => await _dbContext.Users.AddAsync(user);

    public async Task<bool> ExistActiveUserWithEmail(string email) => await _dbContext.Users.AnyAsync(u => u.Email == email && u.IsActive);
}