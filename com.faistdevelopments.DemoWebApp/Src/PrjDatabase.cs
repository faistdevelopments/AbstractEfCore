using com.faistdevelopments.AbstractEfCore;

namespace com.faistdevelopments.DemoWebApp;

public class PrjDatabase : MySqlDatabase
{
    private static readonly string CONNECTION_STRING = MySqlConnectionStringBuilder.Create() //
        .SetHostname("localhost") //
        .SetUsername("root") //
        .SetPassword("12345678") //
        .SetDatabaseName("DeepOceanTest") //
        .Build();

    public PrjDatabase() : base(CONNECTION_STRING)
    {
    }
}
