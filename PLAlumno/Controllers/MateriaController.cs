using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;

namespace PLAlumno.Controllers
{
    public class MateriaController : Controller
    {
        // GET: Materia
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = new ML.Result();
            ML.Materia materia = new ML.Materia();

            using (var getAll = new HttpClient())
            {
                string URLapi = ConfigurationManager.AppSettings["URLapi"]; //obtenemos la URL de la api
                getAll.BaseAddress = new Uri(URLapi);
                var respuesta = getAll.GetAsync("materia");
                respuesta.Wait();
                var respuestaGetAll = respuesta.Result;

                if(respuestaGetAll.IsSuccessStatusCode)
                {
                    result.Objects = new List<object>();
                    var leerMaterias = respuestaGetAll.Content.ReadAsAsync<ML.Result>(); //en respuestaGetAll viene el objeto ML.Result y se lo pasamos a leerMaterias
                    leerMaterias.Wait();

                    foreach(var obj in leerMaterias.Result.Objects)
                    {
                        ML.Materia materias = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(obj.ToString()); //deserealiza el objeto de Objects y lo pasa a un objeto de tipo empleado
                        result.Objects.Add(materias);
                    }

                    materia.Materias = result.Objects;

                    return View(materia);

                } else
                {
                    return View(materia);
                }

            }
                
        }

        [HttpGet]
        public ActionResult Form(int? idMateria)
        {
            ML.Materia materia = new ML.Materia();
            if(idMateria == null) //add
            {
                return View(materia);
            } else //update
            {
                using(var materiaGetById = new HttpClient())
                {
                    string URLapi = ConfigurationManager.AppSettings["URLapi"];
                    materiaGetById.BaseAddress = new Uri(URLapi);
                    var respuesta = materiaGetById.GetAsync("materia/" + idMateria);
                    respuesta.Wait();

                    var resultGetById = respuesta.Result;

                    if (resultGetById.IsSuccessStatusCode)
                    {
                        var readTask = resultGetById.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        ML.Materia resultMateria = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(readTask.Result.Object.ToString());
                        materia = resultMateria;
                    }

                }
                return View(materia);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {
            if (ModelState.IsValid)
            {

                if (materia.IdMateria == 0) //agregar
                {
                    using (var addMateria = new HttpClient())
                    {
                        string URLapi = ConfigurationManager.AppSettings["URLapi"];
                        addMateria.BaseAddress = new Uri(URLapi);

                        var respuestaAdd = addMateria.PostAsJsonAsync<ML.Materia>("materia", materia);
                        respuestaAdd.Wait();

                        var resultAdd = respuestaAdd.Result;

                        if (resultAdd.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "La materia se ha agregado correctamente";
                        }

                    }
                }
                else //update
                {
                    using (var updateMateria = new HttpClient())
                    {
                        string URLapi = ConfigurationManager.AppSettings["URLapi"];
                        updateMateria.BaseAddress = new Uri(URLapi);

                        var respuestaUpdate = updateMateria.PutAsJsonAsync<ML.Materia>("materia/" + materia.IdMateria, materia);
                        respuestaUpdate.Wait();

                        var resultUpdate = respuestaUpdate.Result;

                        if (resultUpdate.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "La materia se ha actualizado correctamente";
                        }

                    }
                }

                return PartialView("Modal");
            } else
            {
                return View(materia);
            }

                
        }

        [HttpGet]
        public ActionResult Delete(int idMateria)
        {
            using (var deleteMateria = new HttpClient())
            {
                string URLapi = ConfigurationManager.AppSettings["URLapi"];
                deleteMateria.BaseAddress = new Uri(URLapi);

                var resultDelete = deleteMateria.DeleteAsync("materia/" + idMateria);
                resultDelete.Wait();

                var respuesta = resultDelete.Result;
                if(respuesta.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Se ha eliminado correctamente";
                } else
                {
                    //Aquí accedes a la propiedad errorMessage del objeto Result
                    var result = respuesta.Content.ReadAsAsync<ML.Result>().Result;

                    ViewBag.Message = "ERROR " + result.ErrorMessage;
                }
            }

            return PartialView("Modal");
        }


    }
}