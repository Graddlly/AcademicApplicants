using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using AcademicApplicants.Models;
using MySql.Data.MySqlClient;

// ReSharper disable InconsistentNaming

namespace AcademicApplicants.Queries;

public class QueryPassports : ObservableCollection<Passports>
{
    private void AddPassports(int PassportID, string WhoIssued, string? WhenIssued, long SNumber)
    {
        Add(new Passports
        {
            PassportID = PassportID, 
            WhoIssued = WhoIssued, 
            WhenIssued = WhenIssued, 
            SNumber = SNumber
        });
    }
    
    public void ClearList() { ClearItems(); }
    
    public QueryPassports()
    {
        using var connection = DBUtils.DBUtils.GetDbConnection();
        connection.Open();

        const string sql = "SELECT * FROM Passports";
        var cmd = new MySqlCommand(sql, connection);

        using DbDataReader reader = cmd.ExecuteReader();
        if (!reader.HasRows) return;
        while (reader.Read())
        {
            AddPassports(
                reader.GetInt32(0),
                reader.GetString(1),
                Convert.ToString(DateOnly.FromDateTime(reader.GetDateTime(2))),
                reader.GetInt64(3));
        }
        connection.Close();
    }
}