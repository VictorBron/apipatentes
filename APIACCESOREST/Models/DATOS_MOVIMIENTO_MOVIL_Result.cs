using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIACCESOREST.Models
{
    public class DATOS_MOVIMIENTO_MOVIL_Result
    {
        public string NOMBRE { get; set; }

        public string PASAPORTE { get; set; }

        public long ID_SOLICITUD { get; set; }

        public string ESTADO { get; set; }

        public string AUTORIZACION { get; set; }

        public string PUERTO { get; set; }

        public string USUARIO { get; set; }

        public string LOCACION { get; set; }

        public string NAVE { get; set; }

        public DateTime FECHA_DESDE { get; set; }

        public DateTime FECHA_HASTA { get; set; }

        public string EMPRESA { get; set; }

        public long ID_PERSONA_APROBADA { get; set; }

        public long ID_VEHICULO { get; set; }

        public string TIPO_MOVIMIENTO { get; set; }

    }
}