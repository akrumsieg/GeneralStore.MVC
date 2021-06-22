using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeneralStore.MVC.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [ForeignKey(nameof(Customer))]
        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey(nameof(Product))]
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        [Display(Name = "Quantity Purchased")]
        public int PurchaseQuantity { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Time of Transaction")]
        public DateTime TimeStamp { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Time of Most Recent Modification")]
        public DateTime? ModifiedTimeStamp { get; set; }

        public decimal TotalPrice()
        {
            return PurchaseQuantity * Product.Price;
        }
    }
}