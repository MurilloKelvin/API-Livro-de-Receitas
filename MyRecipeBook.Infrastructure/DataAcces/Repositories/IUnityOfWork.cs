namespace MyRecipeBook.Infrastructure.DataAcces.Repositories;

public interface IUnityOfWork
{
    public Task Commit();
}
