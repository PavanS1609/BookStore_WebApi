using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;

namespace Models_Layer
{
    public class OrdersModel
    {
        [Required]
        public int Order_Id { get; set; }
        [Required]
        public int User_Id { get; set; }
        [Required]
        public int Cart_Id { get; set; }
        [Required]
        public int Book_Id { get; set; }
        [Required]
        public int Price{ get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Book_Name { get; set; }
        [Required]
        public DateTime Ordered_At { get; set; }
    }
}
