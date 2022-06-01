using MySql.Data.MySqlClient;

namespace AcademicApplicants.DBUtils;

public class DBMySQLUtils
{
    public static MySqlConnection
        GetDbConnection(string host, int port, string database, string username, string password)
        {
            var connString = $"Server={host};Database={database};port={port};userid={username};password={password};charset=utf8";
            var conn = new MySqlConnection(connString);
            return conn;
        }
}