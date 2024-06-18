using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UI.ViewModels.Shared;

namespace UI.ViewModels.Doctors
{
    public class IndexVM : BaseVM
    {
        public List<DoctorVM> Items { get; set; }
        public PagerVM Pager { get; set; }
        public FilterVM Filter { get; set; }

        [StringLength(20)]
        public string FName { get; set; }
    
        [StringLength(20)]
        public string LName { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [StringLength(20)]
        public string Email { get; set; }
        
        public decimal? Salary { get; set; }
        public DateTime? HireDate { get; set; }
     
        [StringLength(20)]
        public string Username { get; set; }
    }
}
