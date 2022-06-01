using System;
using System.IO;
using AcademicApplicants.Models;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Models;
using MySql.Data.MySqlClient;

namespace AcademicApplicants.Queries;

public class RemoveApplicant
{
    // ReSharper disable once FieldCanBeMadeReadOnly.Local
    private int _passportId;
    
    // ReSharper disable once MemberCanBeMadeStatic.Global
    public void Init() {}

    public RemoveApplicant(Applicant applicant)
    {
        using var connection = DBUtils.DBUtils.GetDbConnection();
        connection.Open();

        var cmdRegistration = new MySqlCommand();
        cmdRegistration.CommandText = $"DELETE FROM Registrations WHERE ApplicantID = {applicant.ApplicantID}";
        cmdRegistration.Connection = connection;
        cmdRegistration.ExecuteNonQuery();
        Console.WriteLine($"Регистрация абитуриента с ID {applicant.ApplicantID} успешна удалена!");

        var cmdMark = new MySqlCommand();
        cmdMark.CommandText = $"DELETE FROM Marks WHERE ApplicantID = {applicant.ApplicantID}";
        cmdMark.Connection = connection;
        cmdMark.ExecuteNonQuery();
        Console.WriteLine($"Оценки абитуриента с ID {applicant.ApplicantID} успешно удалены!");

        var cmdGetPassportIdSql = $"SELECT PassportID FROM Applicants WHERE ApplicantID = {applicant.ApplicantID}";
        var cmdGetPassportId = new MySqlCommand(cmdGetPassportIdSql, connection);
        var passportIdText = cmdGetPassportId.ExecuteReader();
        if (!passportIdText.HasRows) return;
        while (passportIdText.Read())
        {
            passportIdText.Read();
            _passportId = Convert.ToInt32(passportIdText.GetString(0));
        }
        passportIdText.Close();
        Console.WriteLine($"Паспорт абитуриента с ID {applicant.ApplicantID} получен - {_passportId}");

        var cmdApplicant = new MySqlCommand();
        cmdApplicant.CommandText = $"DELETE FROM Applicants WHERE ApplicantID = {applicant.ApplicantID}";
        cmdApplicant.Connection = connection;
        cmdApplicant.ExecuteNonQuery();
        Console.WriteLine($"Абитуриент с ID {applicant.ApplicantID} успешно удален!");

        var cmdPassport = new MySqlCommand();
        cmdPassport.CommandText = $"DELETE FROM Passports WHERE PassportID = {_passportId}";
        cmdPassport.Connection = connection;
        cmdPassport.ExecuteNonQuery();
        Console.WriteLine($"Паспорт абитуриента с ID {applicant.ApplicantID} успешно удален!");

        Console.WriteLine($"Абитуриент с ID {applicant.ApplicantID} успешно удален полностью!");
        var msgBoxSuccess = MessageBox.Avalonia.MessageBoxManager
            .GetMessageBoxCustomWindow(new MessageBoxCustomParamsWithImage
            {
                ShowInCenter = true,
                CanResize = false,
                Height = 500,
                WindowIcon = new WindowIcon($"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}/Assets/chelNew.png"),
                ContentMessage = $"Абитуриент {applicant.FullName} успешно удален из картотеки!",
                Icon = new Bitmap($"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}/Assets/ok.png"),
                ButtonDefinitions = new[] 
                {
                    new ButtonDefinition
                    {
                        Name = "Ok", IsDefault = true, IsCancel = false
                    }
                },
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            });
        msgBoxSuccess.Show();
        connection.Close();
    }
}