using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MOTILab4.Models
{
    public class VotingObject
    {
        public VotingObject()
        {
            ElectorChoise = new HashSet<ElectorChoise>();
        }
        [Key]
        public int VotingObjectId { get; set; }
        public string VotingObjectName { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<ElectorChoise> ElectorChoise { get; set; }
    }
}
