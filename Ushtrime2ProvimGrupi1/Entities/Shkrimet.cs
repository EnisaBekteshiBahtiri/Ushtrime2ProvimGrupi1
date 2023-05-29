using System;
using System.Collections.Generic;

#nullable disable

namespace Ushtrime2ProvimGrupi1.Entities
{
    public partial class Shkrimet
    {
        public int Id { get; set; }
        public string Titulli { get; set; }
        public int AutoriId { get; set; }
        public int KategoriaId { get; set; }

        public virtual Autori Autori { get; set; }
        public virtual Kategorium Kategoria { get; set; }
    }
}
