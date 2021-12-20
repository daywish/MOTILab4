using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MOTILab4.Models
{
    public class Elector
    {
        public Elector()
        {
            ElectorChoise = new HashSet<ElectorChoise>();
        }
        [Key]
        public int ElectorId { get; set; }
        public string ElectorName { get; set; }
        public int ElectorRatio { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<ElectorChoise> ElectorChoise { get; set; }
    }
}
