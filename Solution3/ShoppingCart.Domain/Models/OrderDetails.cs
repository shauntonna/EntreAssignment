﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingCart.Domain.Models
{
    public class OrderDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual Product Product { get; set; }

       


        public virtual Order Order { get; set; }

        [ForeignKey("Order")]
        public Guid OrderFK { get; set; }
        

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }


    }
}
