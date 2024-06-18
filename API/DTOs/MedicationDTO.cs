using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class MedicationDTO : BaseDTO
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
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
