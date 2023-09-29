using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SLWebApi.Controllers
{
    [RoutePrefix("api/materia")] // ruta fija
    public class MateriaController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Materia.GetAll();
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result); //OK codigo 200
            } else
            {
                return Content(HttpStatusCode.BadRequest, result); //error del cliente 400
            }
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(ML.Materia materia)
        {
            ML.Result result = BL.Materia.Add(materia);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result); //OK codigo 200
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result); //error del cliente 400
            }
        }

        [Route("{idMateria}")]
        [HttpGet]
        public IHttpActionResult GetById(int idMateria)
        {
            ML.Result result = BL.Materia.GetById(idMateria);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result); //OK codigo 200
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result); //error del cliente 400
            }
        }

        [Route("{idMateria}")]
        [HttpPut]
        public IHttpActionResult Update(int idMateria, [FromBody] ML.Materia materia)
        { 
            materia.IdMateria = idMateria;

            ML.Result result = BL.Materia.Update(materia);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result); //OK codigo 200
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result); //error del cliente 400
            }
        }

        [Route("{idMateria}")]
        [HttpDelete]
        public IHttpActionResult Delete(int idMateria)
        {
            ML.Result result = BL.Materia.Delete(idMateria);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result); //OK codigo 200
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result); //error del cliente 400
            }
        }

    }
}
