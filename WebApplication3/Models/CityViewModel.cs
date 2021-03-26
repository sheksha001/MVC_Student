using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class CityViewModel
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public int? CourseId { get; set; }
        public string CourseName { get; set; }
        public int? StudentId { get; set; }
        public string StudentName { get; set; }
    }
}