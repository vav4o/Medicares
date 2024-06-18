using System.ComponentModel.DataAnnotations;
using UI.ViewModels.Doctors;
using UI.ViewModels.Shared;

namespace UI.ViewModels.Medications
{
    public class MIndexVM
    {
        public List<MedicationVM> Items { get; set; }
        public PagerVM Pager { get; set; }


        [StringLength(30)]
        public string Name { get; set; }
        [StringLength(30)]
        public string? Manufacturer { get; set; }

        [StringLength(10)]
        public string Type { get; set; }
        [StringLength(10)]
        public string? Dosage { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
