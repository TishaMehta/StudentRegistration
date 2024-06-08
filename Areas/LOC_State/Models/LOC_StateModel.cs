using System.ComponentModel.DataAnnotations;

namespace StudentRegistration.Areas.LOC_State.Models
{
    public class LOC_StateModel
    {
        public int MyProperty { get; set; }
        [Required]
        public int StateID { get; set; }
        [Required]

        public string? StateName { get; set; }
        [Required]

        public string? CountryName { get; set; }
        [Required]

        public int CityCount { get; set; }
        public int CountryID { get; set; }
        [Required]

        public string? StateCode { get; set; }
        [Required]

        public DateTime Created { get; set; }
        [Required]

        public DateTime Modified { get; set; }

    }
}
