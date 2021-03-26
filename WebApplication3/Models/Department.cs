namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class Department
    {
        public Department()
        {
            this.Courses = new HashSet<Course>();
        }
    
        public int Deptid { get; set; }

        [Required(ErrorMessage = "Dept Name is Required")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Please Enter Only Character values.")]
        [Remote("IsAlreadyDeptNameAsync", "API", ErrorMessage = "Dept Name already exists in database.")]
        public string DeptName { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
