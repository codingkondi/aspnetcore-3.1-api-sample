using MyCompany.MyProject.Database.Models;
using MyCompany.MyProject.Models.ViewModels;

namespace MyCompany.MyProject.DataRepos.Interface
{
    public interface IMyProjectUnitOfWork
    {
        #region Repositories
        IGenericRepos<Class, ClassVM> ClassRepos { get; }
        IGenericRepos<Student, StudentVM> StudentRepos { get; }
        bool SaveChanges();
        #endregion
    }
}
