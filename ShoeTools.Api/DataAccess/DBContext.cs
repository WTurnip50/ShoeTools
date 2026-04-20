using System.Data.Common;
using MySqlConnector;
using ShoeTools.Api.DataAccess.Interfaces;

namespace ShoeTools.Api.DataAccess;

public class DBContext : IDBContext
{
    private readonly string _connectionString = "server=localhost;user=root;password=Q.34Isrg;database=Ecommerce;port=3306;";
    
    private MySqlConnection _connection;
    
    public DbConnection Connection
    {
        get
        {
            if (_connection == null)
            {
                _connection = new MySqlConnection(_connectionString);
            }
            return _connection;
        }
    }
}