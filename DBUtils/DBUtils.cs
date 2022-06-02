using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Models;
using MySql.Data.MySqlClient;

namespace AcademicApplicants.DBUtils;

// ReSharper disable once InconsistentNaming
// ReSharper disable once ClassNeverInstantiated.Global
public class DBUtils
{
    public static MySqlConnection GetDbConnection()
    {
        try
        {
            using var stream =
                File.OpenRead(
                    $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}/DBUtils/.env");
            DotNetEnv.Env.Load(stream);
        }
        catch (Exception error) { ShowErrorMessage(error.Message); }

        var host = DotNetEnv.Env.GetString("HOST") ?? string.Empty;
        var port = DotNetEnv.Env.GetInt("PORT");
        var database = DotNetEnv.Env.GetString("DATABASE") ?? string.Empty;
        var username = DotNetEnv.Env.GetString("USERNAME") ?? string.Empty;
        var password = DotNetEnv.Env.GetString("PASSWORD") ?? string.Empty;
        
        if (host == string.Empty || database == string.Empty ||
            username == string.Empty || password == string.Empty) Environment.Exit(0);

        return DBMySQLUtils.GetDbConnection(host, port, database, username, password);
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
                ContentMessage = "Системная ошибка: Проверьте подключение к .env и повторите попытку!\n" + error,
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
