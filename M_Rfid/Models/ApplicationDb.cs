using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M_Rfid.Models
{
    public class ApplicationDb : IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {
        public ApplicationDb(DbContextOptions<ApplicationDb> options) : base(options)
        {

        }
        public virtual DbSet<ActiveHaul> ActiveHaul { get; set; }
        public virtual DbSet<Dock> Dock { get; set; }
        public virtual DbSet<Haul> Haul { get; set; }
        public virtual DbSet<Wagon> Wagon { get; set; }

    }
}
