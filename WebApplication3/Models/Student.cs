
namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Student
    {
        public int sid { get; set; }

        [Required(ErrorMessage = "Student Name is Required")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Please Enter Only Character values.")]
        public string sname { get; set; }
        public Nullable<int> syear { get; set; }
        public Nullable<decimal> sgrade { get; set; }
    }
}
