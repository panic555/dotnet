using DbUp;
using MySql.Data.MySqlClient;

namespace DotNetTest.Util;

//Unnecessary, ignore
public class DatabaseMigration
{
    public static void ApplyMigrations(string connection, string scriptPath)
    {
        var upgrader = DeployChanges.To.MySqlDatabase(connection)
            .WithScriptsFromFileSystem(scriptPath)
            .LogToConsole()
            .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.WriteLine("Database building failed.");
        }
    }
    
    public static bool TestConnection(string connectionString)
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}