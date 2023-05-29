using System;
using System.Collections.Generic;

#nullable disable

namespace Ushtrime2ProvimGrupi1.Entities
{
    public partial class Kategorium
    {
        public Kategorium()
        {
            Shkrimets = new HashSet<Shkrimet>();
        }

        public int Id { get; set; }
        public string Emri { get; set; }

        public virtual ICollection<Shkrimet> Shkrimets { get; set; }
    }
}
