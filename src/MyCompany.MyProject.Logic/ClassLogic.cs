using MyCompany.MyProject.DataRepos.Interface;
using MyCompany.MyProject.Logic.Interface;
using MyCompany.MyProject.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MyCompany.MyProject.Logic
{
    public class ClassLogic : IClassLogic
    {
        private readonly IMyProjectUnitOfWork myProjectUniOfWork;
        public ClassLogic(IMyProjectUnitOfWork uniOfWork)
        {
            myProjectUniOfWork = uniOfWork;
        }
        public ClassVM CreateClass(string className)
        {
            myProjectUniOfWork.ClassRepos.InsertAndSaveChange(new ClassVM()
            {
                ClassName = className
            });
            return myProjectUniOfWork.ClassRepos.GetNewItem();
        }

        public bool DeleteClass(int id)
        {
            myProjectUniOfWork.ClassRepos.DeleteById(id);
            myProjectUniOfWork.SaveChanges();
            return !myProjectUniOfWork.ClassRepos.IsExisted(x => x.ClassId == id);
        }

        public List<ClassVM> GetClasses(int? id = null)
        {
            List<ClassVM> classList = myProjectUniOfWork.ClassRepos.Get(x => id != null ? x.ClassId == id : true).ToList();
            return classList;
        }

        public ClassVM UpdateClass(int id, string className)
        {
            ClassVM item = myProjectUniOfWork.ClassRepos.GetById(id);
            item.ClassName = className;
            item = myProjectUniOfWork.ClassRepos.UpdateAndSaveChange(item);
            return item;
        }
    }
}
