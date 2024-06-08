using System.ComponentModel.DataAnnotations;

namespace StudentRegistration.Areas.MST_Branch.Models
{
    public class MST_BranchModel
    {
        public int MyProperty { get; set; }
        [Required]
        public int BranchID { get; set; }
        [Required]
        public string? BranchName { get; set; }
        [Required]
        public string? BranchCode { get; set;}
        [Required]
        public DateTime Created { get; set;}
        [Required]

        public DateTime Modified { get; set;}
    }
}
