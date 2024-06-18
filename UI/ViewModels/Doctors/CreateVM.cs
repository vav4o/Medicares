using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels.Doctors
{
    public class CreateVM : BaseVM
    {
        [Required]
        [StringLength(20)]
        public string FName { get; set; }
        [Required]
        [StringLength(20)]
        public string LName { get; set; }
        [Required]
        [StringLength(10)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(20)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Required]
        public DateTime? HireDate { get; set; }
        [Required]
        [StringLength(20)]
        public string Username { get; set; }
        [Required]
        [StringLength(20)]
        public string Password { get; set; }
    }
}
