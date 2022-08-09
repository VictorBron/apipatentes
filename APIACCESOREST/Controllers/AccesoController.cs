using APIACCESOREST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIACCESOREST.Controllers
{
    public class AccesoController : ApiController
    {
        // GET: api/Acceso
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Acceso/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Acceso
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT: api/Acceso/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Acceso/5
        public void Delete(int id)
        {
        }

        public HttpResponseMessage Get(string rut, string ip)
        {
            var data1 = new
            {
                mensaje = "",
                autorizacion = "",
                codigoAutorizacion = ""
            };
            try
            {
                bool flag = false;
                rut = rut.Replace("'", "");
                rut = rut.Replace("/", "");
                rut = rut.Replace("-", "");
                rut = rut.Replace(" ", "");
                rut = rut.Replace("&", "");
                int length1 = rut.Length;
                if (this.validarRut(rut))
                {
                    int length2 = length1 - 1;
                    string s = rut.Substring(0, length2);
                    if (int.Parse(s) > 3000000)
                        rut = s;
                }
                DATOS_MOVIMIENTO_MOVIL_Result Valida = CONEXIONSP.ValidaAcceso(int.Parse(rut), ip);


                string str = "";
                if (Valida.ID_PERSONA_APROBADA == 0)
                {
                    var data2 = new
                    {
                        mensaje = "nok",
                        autorizacion = "NO EXISTE SOLICITUD",
                        codigoAutorizacion = "0"
                    };
                    return this.Request.CreateResponse(HttpStatusCode.Unauthorized, data2);
                }
           
                    str = Valida.ESTADO;
                    if (Valida.AUTORIZACION.Equals("APROBADO"))
                        flag = true;
                
                if (str.Equals("FUERA DE PUERTO") && !flag)
                {
                    var data2 = new
                    {
                        mensaje = "nok",
                        autorizacion = "Denegada",
                        codigoAutorizacion = "0"
                    };
                    return this.Request.CreateResponse(HttpStatusCode.Unauthorized, data2);
                }
                if (Valida.ESTADO.Equals("FUERA DE PUERTO") & flag)
                {
                    var data2 = new
                    {
                        mensaje = "ok",
                        autorizacion = "permitida",
                        codigoAutorizacion = Valida.ID_PERSONA_APROBADA
                    };
                    return this.Request.CreateResponse(HttpStatusCode.OK, data2);
                }
                if (Valida.ESTADO.Equals("EN PUERTO"))
                {
                    var data2 = new
                    {
                        mensaje = "ok",
                        autorizacion = "USTED YA SE ENCUENTRA DENTRO DEL PUERTO. MARQUE UNA SALIDA",
                        codigoAutorizacion = Valida.ID_PERSONA_APROBADA.ToString()
                    };
                    return this.Request.CreateResponse(HttpStatusCode.Unauthorized, data2);
                }
                var data3 = new
                {
                    mensaje = "nok",
                    autorizacion = "NO EXISTE SOLICITUD",
                    codigoAutorizacion = "0"
                };
                return this.Request.CreateResponse(HttpStatusCode.Unauthorized, data3);
            }
            catch (Exception ex)
            {
                var data2 = new
                {
                    mensaje = "ERROR",
                    autorizacion = "DENEGADA:" + ex.Message.ToString(),
                    codigoAutorizacion = ""
                };
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, data2);
            }
        }

        public HttpResponseMessage Post([FromBody] registroingreso registroingreso)
        {
            var data1 = new { mensaje = "OK" };
            try
            {
                CONEXIONSP.RegistraAcceso(registroingreso);
                var data2 = new
                {
                    mensaje = "INGRESE AHORA POR FAVOR"
                };
                return this.Request.CreateResponse(HttpStatusCode.OK, data2);
            }
            catch (Exception ex)
            {
                var data2 = new
                {
                    mensaje = "ERROR:" + ex.Message.ToString()
                };
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, data2);
            }
        }

        public bool validarRut(string rut)
        {
            bool flag = false;
            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int num1 = int.Parse(rut.Substring(0, rut.Length - 1));
                char ch = char.Parse(rut.Substring(rut.Length - 1, 1));
                int num2 = 0;
                int num3 = 1;
                for (; num1 != 0; num1 /= 10)
                    num3 = (num3 + num1 % 10 * (9 - num2++ % 6)) % 11;
                if ((int)ch == (num3 != 0 ? (int)(ushort)(num3 + 47) : 75))
                    flag = true;
            }
            catch (Exception ex)
            {
            }
            return flag;
        }
    }
}
