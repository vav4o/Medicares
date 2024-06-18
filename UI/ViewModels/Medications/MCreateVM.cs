using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels.Medications
{
    public class MCreateVM : BaseVM
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(30)]
        public string? Manufacturer { get; set; }
        [Required]
        [StringLength(10)]
        public string Type { get; set; }
        [Required]
        [StringLength(10)]
        public string? Dosage { get; set; }
        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
        [Required]
        public DateTime? ExpirationDate { get; set; }
    }
}
