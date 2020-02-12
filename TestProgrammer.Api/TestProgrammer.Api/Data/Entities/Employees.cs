using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TestProgrammer.Api.Data.Entities
{
    public class Employees
    {
        [Key]
        public int EmployeeID { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Display(Name = "Profile")]
        public int PositionID { get; set; }

        [Display(Name = "Profile")]
        public int ProfileID { get; set; }

        //Relaciones
        [JsonIgnore]
        public virtual Positions Positions { get; set; }

        [JsonIgnore]
        public virtual Profiles Profiles { get; set; }
    }
}
