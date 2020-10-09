using System;
using System.Collections.Generic;

namespace M_Rfid.Models
{
    public partial class ActiveHaul
    {
        public int ActiveHaulId { get; set; }
        public int? HaulId { get; set; }
        public int? WagonId { get; set; }
        public bool? ActiveHaulStatus { get; set; }
        
        public virtual Haul Haul { get; set; }
        public virtual Wagon Wagon { get; set; }
    }
}
