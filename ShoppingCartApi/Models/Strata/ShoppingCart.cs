namespace ShoppingCartApi.Models.Strata
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ShoppingCart")]
    public partial class ShoppingCart
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Customer { get; set; }

        [Required]
        [StringLength(10)]
        public string ProductCode { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public bool IsValid()
        {
            return (Quantity > 0);
        }
    }
}
