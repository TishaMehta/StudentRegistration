using System.ComponentModel.DataAnnotations;

namespace StudentRegistration.Areas.LOC_Student.Models
{
    public class LOC_StudentModel
    {
        public int MyProperty { get; set; }
        [Required]
        public int StudentID { get; set; }
        [Required]

        public string? StudentName { get; set;}
        [Required]

        public string? MobileNoStudent { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]

        public string? MobileNoFather { get; set; }
        [Required]

        public string? Address { get; set; }
        [Required]

        public DateTime BirthDate { get; set; }
        [Required]

        public int Age { get; set; }
        [Required]

        public bool IsActive { get; set; }
        [Required]

        public string? Gender { get; set;}
        [Required]

        public string? Password { get; set; }
        [Required]

        public DateTime Created { get; set; }
        [Required]

        public DateTime Modified { get; set; }
        [Required]

        public int BranchID { get; set; }
        [Required]

        public int CityID { get; set;}



    }
    public class MST_BranchDropDownModel
    {
        public int BranchID { get; set;}
        public string? BranchName { get; set; }
    }
    public class LOC_CityDropDownModel
    {
        public int CityID { get; set;}
        public string? CityName { get; set; }
    }
   
}
