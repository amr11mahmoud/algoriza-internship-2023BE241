using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain.Base;

namespace Vezeeta.Core.Domain.Lookup
{
    public class Specialization : Entity<Guid>
    {
        public Specialization(Guid id) : base(id)
        {
        }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
