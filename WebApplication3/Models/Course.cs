namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class Course
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Course Name  is Required")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Please Enter Only Character values.")]
        [Remote("IsAlreadyCourseNameAsync", "API", ErrorMessage = "Course Name already exists in database.")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Dept Name  is Required")]
        public int DeptID { get; set; }
    
        public virtual Department Department { get; set; }
    }
}
