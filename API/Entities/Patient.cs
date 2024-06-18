using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Patient : BaseEntity
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
