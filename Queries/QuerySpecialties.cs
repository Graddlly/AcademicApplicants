using System.Collections.ObjectModel;
using System.Data.Common;
using AcademicApplicants.Models;
using MySql.Data.MySqlClient;

namespace AcademicApplicants.Queries;

public class QuerySpecialties : ObservableCollection<Specialties>
{
    private void AddSpecialties(int SpecialityID, string? SpecialtyName)
    {
        Add(new Specialties
        {
            SpecialtyName = SpecialtyName, 
            SpecialityID = SpecialityID
        });
    }

    public QuerySpecialties()
    {
        using var connection = DBUtils.DBUtils.GetDbConnection();
        connection.Open();

        const string sql = "SELECT * FROM Specialties";
        var cmd = new MySqlCommand(sql, connection);

        using DbDataReader reader = cmd.ExecuteReader();
        if (!reader.HasRows) return;
        while (reader.Read())
        {
            AddSpecialties(
                reader.GetInt32(0),
                reader.GetString(1));
        }
        connection.Close();
    }
}