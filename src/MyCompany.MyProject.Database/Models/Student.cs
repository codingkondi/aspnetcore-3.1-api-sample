using System;

namespace MyCompany.MyProject.Database.Models
{
    public partial class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int ClassId { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
