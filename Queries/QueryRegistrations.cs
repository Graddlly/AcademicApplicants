using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using AcademicApplicants.Models;
using MySql.Data.MySqlClient;

// ReSharper disable InconsistentNaming

namespace AcademicApplicants.Queries;

public class QueryRegistrations : ObservableCollection<Registrarions>
{
    private void AddRegistrations(int RegistrationID, int ApplicantID,
        int GroupID, int FacultyID, int SpecialityID)
    {
        Add(new Registrarions
        {
            RegistrationID = RegistrationID, 
            ApplicantID = ApplicantID, 
            GroupID = GroupID, 
            FacultyID = FacultyID,
            SpecialityID = SpecialityID
        });
    }

    public QueryRegistrations()
    {
        using var connection = DBUtils.DBUtils.GetDbConnection();
        connection.Open();

        const string sql = "SELECT * FROM Registrations";
        var cmd = new MySqlCommand(sql, connection);

        using DbDataReader reader = cmd.ExecuteReader();
        if (!reader.HasRows) return;
        while (reader.Read())
        {
            AddRegistrations(
                reader.GetInt32(0),
                reader.GetInt32(1),
                reader.GetInt32(2),
                reader.GetInt32(3),
                reader.GetInt32(4));
        }
        connection.Close();
    }
}