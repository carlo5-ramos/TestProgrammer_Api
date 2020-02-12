using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestProgrammer.Api.Data.Entities
{
    public class Profiles
    {
        [Key]
        public int ProfileID { get; set; }

        [Display(Name = "Profile Name")]
        public string ProfileName { get; set; }

        //Relaciones
        [JsonIgnore]
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
