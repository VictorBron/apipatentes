using APIACCESOREST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIACCESOREST.Controllers
{
    public class ImagenController : ApiController
    {
        // GET: api/Imagen
        public HttpResponseMessage Get(int rut,int idFoto)
        {
            try
            {
                Imagen pi = CONEXIONSP.ImagenPersona(rut, idFoto);


                var data2 = new
                {
                    rut = pi.Rut,
                    idFoto = pi.idFoto,
                    imagen = pi.fotoB64
                };
                return this.Request.CreateResponse(HttpStatusCode.OK, data2);


            }
            catch (Exception exp)
            {
                var data2 = new
                {
                    mensaje = "error " + exp.Message.ToString()

                };
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, data2);

            }

        }

        // GET: api/Imagen/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Imagen
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Imagen/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Imagen/5
        public void Delete(int id)
        {
        }
    }
}
