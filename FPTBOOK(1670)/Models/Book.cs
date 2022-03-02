namespace FPTBOOK_1670_.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Book
    {
        [StringLength(10)]
        public string BookID { get; set; }

        [Required]
        [StringLength(100)]
        public string BookName { get; set; }

        [Required]
        [StringLength(10)]
        public string CategoryID { get; set; }

        [Required]
        [StringLength(10)]
        public string AuthorID { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        [Required]
        public byte[] Image { get; set; }

        [Required]
        public string UrlImage { get; set; }

        [Required]
        [StringLength(200)]
        public string ShortDesc { get; set; }

        [Required]
        [StringLength(500)]
        public string DetailDesc { get; set; }

        public virtual Author Author { get; set; }

        public virtual Category Category { get; set; }
    }
}
