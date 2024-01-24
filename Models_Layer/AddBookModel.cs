using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models_Layer
{
    public class AddBookModel
    {
        [Required]
        public int Book_Id { get; set; }
        [Required]
        public string Book_Name { get; set; }
        [Required]
        public string Author_Name { get; set; }
        [Required]
        public string Book_Details { get; set; }
        [Required]
        public string Book_Image { get; set; }
        [Required]
        public float Book_Price { get; set; }
       
        [Required]
        public float Book_Rating { get; set; }
        [Required]
        public int Quantity { get; set; }


    }
}
