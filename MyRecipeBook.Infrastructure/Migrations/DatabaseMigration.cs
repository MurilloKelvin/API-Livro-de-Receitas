using Dapper;
using MySqlConnector;
using MyRecipeBook.Domain.Enums;

namespace MyRecipeBook.Infrastructure.Migrations
{
    public static class DatabaseMigration
    {
        public static void Migrate(DatabaseType databaseType, string connectionString)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString); // construtor

            var databaseName = connectionStringBuilder.Database; // recupero o constructor

            connectionStringBuilder.Remove("Database"); // removo o nome do banco de dados


            using var dbConnection = new MySqlConnection(connectionStringBuilder.ConnectionString);

            var parameters = new DynamicParameters();
            parameters.Add("name", databaseName); // Parâmetro para o nome do banco de dados

            var records = dbConnection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @name", parameters);

            if(!records.Any())
                dbConnection.Execute($"CREATE DATABASE {databaseName}");  // se nao existe a tabela, cria ela
        }

        private static  void EnsureMySqlDatabase(string connectionString)
        {
            // Lógica para garantir que o banco de dados MySQL esteja criado
            // Pode incluir a criação do banco de dados se ele não existir
        }

    }
}
