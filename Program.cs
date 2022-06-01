using System;
using Avalonia;
using Avalonia.ReactiveUI;

namespace AcademicApplicants
{
    internal static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Console.WriteLine("Getting Connection...");
            var conn = DBUtils.DBUtils.GetDbConnection();

            try
            {
                Console.WriteLine("Opening Connection...");
                conn.Open();
                Console.WriteLine("Connection Successful!");
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        private static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
    }
}