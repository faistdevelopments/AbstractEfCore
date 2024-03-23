namespace com.faistdevelopments.AbstractEfCore;

/// <summary>
/// Base class for connection string builders
/// </summary>
public abstract class DbConnectionStringBuilder
{
    protected string? Hostname;
    protected string? Username;
    protected string? Password;
    protected string? DatabaseName;

    public abstract string Build();

    public DbConnectionStringBuilder SetHostname(string hostname)
    {
        this.Hostname = hostname;
        return this;
    }

    public DbConnectionStringBuilder SetUsername(string username)
    {
        this.Username = username;
        return this;
    }

    public DbConnectionStringBuilder SetPassword(string password)
    {
        this.Password = password;
        return this;
    }

    public DbConnectionStringBuilder SetDatabaseName(string databaseName)
    {
        this.DatabaseName = databaseName;
        return this;
    }
}
