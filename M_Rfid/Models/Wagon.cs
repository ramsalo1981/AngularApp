using System;
using System.Collections.Generic;

namespace M_Rfid.Models
{
    public partial class Wagon
    {
        public Wagon()
        {
            ActiveHaul = new HashSet<ActiveHaul>();
        }

        public int WagonId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ActiveHaul> ActiveHaul { get; set; }
    }
}
