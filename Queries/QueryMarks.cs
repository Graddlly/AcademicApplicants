using System.Collections.ObjectModel;
using System.Data.Common;
using AcademicApplicants.Models;
using MySql.Data.MySqlClient;

// ReSharper disable InconsistentNaming

namespace AcademicApplicants.Queries;

public class QueryMarks : ObservableCollection<Marks>
{
    private void AddMarks(int MarkID, int ApplicantID,
        int Exam1, int Exam2, int Exam3,
        float AverageMark)
    {
        Add(new Marks
        {
           MarkID = MarkID,
           ApplicantID = ApplicantID,
           Exam1 = Exam1,
           Exam2 = Exam2,
           Exam3 = Exam3,
           AverageMark = AverageMark
        });
    }

    public QueryMarks()
    {
        using var connection = DBUtils.DBUtils.GetDbConnection();
        connection.Open();

        const string sql = "SELECT * FROM Marks";
        var cmd = new MySqlCommand(sql, connection);

        using DbDataReader reader = cmd.ExecuteReader();
        if (!reader.HasRows) return;
        while (reader.Read())
        {
            AddMarks(
                reader.GetInt32(0),
                reader.GetInt32(1),
                reader.GetInt32(2),
                reader.GetInt32(3),
                reader.GetInt32(4),
                reader.GetFloat(5));
        }
        connection.Close();
    }
}