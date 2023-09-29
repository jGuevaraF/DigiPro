using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PLAlumno.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Alumno alumno = new ML.Alumno();
            ServiceReferenceAlumno.ServiceAlumnoClient getAll = new ServiceReferenceAlumno.ServiceAlumnoClient();
            var result = getAll.GetAll();
            if (result.Correct)
            {
                alumno.Alumnos = result.Objects.ToList();
                return View(alumno);
            }
            else
            {
                return View(alumno);
            }


        }

        [HttpGet]
        public ActionResult Form(int? idAlumno) {
            ML.Alumno alumno = new ML.Alumno();

            if(idAlumno == null)//add
            {
                return View();
            } else //update
            {
                //ML.Result result = BL.Alumno.GetById(idAlumno.Value);
                ServiceReferenceAlumno.ServiceAlumnoClient getById = new ServiceReferenceAlumno.ServiceAlumnoClient();
                var result = getById.GetById(idAlumno.Value);

                if (result.Correct)
                {
                    alumno = (ML.Alumno)result.Object;
                    return View(alumno);
                } else
                {
                    ViewBag.Message = "ERROR " + result.ErrorMessage;
                    return PartialView("Modal");
                }
                
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {
            ServiceReferenceAlumno.ServiceAlumnoClient procesoAlumno = new ServiceReferenceAlumno.ServiceAlumnoClient();
            if (alumno.IdAlumno == 0)
            {
                //ML.Result result = BL.Alumno.Add(alumno);
                var result = procesoAlumno.Add(alumno);
                
                if (result.Correct)
                {
                    ViewBag.Message = "El Alumno se ha dado de alta correctamente";
                } else
                {
                    ViewBag.Message = "ERROR" + result.ErrorMessage;
                }
            }
            else
            {
                //ML.Result result = BL.Alumno.Update(alumno);
                var result = procesoAlumno.Update(alumno);
                if (result.Correct)
                {
                    ViewBag.Message = "El Alumno se ha actualizado correctamente";
                }
                else
                {
                    ViewBag.Message = "ERROR" + result.ErrorMessage;
                }
            }

            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int idAlumno)
        {
            ServiceReferenceAlumno.ServiceAlumnoClient deleteAlumno = new ServiceReferenceAlumno.ServiceAlumnoClient();
            var result = deleteAlumno.Delete(idAlumno);
            if (result.Correct)
            {
                ViewBag.Message = "El Alumno se ha borrado correctamente";
            } else
            {
                ViewBag.Message = "ERROR" + result.ErrorMessage;
            }
            return PartialView("Modal");
        }

    }
}