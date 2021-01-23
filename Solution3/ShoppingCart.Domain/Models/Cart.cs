using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingCart.Domain.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual Member Member { get; set; }
        [Required]
        public double price { get; set; }
        [ForeignKey("Member")]
        public string Email { get; set; }

        public virtual List<CartItem> CartItems { get; set; }
      
    }
}
