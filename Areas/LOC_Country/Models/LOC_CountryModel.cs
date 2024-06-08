using System.ComponentModel.DataAnnotations;

namespace StudentRegistration.Areas.LOC_Country.Models
{
    public class LOC_CountryModel
    {
        public int  MyProperty { get; set; }
        [Required]
        public int CountryID { get; set; }
        [Required]
        public string? CountryName { get; set; }
        [Required]
        public string? CountryCode { get; set; }
        [Required]
        public DateTime Created {get; set; }
        [Required]
        public DateTime Modified { get; set; }
          

    }


}
