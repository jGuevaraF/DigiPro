using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AlumnoMateria
    {
        public static ML.Result GetAll(int idAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ControlEscolarEntities context  = new DL.ControlEscolarEntities())
                {
                    var query = context.AlumnoMateriaGetAll(idAlumno).ToList();

                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var obj in query)
                        {
                            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
                            alumnoMateria.Materia = new ML.Materia();

                            alumnoMateria.Materia.IdMateria = obj.IdMateria;
                            alumnoMateria.Materia.Nombre = obj.MateriaNombre;
                            alumnoMateria.Materia.Costo = (decimal)obj.Costo;

                            result.Objects.Add(alumnoMateria);
                        }

                        result.Correct = true;

                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay materias";
                    }
                }

            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.InnerException.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result GetMateriasAsignadas(int idAlumno)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DL.ControlEscolarEntities context = new DL.ControlEscolarEntities())
                {
                    var query = context.GetMateriasAsignadas(idAlumno).ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
                            alumnoMateria.Materia = new ML.Materia();

                            alumnoMateria.Materia.IdMateria = obj.IdMateria;
                            alumnoMateria.Materia.Nombre = obj.Nombre;
                            alumnoMateria.Materia.Costo = (decimal)obj.Costo;

                            result.Objects.Add(alumnoMateria);
                        }

                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay materias";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.InnerException.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Add(int idAlumno, int idMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ControlEscolarEntities context = new DL.ControlEscolarEntities())
                {
                    int query = context.AlumnoMateriaAdd(idAlumno, idMateria);
                    if (query > 0)
                    {
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo agregar";
                    }
                }

            } catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.InnerException.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Delete(int idAlumno, int idMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.ControlEscolarEntities context = new DL.ControlEscolarEntities())
                {
                    int query = context.AlumnoMateriaDelete(idAlumno, idMateria);
                    if(query > 0)
                    {
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se puedo eliminar";
                    }
                }

            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.InnerException.Message;
                result.Ex = ex;
            }

            return result;
        }

    }
}
