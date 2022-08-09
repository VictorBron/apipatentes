using APIACCESOREST.Models;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIACCESOREST.Controllers
{
    public class AccesoVehicularController : ApiController
    {
        // GET: api/AccesoVehicular
        public string Get(string placa,string ip)
        {
            return "";
        }

        // GET: api/AccesoVehicular/5
        public string Get(int id)
        {
            try
            {
                SshClient cSSH = new SshClient("192.168.0.162", 22, "pi", "raspberry");
                cSSH.Connect();
                SshCommand x = cSSH.RunCommand("sudo python /home/pi/r1.py");
                System.Threading.Thread.Sleep(1000);
                SshCommand y = cSSH.RunCommand("sudo python /home/pi/r2.py");

                cSSH.Disconnect();
                cSSH.Dispose();
                return "ok";
            }catch(Exception exp)
            {
                return "error" + exp.Message.ToString();
            }
       
        }

        // POST: api/AccesoVehicular
        public HttpResponseMessage Post([FromBody] Vehiculo datos)
        {
        
            try
            {
                bool flag = false;
                string respuesta="0";
      
                DATOS_MOVIMIENTO_MOVIL_Result Valida = CONEXIONSP.ValidaAccesoVehicular(datos);
                var response = Request.CreateResponse(HttpStatusCode.OK);

                string str = "";
                if (Valida.ID_PERSONA_APROBADA == 0)
                {
                 


                    response.Content = new StringContent("0", System.Text.Encoding.UTF8, "application/text");
                    return response;
                }

                string movimiento = Valida.TIPO_MOVIMIENTO;

                str = Valida.ESTADO;
                if (Valida.AUTORIZACION.Equals("APROBADO"))
                {
                    respuesta = "1";
                }
             

                //if (str.Equals("FUERA DE PUERTO") && !flag)
                //{
                //    respuesta = "N";
                
                //}
                //if (Valida.ESTADO.Equals("FUERA DE PUERTO") & flag & movimiento=="E")
                //{
                //    respuesta = "Y";
                 
                //}
                //if (Valida.ESTADO.Equals("EN PUERTO") & movimiento == "E")
                //{
                //    respuesta = "N";
               
                //}
                //if (Valida.ESTADO.Equals("EN PUERTO") & movimiento == "S")
                //{
                //    respuesta = "Y";
                    
                //}

                if (respuesta == "1")
                {
                    registroingreso re = new registroingreso();
                    re.codigoAutorizacion = Valida.ID_PERSONA_APROBADA;
                    re.idVehichulo = Valida.ID_VEHICULO.ToString();
                    if (Valida.TIPO_MOVIMIENTO == "E")
                    {
                        re.tipomovimiento = "1";
                    }
                    else
                    {
                        re.tipomovimiento = "2";
                    }

                    re.ip = datos.ip;
                    CONEXIONSP.RegistraAccesoVehiculo(re);

                    SshClient cSSH = new SshClient("192.168.0.162", 22, "pi", "raspberry");
                    cSSH.Connect();
                    SshCommand x = cSSH.RunCommand("sudo python /home/pi/r1.py");
                    System.Threading.Thread.Sleep(1000);
                    SshCommand y = cSSH.RunCommand("sudo python /home/pi/r2.py");

                    cSSH.Disconnect();
                    cSSH.Dispose();

                }
             
                   // return respuesta;


                response.Content = new StringContent(respuesta, System.Text.Encoding.UTF8, "application/text");
                return response;
            }
            catch (Exception ex)
            {

                var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                response.Content = new StringContent("N", System.Text.Encoding.UTF8, "application/text");
                return response;
            }
        }

        // PUT: api/AccesoVehicular/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AccesoVehicular/5
        public void Delete(int id)
        {
        }
    }
}
