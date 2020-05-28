using MyCompany.MyProject.Models.ViewModels;
using System.Collections.Generic;

namespace MyCompany.MyProject.Logic.Interface
{
    public interface IStudentLogic
    {
        List<StudentVM> GetStudents(int? id = null);
        StudentVM CreateStudent(string StudentName);
        StudentVM UpdateStudent(int id, string StudentName);
        bool DeleteStudent(int id);
    }
}
