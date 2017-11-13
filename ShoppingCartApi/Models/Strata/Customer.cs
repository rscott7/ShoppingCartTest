namespace ShoppingCartApi.Models.Strata
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Column("Type")]
        public string TypeString
        {
            get
            {
                return Type.ToString();
            }
            private set
            {
                CustomerType type;
                Enum.TryParse(value, out type);
                Type = type;
            }
        }

        [Required]
        [StringLength(250)]
        public string Address { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [NotMapped]
        public CustomerType Type { get; set; }
    }
}

