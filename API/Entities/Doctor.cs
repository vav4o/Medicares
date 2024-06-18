using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Doctor : BaseEntity
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
        [Column(TypeName = "decimal(10,2)")]
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
