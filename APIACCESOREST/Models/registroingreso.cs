using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIACCESOREST.Models
{
    public class registroingreso
    {
        public long codigoAutorizacion { get; set; }

        public Decimal temperatura { get; set; }

        public string ip { get; set; }
        public string idVehichulo { get; set; }

        public string tipomovimiento { get; set; }
    }
}