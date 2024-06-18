using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels.Doctors
{
    public class FilterVM
    {
        [MaxLength(20)]
        public string FName { get; set; }
        [MaxLength(20)]
        public string Lname { get; set; }
        [MaxLength(20)]
        public string Email { get; set; }
    }
}
