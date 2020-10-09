using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace M_Rfid.Models
{
    public partial class Haul
    {
        public Haul()
        {
            ActiveHaul = new HashSet<ActiveHaul>();
        }

        public int HaulId { get; set; }
        public int? DockId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string HaulSituation { get; set; }

        [NotMapped]
        public string DeletedActiveHaulIDs { get; set; }

        public virtual Dock Dock { get; set; }
        public virtual ICollection<ActiveHaul> ActiveHaul { get; set; }
    }
}
