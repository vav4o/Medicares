using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UI.Models;
using UI.ViewModels.Doctors;

namespace UI.ViewModels.Patients
{
    public class PatientVM : BaseVM
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
