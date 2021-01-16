﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingCart.Domain.Models
{
    public class OrderDetail 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid ProductFK { get; set; }

        public virtual Product Product { get; set; }


        public Guid OrderFK { get; set; }
        public virtual Order Order { get; set; }


        public int Quantity { get; set; }

        public double Price { get; set; }


    }
}
