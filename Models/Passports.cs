using System;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Models;

namespace AcademicApplicants.Models;

public class Passports
{
    private string _whoIssued, _whenIssued;
    private long _sNumber;
    
    public int? PassportID { get; set; }
    public string WhoIssued
    {
        get => _whoIssued;
        set
        {
            if (value is {Length: > 100}) ShowErrorMessage();
            else _whoIssued = value;
        }
    }
    public string? WhenIssued
    {
        get => _whenIssued;
        set
        {
            if (value == null) ShowErrorMessage();
            else _whenIssued = value;
        }
    }
    public long SNumber
    {
        get => _sNumber;
        set
        {
            if (!IsDigitOnly(value.ToString()) && 
                value.ToString().Length != 10) ShowErrorMessage();
            else _sNumber = value;
        }
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
    
    private static bool IsDigitOnly(string str)
    {
        return str.All(c => c is >= '0' and <= '9');
    }
}