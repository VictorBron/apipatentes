using APIACCESOREST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIACCESOREST.Controllers
{
    public class PersonasController : ApiController
    {
        // GET: api/Personas
        public HttpResponseMessage Get()
        {
            try
            {
                List<Personas_Imagenes> LP = CONEXIONSP.PersonasImagenes();


                var data2 = new
                {
                    mensaje = "ok",
                    data = LP
                };
                return this.Request.CreateResponse(HttpStatusCode.OK, data2);


            }catch(Exception exp)
            {
                var data2 = new
                {
                    mensaje = "error " + exp.Message.ToString()
               
                };
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, data2);

            }
          
        }

        // GET: api/Personas/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Personas
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Personas/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Personas/5
        public void Delete(int id)
        {
        }
    }
}
