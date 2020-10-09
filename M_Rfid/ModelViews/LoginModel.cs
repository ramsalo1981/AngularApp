﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace M_Rfid.ModelViews
{
    public class LoginModel
    {
        [StringLength(256)]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }

        [Required]
        public bool RememberMe { get; set; }
    }
}
