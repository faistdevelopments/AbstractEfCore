using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace com.faistdevelopments.AbstractEfCore;

/// <summary>
/// Custom implementation to be used for MySql databases
/// </summary>
public class MySqlDatabase : AbstractDatabase
{
    public MySqlDatabase(string connectionString) : base(connectionString, Assembly.GetCallingAssembly())
    {
    }

    protected override void OnSpecificConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(this.ConnectionString);
    }
}
