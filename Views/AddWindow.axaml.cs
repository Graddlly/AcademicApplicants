using System;
using System.IO;
using System.Linq;
using AcademicApplicants.Models;
using AcademicApplicants.Queries;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Models;

namespace AcademicApplicants.Views;

public partial class AddWindow : Window
{
    private readonly TextBox _tbFullName, _tbSNumberPassport, _tbWhoIssuedPassport, 
                             _tbInstitution, _tbCertificate;
    private readonly DatePicker _dpBirthday, _dpWhenIssuedPassport, _dpEndInstitution;
    private readonly CheckBox _cbGoldMedal;
    private readonly NumericUpDown _nudFaculty, _nudGroup, _nudSpeciality,
                                   _nudExam1, _nudExam2, _nudExam3;

    public AddWindow()
    {
        AvaloniaXamlLoader.Load(this);

        _tbFullName = this.FindControl<TextBox>("TB_FullName");
        _tbSNumberPassport = this.FindControl<TextBox>("TB_SNumberPassport");
        _tbWhoIssuedPassport = this.FindControl<TextBox>("TB_WhoIssuedPassport");
        _tbInstitution = this.FindControl<TextBox>("TB_Institution");
        _tbCertificate = this.FindControl<TextBox>("TB_Certificate");

        _dpBirthday = this.FindControl<DatePicker>("DP_Birthday");
        _dpWhenIssuedPassport = this.FindControl<DatePicker>("DP_WhenIssuedPassport");
        _dpEndInstitution = this.FindControl<DatePicker>("DP_EndInstitution");

        _cbGoldMedal = this.FindControl<CheckBox>("CB_GoldMedal");

        _nudFaculty = this.FindControl<NumericUpDown>("NUD_Faculty");
        _nudGroup = this.FindControl<NumericUpDown>("NUD_Group");
        _nudSpeciality = this.FindControl<NumericUpDown>("NUD_Speciality");
        _nudExam1 = this.FindControl<NumericUpDown>("NUD_Exam1");
        _nudExam2 = this.FindControl<NumericUpDown>("NUD_Exam2");
        _nudExam3 = this.FindControl<NumericUpDown>("NUD_Exam3");

        _dpBirthday.MaxYear = DateTimeOffset.Now.AddYears(-18);
        _dpBirthday.MinYear = DateTimeOffset.Now.AddYears(-100);
        _dpEndInstitution.MaxYear = DateTimeOffset.Now;
        _dpEndInstitution.MinYear = DateTimeOffset.Now.AddYears(-50);
    }

    private void B_Add_OnClick(object? sender, RoutedEventArgs e)
    {
#pragma warning disable CS8629
        try
        {
            var tempPassport = DateOnly.FromDateTime(_dpWhenIssuedPassport.SelectedDate.Value.DateTime);
            var tempApplicantB = DateOnly.FromDateTime(_dpBirthday.SelectedDate.Value.DateTime);
            var tempApplicantEi = DateOnly.FromDateTime(_dpEndInstitution.SelectedDate.Value.DateTime);

            var newPassport = new Passports
            {
                PassportID = null, 
                WhoIssued = _tbWhoIssuedPassport.Text ?? string.Empty, 
                WhenIssued = $"{tempPassport.Year}.{tempPassport.Month}.{tempPassport.Day}",
                SNumber = Convert.ToInt64(_tbSNumberPassport.Text ?? string.Empty)
            };
            
#pragma warning restore CS8629
            var newApplicant = new Applicant
            {
                ApplicantID = 99999, 
                FullName = _tbFullName.Text ?? string.Empty, 
                Birthday = $"{tempApplicantB.Year}.{tempApplicantB.Month}.{tempApplicantB.Day}",
                PassportID = null, 
                Institution = _tbInstitution.Text ?? string.Empty,
                EndInstitution = $"{tempApplicantEi.Year}.{tempApplicantEi.Month}.{tempApplicantEi.Day}",
                GoldMedal = _cbGoldMedal.IsChecked != null && (bool)_cbGoldMedal.IsChecked,
                CertificateID = _tbCertificate.Text ?? string.Empty
            };
            
            var newFaculty = new Faculties
            {
                FacultyID = Convert.ToInt32(_nudFaculty.Value), 
                FacultyName = null
            };
            
            var newGroup = new Groups
            {
                GroupID = Convert.ToInt32(_nudGroup.Value), 
                GroupName = null
            };
            
            var newSpeciality = new Specialties
            {
                SpecialityID = Convert.ToInt32(_nudSpeciality.Value), 
                SpecialtyName = null
            };

            int[] marks =
                {Convert.ToInt32(_nudExam1.Value), Convert.ToInt32(_nudExam2.Value), Convert.ToInt32(_nudExam3.Value)};
            var newMark = new Marks
            {
                MarkID = null, 
                ApplicantID = 99999, 
                Exam1 = marks[0], 
                Exam2 = marks[1], 
                Exam3 = marks[2],
                AverageMark = ToSingle(marks.Average())
            };
            
            var addApplicant = new AddApplicant(newPassport, newApplicant, newFaculty,
                newGroup, newSpeciality, newMark);
            addApplicant.Init();
        }
        catch (Exception error)
        {
            ShowErrorMessage(error.ToString());
        }
    }

    private void B_Clear_OnClick(object? sender, RoutedEventArgs e)
    {
        _tbFullName.Clear(); _tbSNumberPassport.Clear();
        _tbWhoIssuedPassport.Clear(); _tbInstitution.Clear(); _tbCertificate.Clear();

        _dpBirthday.SelectedDate = null; _dpWhenIssuedPassport.SelectedDate = null;
        _dpEndInstitution.SelectedDate = null;

        _cbGoldMedal.IsChecked = false;

        _nudFaculty.Value = 0; _nudGroup.Value = 0; _nudSpeciality.Value = 0;
        _nudExam1.Value = 2; _nudExam2.Value = 2; _nudExam3.Value = 2;
    }

    private static float ToSingle(double value)
    {
        return (float) value;
    }
    
    private static void ShowErrorMessage(string error)
    {
        var msgBoxError = MessageBox.Avalonia.MessageBoxManager
            .GetMessageBoxCustomWindow(new MessageBoxCustomParamsWithImage
            {
                ShowInCenter = true,
                CanResize = false,
                Height = 250,
                WindowIcon = new WindowIcon($"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}/Assets/chelNew.png"),
                ContentMessage = "Системная ошибка: Проверьте введенные значения и повторите попытку!\n" + error,
                Icon = new Bitmap($"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}/Assets/cancel.png"),
                ButtonDefinitions = new[] 
                {
                    new ButtonDefinition
                    {
                        Name = "Ok", IsDefault = true, IsCancel = false
                    }
                },
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            });
        msgBoxError.Show();
    }
}