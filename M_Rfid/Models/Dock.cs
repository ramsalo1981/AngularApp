using System;
using System.Collections.Generic;

namespace M_Rfid.Models
{
    public partial class Dock
    {
        public Dock()
        {
            Haul = new HashSet<Haul>();
        }

        public int DockId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Haul> Haul { get; set; }
    }
}
