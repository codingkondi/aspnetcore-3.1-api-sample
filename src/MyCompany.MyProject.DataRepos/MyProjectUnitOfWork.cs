using MyCompany.MyProject.Database;
using MyCompany.MyProject.Database.Models;
using MyCompany.MyProject.DataRepos.Interface;
using MyCompany.MyProject.Models.ViewModels;

namespace MyCompany.MyProject.DataRepos
{
    public class MyProjectUnitOfWork : IMyProjectUnitOfWork
    {
        private readonly MyDbContext myDbContext;
        #region
        private IGenericRepos<Class, ClassVM> classRepos;
        private IGenericRepos<Student, StudentVM> studentRepos;
        #endregion
        public MyProjectUnitOfWork(MyDbContext context)
        {
            myDbContext = context;
        }
        public IGenericRepos<Class, ClassVM> ClassRepos
        {
            get
            {
                if (classRepos == null)
                {
                    classRepos = new MyDbRepos<Class, ClassVM>(myDbContext);
                }
                return classRepos;
            }
        }

        public IGenericRepos<Student, StudentVM> StudentRepos
        {
            get
            {
                if (studentRepos == null)
                {
                    studentRepos = new MyDbRepos<Student, StudentVM>(myDbContext);
                }
                return studentRepos;
            }
        }
        //setting db savechange result as boolean type
        public bool SaveChanges() => myDbContext.SaveChanges() == 1 ? true : false;
    }
}
