using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Medication : BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [StringLength(30)]
        public string? Manufacturer { get; set; }
        [Required]
        [StringLength(10)]
        public string Type { get; set; }
        [StringLength(10)]
        public string? Dosage { get; set; }
        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
