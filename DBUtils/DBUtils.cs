using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Models;
using MySql.Data.MySqlClient;

namespace AcademicApplicants.DBUtils;

public class DBUtils
{
    public static MySqlConnection GetDbConnection()
    {
        using (var stream = File.OpenRead($"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}/DBUtils/.env"))
        {
            DotNetEnv.Env.Load(stream);
        }
        
        var host = DotNetEnv.Env.GetString("HOST") ?? string.Empty;
        var port = DotNetEnv.Env.GetInt("PORT");
        var database = DotNetEnv.Env.GetString("DATABASE") ?? string.Empty;
        var username = DotNetEnv.Env.GetString("USERNAME") ?? string.Empty;
        var password = DotNetEnv.Env.GetString("PASSWORD") ?? string.Empty;

        // ReSharper disable once InvertIf
        if (host == string.Empty || database == string.Empty ||
            username == string.Empty || password == string.Empty)
        {
            ShowErrorMessage();
            Environment.Exit(0);
        }
        
        return DBMySQLUtils.GetDbConnection(host, port, database, username, password);
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
                ContentMessage = "Системная ошибка: Проверьте подключение к .env и повторите попытку!",
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