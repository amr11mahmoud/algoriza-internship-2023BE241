﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Domain.Users
{
    public class Role:IdentityRole<Guid>
    {
        public Role() { }
        public Role(string name) : base(name)
        {

        }
    }
}
