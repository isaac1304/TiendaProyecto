using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaProyecto.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public string Category { get; set; }

        public string? ImageUrl { get; set; }

        public byte[]? ImageData { get; set; }
    }
}
