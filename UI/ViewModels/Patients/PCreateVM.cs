using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels.Patients
{
    public class PCreateVM : BaseVM
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

        [Required]
        public int MedicationId { get; set; }

    }
}
