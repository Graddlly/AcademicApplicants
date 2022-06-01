namespace AcademicApplicants.Models;

public class Marks
{
    public int? MarkID { get; set; }
    public int? ApplicantID { get; set; }
    public int Exam1 { get; set; }
    public int Exam2 { get; set; }
    public int Exam3 { get; set; }
    public float AverageMark { get; set; }
}