using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models_Layer
{
    public class AddressModel
    {
        [Required]
        public int Address_Id { get; set; }
        [Required]
        public int User_Id { get; set; }
        [Required]
        [RegularExpression(@"(?=^.{0,40}$)^[a-zA-Z-]{3,}\s[a-zA-Z-]{3,}$", ErrorMessage = "Full name is not valid")]
        public string Customer_Name { get; set; }
        [Required]
        [RegularExpression(@"^(0|91)?[6-9][0-9]{9}$", ErrorMessage = "Mobile Number is not valid")]
        public long Customer_Number { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }

    }
}
