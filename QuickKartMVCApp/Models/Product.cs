using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuickKartMVCApp.Models
{
    public class Product
    {

        [Required (ErrorMessage="CategoryId is Mandatory")]
        public Nullable<byte> CategoryId { set; get; }
        [Required(ErrorMessage = "Price is Mandatory")]
        public decimal Price { set; get; }
        [Required(ErrorMessage = "Product Id is Mandatory")]
        public string ProductId { set; get; }

        [Required(ErrorMessage = "Product Name is Mandatory")]
        [MinLength(3,ErrorMessage ="Product name must be 3 characters")]
        public string ProductName { set; get; }

        [Required(ErrorMessage = "Quantity Available is Mandatory")]
        [Range(1,int.MaxValue,ErrorMessage ="Must be greater than 0")]
        public int QuantityAvailable { set; get; }
    }
}