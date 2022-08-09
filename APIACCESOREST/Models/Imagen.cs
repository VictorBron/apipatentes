using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIACCESOREST.Models
{
    public class Imagen
    {
        public int Rut { get; set; }
        public int idFoto { get; set; }
        public string fotoB64 { get; set; }
    }
}