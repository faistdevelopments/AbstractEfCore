namespace com.faistdevelopments.AbstractEfCore;

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
