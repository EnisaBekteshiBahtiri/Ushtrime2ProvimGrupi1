using System;
using System.Collections.Generic;

#nullable disable

namespace Ushtrime2ProvimGrupi1.Entities
{
    public partial class Autori
    {
        public Autori()
        {
            Shkrimets = new HashSet<Shkrimet>();
        }

        public int Id { get; set; }
        public string Emri { get; set; }
        public string Mbiemri { get; set; }

        public virtual ICollection<Shkrimet> Shkrimets { get; set; }
    }
}
