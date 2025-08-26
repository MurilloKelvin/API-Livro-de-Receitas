using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAcces;
using MyRecipeBook.Infrastructure.DataAcces.Repositories;
using MyRecipeBook.Infrastructure.Extensions;

namespace MyRecipeBook.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration )
    {
        var databaseType = configuration.DatabaseType(); //recupera o tipo de banco de dados do appsettings.development.json

        AddDbContext(services, configuration); //chama o serviço de contexto do banco de dados
        AddRepositories(services); //chama os serviços de repositório
    }
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString(); // string de conexão com o banco de dados MySQL >> appsettings.development.json
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 43)); // versão do MySQL Server
        
        services.AddDbContext<MyRecipeBookDbContext>(dbContextOptions =>
        {
            dbContextOptions.UseMySql(connectionString, serverVersion);
        });
    }
    private static void AddRepositories(IServiceCollection services)
    {
        //configura os repositórios de usuário com injeção de dependência

        services.AddScoped<IUnityOfWork, UnityOfWork>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
    }
}