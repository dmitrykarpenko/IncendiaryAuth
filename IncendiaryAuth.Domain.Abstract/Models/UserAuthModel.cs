﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncendiaryAuth.Domain.Abstract.Models
{
    public class UserAuthModel
    {
        [RegularExpression(@"^[a-zA-Z]+$",
            ErrorMessage = "Only letters are allowed.")]
        public string UserName { get; set; }

        [RegularExpression(@"^[0-9]+$",
            ErrorMessage = "Only numbers are allowed.")]
        public string Password { get; set; }
    }
}
