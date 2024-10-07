using System.ComponentModel.DataAnnotations;

namespace Business_Card.Models.Dtos
{
    public class BusinessCardDto
    {

        //create properties for Business Card
       

        [MaxLength(100)]
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter your Name")]
        public string BusinessCard_Name { get; set; }


        [MaxLength(100)]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter your email")]
        public  string BusinessCard_Email { get; set; }


        // [MaxLength(20)]
        [Display(Name = "PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Please enter your phone number")]
        public  string BusinessCard_PhoneNumber { get; set; }


        [MaxLength(200)]
        [Display(Name = "Address")]
        public  string BusinessCard_Address { get; set; }



        [Display(Name = "Gender ")]
        [MaxLength(10)]
        public  string BusinessCard_Gender { get; set; }



        [Display(Name = "Date_Of_Birth")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please enter your Date of Birth")]
        public  string BusinessCard_Date_Of_Birth { get; set; }



        [MaxLength(1400000)]
        public string Photo { get; set; }

    }
}
