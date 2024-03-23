namespace com.faistdevelopments.AbstractEfCore;

/// <summary>
/// Build the connection string for MySql databases
/// </summary>
public class MySqlConnectionStringBuilder : DbConnectionStringBuilder
{
    public static MySqlConnectionStringBuilder Create()
    {
        return new MySqlConnectionStringBuilder();
    }

    public override string Build()
    {
        return "server=" + Hostname + ";uid=" + Username + ";pwd=" + Password + ";database=" + DatabaseName + "";
    }
}
