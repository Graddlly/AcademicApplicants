namespace AcademicApplicants.Assets;

public class DataGridUpdateData
{
    internal static readonly string[] ApplicantsLabels =
    {
        "ID абитуриента", "ФИО", "Дата рождения", "ID паспорта", "Учеб. заведение", 
        "Дата окончания УЗ", "Золотая медаль", "ID аттестата/диплома"
    };
    internal static readonly string[] ApplicantsBinding =
    {
        "ApplicantID", "FullName", "Birthday", "PassportID", "Institution",
        "EndInstitution", "GoldMedal", "CertificateID"
    };

    internal static readonly string[] FacultiesLabels =
    {
        "ID факультета", "Название факультета"
    };
    internal static readonly string[] FacultiesBinding =
    {
        "FacultyID", "FacultyName"
    };

    internal static readonly string[] GroupsLabels =
    {
        "ID группы", "Название группы"
    };
    internal static readonly string[] GroupsBinding =
    {
        "GroupID", "GroupName"
    };

    internal static readonly string[] MarksLabels =
    {
        "ID оценок", "ID абитуриента",
        "Экзамен №1", "Экзамен №2", "Экзамен №3", "Среднее за экзамены"
    };
    internal static readonly string[] MarksBinding =
    {
        "MarkID", "ApplicantID",
        "Exam1", "Exam2", "Exam3", "AverageMark"
    };

    internal static readonly string[] PassportsLabels =
    {
        "ID паспорта", "Кем выдан", "Когда выдан", "Серия и номер"
    };
    internal static readonly string[] PassportsBinding =
    {
        "PassportID", "WhoIssued", "WhenIssued", "SNumber"
    };

    internal static readonly string[] RegistrationsLabels =
    {
        "ID регистрации", "ID абитуриента",
        "ID группы", "ID факультета", "ID специальности"
    };
    internal static readonly string[] RegistrationsBinding =
    {
        "RegistrationID", "ApplicantID",
        "GroupID", "FacultyID", "SpecialityID"
    };

    internal static readonly string[] SpecialtiesLabels =
    {
        "ID специальности", "Название специальности"
    };
    internal static readonly string[] SpecialtiesBinding =
    {
        "SpecialityID", "SpecialtyName"
    };
}