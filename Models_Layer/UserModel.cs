using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Models_Layer
{
    public class UserModel
    {
        [Required]

        public int User_Id { get; set; }
        [Required(ErrorMessage = "{0} should not be empty")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "First name should starts with Cap and should have minimum 3 characters")]
        [RegularExpression(@"(?=^.{0,40}$)^[a-zA-Z-]{3,}\s[a-zA-Z-]{3,}$", ErrorMessage = "Full name is not valid")]
        public string FullName { get; set; }
        [Required]
        [RegularExpression(@"^(0|91)?[6-9][0-9]{9}$", ErrorMessage = "Mobile Number is not valid")]
        public long MobileNumber { get; set; }
        
        [Required(ErrorMessage = "{0} should not be empty")]
        [RegularExpression(@"^[a-zA-Z0-9]{3,}([._+-][0-9a-zA-Z]{2,})*@[0-9a-zA-Z]+[.]?([a-zA-Z]{2})+[.]([a-zA-Z]{3})+$", ErrorMessage = "Email id is not valid")]
        [DataType(DataType.EmailAddress)]
        public string Email_Id { get; set; }
      
        public string Password { get; set; }
      
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State {  get; set; }
        
       
        



    }
}
