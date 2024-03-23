using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace com.faistdevelopments.AbstractEfCore;

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
