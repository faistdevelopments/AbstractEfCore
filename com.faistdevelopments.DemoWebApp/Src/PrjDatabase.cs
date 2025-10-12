using com.faistdevelopments.AbstractEfCore;

namespace com.faistdevelopments.DemoWebApp;

public class PrjDatabase : MySqlDatabase
{
    private static readonly string CONNECTION_STRING = AppSettings.GetDbConnectionString("localhost");

    public PrjDatabase() : base(CONNECTION_STRING)
    {
    }
}
