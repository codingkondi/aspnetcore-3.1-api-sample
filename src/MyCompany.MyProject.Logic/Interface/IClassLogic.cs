using MyCompany.MyProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject.Logic.Interface
{
    public interface IClassLogic
    {
        List<ClassVM> GetClasses(int? id =null);
        ClassVM CreateClass(string className);
        ClassVM UpdateClass(int id,string className);
        bool DeleteClass(int id);
    }
}
