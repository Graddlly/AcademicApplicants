using MySql.Data.MySqlClient;

namespace AcademicApplicants.DBUtils;

public class DBUtils
{
    public static MySqlConnection GetDbConnection()
    {
        const string host = "localhost";
        const int port = 3306;
        const string database = "AcademicApplicants";
        const string username = "AcademicApp";
        const string password = "7896";

        return DBMySQLUtils.GetDbConnection(host, port, database, username, password);
    }
}