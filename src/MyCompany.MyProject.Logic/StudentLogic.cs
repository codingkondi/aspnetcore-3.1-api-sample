using MyCompany.MyProject.DataRepos.Interface;
using MyCompany.MyProject.Logic.Interface;
using MyCompany.MyProject.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MyCompany.MyProject.Logic
{
    public class StudentLogic : IStudentLogic
    {
        private readonly IMyProjectUnitOfWork myProjectUniOfWork;
        public StudentLogic(IMyProjectUnitOfWork uniOfWork)
        {
            myProjectUniOfWork = uniOfWork;
        }
        public List<StudentVM> GetStudents(int? id = null)
        {
            List<StudentVM> StudentList = myProjectUniOfWork.StudentRepos.Get(x => id != null ? x.StudentId == id : true).ToList();
            return StudentList;
        }
        public StudentVM CreateStudent(string studentName)
        {
            myProjectUniOfWork.StudentRepos.InsertAndSaveChange(new StudentVM()
            {
                StudentName = studentName
            });
            return myProjectUniOfWork.StudentRepos.GetNewItem();
        }
        public StudentVM UpdateStudent(int id, string studentName)
        {
            StudentVM item = myProjectUniOfWork.StudentRepos.GetById(id);
            item.StudentName = studentName;
            item = myProjectUniOfWork.StudentRepos.UpdateAndSaveChange(item);
            return item;
        }
        public bool DeleteStudent(int id)
        {
            myProjectUniOfWork.StudentRepos.DeleteById(id);
            myProjectUniOfWork.SaveChanges();
            return !myProjectUniOfWork.StudentRepos.IsExisted(x => x.StudentId == id);
        }
    }
}
