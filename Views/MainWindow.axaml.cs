using AcademicApplicants.Assets;
using AcademicApplicants.Queries;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AcademicApplicants.Views
{
    public partial class MainWindow : Window
    {
        private readonly DataGrid _dataGrid;
        private readonly RadioButton _rbApplicants, _rbMarks, _rbPassports,
                                     _rbGroups, _rbFaculties, _rbRegistrations, _rbSpecialties;

        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
            _dataGrid = this.FindControl<DataGrid>("DG_Main");
            
            _rbApplicants = this.FindControl<RadioButton>("RB_Applicants");
            _rbMarks = this.FindControl<RadioButton>("RB_Marks");
            _rbPassports = this.FindControl<RadioButton>("RB_Passport");
            _rbGroups = this.FindControl<RadioButton>("RB_Groups");
            _rbFaculties = this.FindControl<RadioButton>("RB_Faculties");
            _rbRegistrations = this.FindControl<RadioButton>("RB_Registrations");
            _rbSpecialties = this.FindControl<RadioButton>("RB_Specialties");
        }

        private void B_Update_OnClick(object? sender, RoutedEventArgs e)
        {
            _dataGrid.Columns.Clear();

            if (_rbApplicants.IsChecked == true)
            {
                _dataGrid.Items = new QueryApplicants();

                _dataGrid.ColumnWidth = DataGridLength.Auto;
                _dataGrid.AutoGenerateColumns = false;

                var tempLocal = 0;
                foreach (var label in DataGridUpdateData.ApplicantsLabels)
                {
                    var binding = DataGridUpdateData.ApplicantsBinding[tempLocal];
                    var textColumn = new DataGridTextColumn
                    {
                        Header = label,
                        Binding = new Binding(binding)
                    };
                    _dataGrid.Columns.Add(textColumn);
                    tempLocal++;
                }
            }
            else if (_rbMarks.IsChecked == true)
            {
                _dataGrid.Items = new QueryMarks();
                
                _dataGrid.ColumnWidth = DataGridLength.Auto;
                _dataGrid.AutoGenerateColumns = false;

                var tempLocal = 0;
                foreach (var label in DataGridUpdateData.MarksLabels)
                {
                    var binding = DataGridUpdateData.MarksBinding[tempLocal];
                    var textColumn = new DataGridTextColumn
                    {
                        Header = label,
                        Binding = new Binding(binding)
                    };
                    _dataGrid.Columns.Add(textColumn);
                    tempLocal++;
                }
            }
            else if (_rbPassports.IsChecked == true)
            {
                _dataGrid.Items = new QueryPassports();
                
                _dataGrid.ColumnWidth = DataGridLength.Auto;
                _dataGrid.AutoGenerateColumns = false;

                var tempLocal = 0;
                foreach (var label in DataGridUpdateData.PassportsLabels)
                {
                    var binding = DataGridUpdateData.PassportsBinding[tempLocal];
                    var textColumn = new DataGridTextColumn
                    {
                        Header = label,
                        Binding = new Binding(binding)
                    };
                    _dataGrid.Columns.Add(textColumn);
                    tempLocal++;
                }
            }
            else if (_rbGroups.IsChecked == true)
            {
                _dataGrid.Items = new QueryGroups();
                
                _dataGrid.ColumnWidth = DataGridLength.Auto;
                _dataGrid.AutoGenerateColumns = false;

                var tempLocal = 0;
                foreach (var label in DataGridUpdateData.GroupsLabels)
                {
                    var binding = DataGridUpdateData.GroupsBinding[tempLocal];
                    var textColumn = new DataGridTextColumn
                    {
                        Header = label,
                        Binding = new Binding(binding)
                    };
                    _dataGrid.Columns.Add(textColumn);
                    tempLocal++;
                }
            }
            else if (_rbFaculties.IsChecked == true)
            {
                _dataGrid.Items = new QueryFaculties();
                
                _dataGrid.ColumnWidth = DataGridLength.Auto;
                _dataGrid.AutoGenerateColumns = false;

                var tempLocal = 0;
                foreach (var label in DataGridUpdateData.FacultiesLabels)
                {
                    var binding = DataGridUpdateData.FacultiesBinding[tempLocal];
                    var textColumn = new DataGridTextColumn
                    {
                        Header = label,
                        Binding = new Binding(binding)
                    };
                    _dataGrid.Columns.Add(textColumn);
                    tempLocal++;
                }
            }
            else if (_rbRegistrations.IsChecked == true)
            {
                _dataGrid.Items = new QueryRegistrations();
                
                _dataGrid.ColumnWidth = DataGridLength.Auto;
                _dataGrid.AutoGenerateColumns = false;

                var tempLocal = 0;
                foreach (var label in DataGridUpdateData.RegistrationsLabels)
                {
                    var binding = DataGridUpdateData.RegistrationsBinding[tempLocal];
                    var textColumn = new DataGridTextColumn
                    {
                        Header = label,
                        Binding = new Binding(binding)
                    };
                    _dataGrid.Columns.Add(textColumn);
                    tempLocal++;
                }
            }
            else if (_rbSpecialties.IsChecked == true)
            {
                _dataGrid.Items = new QuerySpecialties();
                
                _dataGrid.ColumnWidth = DataGridLength.Auto;
                _dataGrid.AutoGenerateColumns = false;

                var tempLocal = 0;
                foreach (var label in DataGridUpdateData.SpecialtiesLabels)
                {
                    var binding = DataGridUpdateData.SpecialtiesBinding[tempLocal];
                    var textColumn = new DataGridTextColumn
                    {
                        Header = label,
                        Binding = new Binding(binding)
                    };
                    _dataGrid.Columns.Add(textColumn);
                    tempLocal++;
                }
            }
        }

        private void B_Add_OnClick(object? sender, RoutedEventArgs e)
        {
            var addWindow = new AddWindow();
            addWindow.Show();
        }

        private void B_Delete_OnClick(object? sender, RoutedEventArgs e)
        {
            var deleteWindow = new DeleteWindow();
            deleteWindow.Show();
        }
    }
}