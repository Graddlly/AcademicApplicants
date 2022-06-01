using System.Collections.ObjectModel;
using System.Data.Common;
using AcademicApplicants.Models;
using MySql.Data.MySqlClient;

// ReSharper disable InconsistentNaming

namespace AcademicApplicants.Queries;

public class QueryFaculties : ObservableCollection<Faculties>
{
    private void AddFaculties(int FacultyID, string? FacultyName)
    {
        Add(new Faculties
        {
            FacultyID = FacultyID,
            FacultyName = FacultyName
        });
    }

    public QueryFaculties()
    {
        using var connection = DBUtils.DBUtils.GetDbConnection();
        connection.Open();

        const string sql = "SELECT * FROM Faculties";
        var cmd = new MySqlCommand(sql, connection);

        using DbDataReader reader = cmd.ExecuteReader();
        if (!reader.HasRows) return;
        while (reader.Read())
        {
            AddFaculties(
                reader.GetInt32(0),
                reader.GetString(1));
        }
        connection.Close();
    }
}