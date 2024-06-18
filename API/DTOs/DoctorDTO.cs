using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class DoctorDTO : BaseDTO
    {
        [Required]
        [StringLength(20)]
        public string FName { get; set; }
        [Required]
        [StringLength(20)]
        public string LName { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [StringLength(20)]
        public string Email { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Salary { get; set; }
        public DateTime? HireDate { get; set; }
        [Required]
        [StringLength(20)]
        public string Username { get; set; }
        [Required]
        [StringLength(20)]
        public string Password { get; set; }
    }
}
