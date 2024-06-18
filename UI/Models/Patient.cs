using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class Patient : BaseModel
    {
        [Required]
        [StringLength(20)]
        public string FName { get; set; }
        [Required]
        [StringLength(20)]
        public string LName { get; set; }
        [Required]
        public char Gender { get; set; }
        public DateTime? BirthDate { get; set; }


        [Required]
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public virtual Doctor Doctor { get; set; }
        [Required]
        public int MedicationId { get; set; }
        [ForeignKey("MedicationId")]
        public virtual Medication Medication { get; set; }
    }
}
