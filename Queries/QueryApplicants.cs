using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using AcademicApplicants.Models;
using MySql.Data.MySqlClient;

// ReSharper disable InconsistentNaming

namespace AcademicApplicants.Queries;

public class QueryApplicants : ObservableCollection<Applicant>
{
    private void AddApplicant(int ApplicantID, string FullName, string? Birthday, 
                                int PassportID, string Institution, string? EndInstitution,
                                bool GoldMedal, string CertificateID)
    {
        Add(new Applicant
        {
            ApplicantID = ApplicantID, 
            FullName = FullName, 
            Birthday = Birthday, 
            PassportID = PassportID,
            Institution = Institution, 
            EndInstitution = EndInstitution,
            GoldMedal = GoldMedal, 
            CertificateID = CertificateID
                                
        });
    }

    public QueryApplicants()
    {
        using var connection = DBUtils.DBUtils.GetDbConnection();
        connection.Open();

        const string sql = "SELECT * FROM Applicants";
        var cmd = new MySqlCommand(sql, connection);

        using DbDataReader reader = cmd.ExecuteReader();
        if (!reader.HasRows) return;
        while (reader.Read())
        {
            AddApplicant(
                reader.GetInt32(0),
                reader.GetString(1),
                Convert.ToString(DateOnly.FromDateTime(reader.GetDateTime(2))),
                reader.GetInt32(3),
                reader.GetString(4),
                Convert.ToString(DateOnly.FromDateTime(reader.GetDateTime(5))),
                reader.GetBoolean(6),
                reader.GetString(7)
            );
        }
        connection.Close();
    }
}