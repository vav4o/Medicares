using System.ComponentModel;

namespace API.DTOs
{
    public abstract class BaseDTO
    {
        [DefaultValue(0)]
        public int Id { get; set; }
    }
}
