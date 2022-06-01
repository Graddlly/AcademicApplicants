using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AcademicApplicants.Models;
using DynamicData.Kernel;

namespace AcademicApplicants.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public List<Applicant> Applicants { get; set; }
    }
}