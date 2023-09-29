using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Materia
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ControlEscolarEntities context = new DL.ControlEscolarEntities())
                {
                    var query = context.MateriaGetAll().ToList();

                    if(query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach(var obj in query)
                        {
                            ML.Materia materia = new ML.Materia();
                            materia.IdMateria = obj.IdMateria;
                            materia.Nombre = obj.Nombre;
                            materia.Costo = (decimal)obj.Costo;
                            result.Objects.Add(materia);
                        }

                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros";
                    }
                }

            } catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.InnerException.Message;
                result.Ex = e;
            }

            return result;
        }

        public static ML.Result Add(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ControlEscolarEntities context = new DL.ControlEscolarEntities())
                {
                    int query = context.MateriaAdd(materia.Nombre, materia.Costo);

                    if(query>0)
                    {
                        result.Correct =true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al insertar la materia";
                    }
                }

            } catch (Exception ex) {
                result.Correct = false;
                result.ErrorMessage = ex.InnerException.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result GetById (int idMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ControlEscolarEntities context = new DL.ControlEscolarEntities())
                {
                    var query = context.MateriaGetById(idMateria).SingleOrDefault();

                    if(query != null)
                    {
                        ML.Materia materia = new ML.Materia();
                        materia.IdMateria = query.IdMateria;
                        materia.Nombre = query.Nombre;
                        materia.Costo = (decimal)query.Costo;
                        result.Object = materia;

                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al obtener el registro";
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

        public static ML.Result Update(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ControlEscolarEntities context = new DL.ControlEscolarEntities())
                {
                    int query = context.MateriaUpdate(materia.IdMateria, materia.Nombre, materia.Costo);

                    if (query > 0)
                    {
                        result.Correct = true;
                    } else
                    {
                        result.Correct= false;
                        result.ErrorMessage = "No se pudo actualizar";
                    }
                }

            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage += ex.InnerException.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Delete(int idMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ControlEscolarEntities context = new DL.ControlEscolarEntities())
                {
                    int query = context.MateriaDelete(idMateria);

                    if(query > 0)
                    {
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar";
                    }
                }
                 
            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage += ex.InnerException.Message;
                result.Ex = ex;
            }

            return result;
        }

    }
}
