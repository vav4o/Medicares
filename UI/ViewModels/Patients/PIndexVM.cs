using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UI.ViewModels.Doctors;
using UI.ViewModels.Shared;

namespace UI.ViewModels.Patients
{
    public class PIndexVM
    {
        public List<PatientVM> Items { get; set; }
        public PagerVM Pager { get; set; }

        [StringLength(20)]
        public string FName { get; set; }

        [StringLength(20)]
        public string LName { get; set; }

        public char Gender { get; set; }
        public DateTime? BirthDate { get; set; }

        public int DoctorId { get; set; }

        public int MedicationId { get; set; }

    }
}
