using System.ComponentModel.DataAnnotations;

namespace StudentRegistration.Areas.LOC_City.Models
{
    public class LOC_CityModel
    {
        public int MyProperty { get; set; }
        [Required]
        public int CityID { get; set; }
        [Required]

        public string? CityName { get; set; }
        [Required]


        public string? CityCode { get; set; }
        [Required]

        public int CountryID { get; set; }
        [Required]
        

        public int StateID { get; set; }
        [Required]

        public DateTime Created { get; set; }
        [Required]

        public DateTime Modified { get; set; }
        [Required]

        public string? CountryName { get; set; }
        [Required]
        public string? StateName { get; set; }
    }
    public class LOC_StateDropDownModel
    {
        public int StateID { get; set; }
        public String? StateName { get; set; }
    }
    public class LOC_CountryDropDownModel
    {
        public int CountryID { get; set; }
        public String? CountryName { get; set; }
    }
}

