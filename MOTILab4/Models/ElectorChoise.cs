using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MOTILab4.Models
{
    public class ElectorChoise
    {
        [Key]
        public int ElectorChoiseId { get; set; }
        public int ElectorId { get; set; }
        public int VotingObjectId { get; set; }
        public int Order { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public Elector Elector { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public VotingObject VotingObject { get; set; }
    }
}
