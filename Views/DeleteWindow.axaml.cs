using System;
using System.IO;
using AcademicApplicants.Models;
using AcademicApplicants.Queries;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Models;

namespace AcademicApplicants.Views;

public partial class DeleteWindow : Window
{
    private readonly TextBox _tbApplicant;
    
    public DeleteWindow()
    {
        AvaloniaXamlLoader.Load(this);

        _tbApplicant = this.FindControl<TextBox>("TB_Applicant");
    }

    private void On_RemoveButton_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            var newApplicant = new Applicant
            {
                ApplicantID = Convert.ToInt32(_tbApplicant.Text),
                FullName = "null",
                Birthday = "25.05.2022",
                PassportID = null,
                Institution = "null",
                EndInstitution = "25.05.2022",
                GoldMedal = false,
                CertificateID = "1234567891"
            };

            var removeApplicant = new RemoveApplicant(newApplicant);
            removeApplicant.Init();
        }
        catch (Exception error)
        {
            ShowErrorMessage(error.ToString());
        }
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