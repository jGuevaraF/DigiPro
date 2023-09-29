using Microsoft.Ajax.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PLAlumno.Controllers
{
    public class AlumnoMateriaController : Controller
    {
        // GET: AlumnoMateria
        [HttpGet]
        public ActionResult GetAll()
        {
            Session["IdAlumno"] = null;
            Session["Nombre"] = null;
            Session["ApellidoPaterno"] = null;
            Session["ApellidoMaterno"] = null;

            ML.Alumno alumno = new ML.Alumno();
            ML.Result result = BL.Alumno.GetAll();
            if (result.Correct)
            {
                alumno.Alumnos = result.Objects;
                return View(alumno);
            }
            else
            {
                return View(alumno);
            }

        }

        [HttpGet]
        public ActionResult GetAllAlumnoMateria(int? idAlumno, string nombre, string apellidoPaterno, string apellidoMaterno)
        {
            if (Session["IdAlumno"] == null)
            {
                Session["IdAlumno"] = idAlumno.Value;
                Session["Nombre"] = nombre;
                Session["ApellidoPaterno"] = apellidoPaterno;
                Session["ApellidoMaterno"] = apellidoMaterno;
            }



            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
            ML.Result resultMateriasAsignadas = BL.AlumnoMateria.GetAll(int.Parse(Session["IdAlumno"].ToString()));

            if (resultMateriasAsignadas.Correct)
            {
                alumnoMateria.Materia = new ML.Materia();
                alumnoMateria.Materia.Materias = resultMateriasAsignadas.Objects;
                return View(alumnoMateria);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult TablaMateriasAdd()
        {
            string sesion = (string)Session["IdAlumno"].ToString();
            int idAlumno = int.Parse(sesion);

            ML.AlumnoMateria materias = new ML.AlumnoMateria();
            ML.Result result = BL.AlumnoMateria.GetMateriasAsignadas(idAlumno);
            if (result.Correct)
            {
                materias.Materia = new ML.Materia();
                materias.Materia.Materias = result.Objects;
                return View(materias);
            }
            else
            {
                return View();

            }
        }

        [HttpPost]
        public ActionResult TablaMateriasAdd(List<int> idMaterias)
        {
            string sesion = Session["IdAlumno"].ToString();
            int idAlumno = int.Parse(sesion);
            int count = (idMaterias ?? new List<int>()).Count; //si no se selecciona ninguna materia asignar, se colo un count de 0 para validarlo


            if (count > 0)
            {
                foreach(int id in idMaterias)
                {
                    ML.Result result = BL.AlumnoMateria.Add(idAlumno, id);
                    if(!result.Correct)
                    {
                        ViewBag.Message = "Error al asignar una Materia";
                        return PartialView("Modal");
                    }
                }

                ViewBag.Message = "Asignatura Asignadas";
            } else
            {
                ViewBag.Message = "No seleccionaste ninguna materia nueva";
            }
            

            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int idMateria)
        {
            string sesion = Session["IdAlumno"].ToString() ;
            int idUsuario = int.Parse(sesion) ;
            ML.Result result = BL.AlumnoMateria.Delete(idUsuario, idMateria);
            if (result.Correct)
            {
                ViewBag.Message = "Se ha dado de baja el alumno de la materia.";
            } else
            {
                ViewBag.Message = "ERROR " + result.ErrorMessage;
            }

            return PartialView("Modal");
        }


    }
}