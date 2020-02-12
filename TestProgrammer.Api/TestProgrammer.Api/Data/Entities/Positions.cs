using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestProgrammer.Api.Data.Entities
{
    public class Positions
    {
        [Key]
        public int PositionID { get; set; }

        [Display(Name = "Position Name")]
        public string PositionName { get; set; }

        //Relaciones
        [JsonIgnore]
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
