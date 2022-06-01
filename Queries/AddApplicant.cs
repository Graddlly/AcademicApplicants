using System;
using System.IO;
using AcademicApplicants.Models;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Models;
using MySql.Data.MySqlClient;

namespace AcademicApplicants.Queries;

public class AddApplicant
{
    // ReSharper disable once MemberCanBeMadeStatic.Global
    public void Init() {}
    
    public AddApplicant(Passports passport, Applicant applicant, Faculties faculty, 
                        Groups group, Specialties speciality, Marks mark)
    {
        using var connection = DBUtils.DBUtils.GetDbConnection();
        connection.Open();
        
        var cmdPassport = new MySqlCommand();
        cmdPassport.CommandText = "INSERT INTO Passports VALUES (NULL, @WhoIssued, @WhenIssued, @SNumber)";
        cmdPassport.Parameters.AddWithValue("@WhoIssued", passport.WhoIssued);
        cmdPassport.Parameters.AddWithValue("@WhenIssued", passport.WhenIssued);
        cmdPassport.Parameters.AddWithValue("@SNumber", passport.SNumber);
        cmdPassport.Connection = connection;
        cmdPassport.ExecuteNonQuery();
        Console.WriteLine($"Паспорт {passport.SNumber} добавлен в картотеку!");

        var cmdApplicant = new MySqlCommand();
        cmdApplicant.Connection = connection;
        cmdApplicant.CommandText = "INSERT INTO Applicants VALUES (NULL, @FullName, @Birthday, " + 
                                    "(SELECT PassportID FROM Passports WHERE SNumber = @SNumber), " + 
                                    "@Institution, @EndInstitution, @GoldMedal, @CertificateID)";
        cmdApplicant.Parameters.AddWithValue("@FullName", applicant.FullName);
        cmdApplicant.Parameters.AddWithValue("@Birthday", applicant.Birthday);
        cmdApplicant.Parameters.AddWithValue("@SNumber", passport.SNumber);
        cmdApplicant.Parameters.AddWithValue("@Institution", applicant.Institution);
        cmdApplicant.Parameters.AddWithValue("@EndInstitution", applicant.EndInstitution);
        cmdApplicant.Parameters.AddWithValue("@GoldMedal", applicant.GoldMedal);
        cmdApplicant.Parameters.AddWithValue("@CertificateID", applicant.CertificateID);
        cmdApplicant.ExecuteNonQuery();
        Console.WriteLine($"Абитуриент {applicant.FullName} добавлен в картотеку!");

        var addMarksSql = $"INSERT INTO Marks VALUES (NULL, " +
                            $"(SELECT ApplicantID FROM Applicants WHERE CertificateID = {applicant.CertificateID}), " +
                            $"{mark.Exam1}, {mark.Exam2}, {mark.Exam3}, {mark.AverageMark})";
        var cmdMark = new MySqlCommand(addMarksSql, connection);
        cmdMark.ExecuteNonQuery();
        Console.WriteLine($"Оценки абитуриента {applicant.FullName} занесены в картотеку!");

        var addRegistrationSql = $"INSERT INTO Registrations VALUES (NULL, " +
                                   $"(SELECT ApplicantID FROM Applicants WHERE CertificateID = {applicant.CertificateID}), " +
                                   $"{group.GroupID}, {faculty.FacultyID}, {speciality.SpecialityID})";
        var cmdRegistration = new MySqlCommand(addRegistrationSql, connection);
        cmdRegistration.ExecuteNonQuery();
        Console.WriteLine($"Абитуриент {applicant.FullName} полностью зарегистрирован!");

        var msgBoxSuccess = MessageBox.Avalonia.MessageBoxManager
            .GetMessageBoxCustomWindow(new MessageBoxCustomParamsWithImage
            {
                ShowInCenter = true,
                CanResize = false,
                Height = 500,
                WindowIcon = new WindowIcon($"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}/Assets/chelNew.png"),
                ContentMessage = $"Абитуриент {applicant.FullName} успешно занесен в картотеку!",
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