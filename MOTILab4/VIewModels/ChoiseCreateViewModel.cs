using MOTILab4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOTILab4.VIewModels
{
    public class ChoiseCreateViewModel
    {
        public ICollection<Elector> Electors { get; set; }
        public ICollection<VotingObject> VotingObjects { get; set; }
    }
}
