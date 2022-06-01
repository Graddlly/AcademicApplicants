using System.Collections.ObjectModel;
using System.Data.Common;
using AcademicApplicants.Models;
using MySql.Data.MySqlClient;

namespace AcademicApplicants.Queries;

public class QueryGroups : ObservableCollection<Groups>
{
    private void AddGroup(int GroupID, string? GroupName)
    {
        Add(new Groups
        {
            GroupID = GroupID,
            GroupName = GroupName
        });
    }

    public QueryGroups()
    {
        using var connection = DBUtils.DBUtils.GetDbConnection();
        connection.Open();

        const string sql = "SELECT * FROM Groups";
        var cmd = new MySqlCommand(sql, connection);

        using DbDataReader reader = cmd.ExecuteReader();
        if (!reader.HasRows) return;
        while (reader.Read())
        {
            AddGroup(
                reader.GetInt32(0),
                reader.GetString(1));
        }
        connection.Close();
    }
}