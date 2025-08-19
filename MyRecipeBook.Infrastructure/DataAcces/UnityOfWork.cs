using MyRecipeBook.Infrastructure.DataAcces.Repositories;

namespace MyRecipeBook.Infrastructure.DataAcces;

public class UnityOfWork : IUnityOfWork
{
    private readonly MyRecipeBookDbContext _dbContext;
    
    public UnityOfWork(MyRecipeBookDbContext dbContext) => _dbContext = dbContext;
    
    public async Task Commit() => await _dbContext.SaveChangesAsync(); 
}