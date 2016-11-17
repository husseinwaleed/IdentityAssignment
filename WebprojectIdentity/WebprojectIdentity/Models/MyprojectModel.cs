using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebprojectIdentity.Models
{
    public class Teacher
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string secondname { get; set; }
        public string city { get; set; }
        public string phone { get; set; }
        //public string  CreatedBy { get; set; }
        //public Course relatedcourse { get; set; }

    }

    public class Assignement
    {
        public int id { get; set; }
        public string assignmentname { get; set; }
        public string periode { get; set; }
        //public Course relatedcourse { get; set; }
    }

    public class Student
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string secondname { get; set; }
        public string city { get; set; }
        public string phone { get; set; }
        public virtual List<Course> ownedcourse { get; set; }
    }



    public class Course
    {

        public int id { get; set; }
        public string coursename { get; set; }
        public string periode { get; set; }
        public virtual List<Assignement> assignement { get; set; }
        public Teacher teacher { get; set; }
        public virtual List<Student> student { get; set; }
    }
}