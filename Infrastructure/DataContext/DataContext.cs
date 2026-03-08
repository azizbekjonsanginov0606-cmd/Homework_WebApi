using Npgsql;

namespace Infrastructure.DataContext;

public class DataContext
{
    private const string connectionString = @"Host=localhost;
                                            Port=5433;
                                            Username=hacker;
                                            Database=yalla_delivery;
                                            Password=200606;";

    public NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(connectionString);
    }
}
