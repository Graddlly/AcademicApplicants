using System;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Models;

namespace AcademicApplicants.Models;
// ReSharper disable InconsistentNaming

public class Applicant
{
    private int _applicantID;
    private string? _fullName, _birthday, _institution,
                    _endInstitution, _certificateID;
    
    public int? ApplicantID
    {
        get => _applicantID;
        set
        {
            if (value == null || !IsDigitOnly(value.ToString())) ShowErrorMessage();
#pragma warning disable CS8629
            else _applicantID = (int)value;
#pragma warning restore CS8629
        }
    }

    public string? FullName
    {
        get => _fullName;
        set
        {
            if (value is {Length: > 150}) ShowErrorMessage();
            else _fullName = value;
        }
    }

    public string? Birthday
    {
        get => _birthday;
        set
        {
            if (value == null) ShowErrorMessage();
            else _birthday = value;
        }
    }
    public int? PassportID { get; set; }

    public string? Institution
    {
        get => _institution;
        set
        {
            if (value is {Length: > 100}) ShowErrorMessage();
            else _institution = value;
        }
    }
    public string? EndInstitution
    {
        get => _endInstitution;
        set
        {
            if (value == null) ShowErrorMessage();
            else _endInstitution = value;
        }
    }
    public bool GoldMedal { get; set; }
    public string? CertificateID
    {
        get => _certificateID;
        set
        {
            if (value == null || !IsDigitOnly(value)) ShowErrorMessage();
#pragma warning disable CS8604
            else _certificateID = new string(value.Where(char.IsDigit).ToArray());
#pragma warning restore CS8604
        }
    }

    private static bool IsDigitOnly(string? str)
    {
        return str.All(c => c is >= '0' and <= '9');
    }

    private static void ShowErrorMessage()
    {
        var msgBoxError = MessageBox.Avalonia.MessageBoxManager
            .GetMessageBoxCustomWindow(new MessageBoxCustomParamsWithImage
            {
                ShowInCenter = true,
                CanResize = false,
                Height = 250,
                WindowIcon = new WindowIcon($"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}/Assets/chelNew.png"),
                ContentMessage = $"Введите параметры правильно и повторите попытку!",
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