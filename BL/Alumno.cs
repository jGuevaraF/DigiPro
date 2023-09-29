using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Alumno
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    //le damos el nombre del SP
                    string query = "AlumnoGetAll";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.StoredProcedure;//indicamos que el query a ejecutar es un SP
                    cmd.Connection = context; //establecemos la conexion

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd); //recibimos la informacion obtenida
                    DataTable tablaAlumno = new DataTable(); //creamos la tabla donde va a ser relacionada la informacion
                    adapter.Fill(tablaAlumno); //llenamos la tabla con la informacion obtenida

                    if(tablaAlumno.Rows.Count > 0 )
                    {
                        result.Objects = new List<object>();
                        foreach(DataRow row in tablaAlumno.Rows) //vamos a recorrer por filas, por lo que creamos un DataRow
                        {
                            ML.Alumno alumno = new ML.Alumno();
                            //sacamos los datos de row y lo asigno a mi objeto
                            alumno.IdAlumno= int.Parse(row[0].ToString());
                            alumno.Nombre = row[1].ToString();
                            alumno.ApellidoPaterno = row[2].ToString();
                            alumno.ApellidoMaterno = row[3].ToString();
                            
                            result.Objects.Add( alumno ); //agregamos al alumno en mi lista de result
                        }

                        result.Correct = true;

                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros en la tabla alumno";
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

        public static ML.Result Add(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "AlumnoAdd";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = context;

                    SqlParameter[] collection = new SqlParameter[3]; //creamos los parametros del SP, y le asignamos el valor que trae nuestro objeto alumno
                    collection[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    collection[0].Value = alumno.Nombre;

                    collection[1] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
                    collection[1].Value = alumno.ApellidoPaterno;

                    collection[2] = new SqlParameter("@ApellidoMaterno", SqlDbType.VarChar);
                    collection[2].Value = alumno.ApellidoMaterno??"";

                    //agregamos las referencias al objeto cmd de SqlCommand
                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open(); //abrimos la conexion para que se pueda insertar

                    int filasAfectadas = cmd.ExecuteNonQuery(); //obtiene el numero de filas afectadas

                    if(filasAfectadas > 0)
                    {
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "ERROR al insertar";
                    }

                }

            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage= ex.InnerException.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result GetById(int idAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "AlumnoGetById";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = context;

                    cmd.Parameters.AddWithValue("@IdAlumno", idAlumno); //es lo mismo que hacer el arreglo de collection, solo que aqui se evita hacer el arreglo, y simplemente se especifica que es un nuevo parametro

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd); //se guarda la informacion obtenida
                    DataTable tablaAlumno = new DataTable();
                    adapter.Fill(tablaAlumno); //se llena la tabla con la informacion

                    if(tablaAlumno.Rows.Count > 0)
                    {
                        DataRow row = tablaAlumno.Rows[0]; //accedemos al primer resultado

                        ML.Alumno alumno = new ML.Alumno();
                        alumno.IdAlumno = int.Parse(row[0].ToString());
                        alumno.Nombre = row[1].ToString();
                        alumno.ApellidoPaterno = row[2].ToString();
                        alumno.ApellidoMaterno = row[3].ToString();

                        result.Object = alumno;

                        result.Correct = true;

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

        public static ML.Result Update (ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "AlumnoUpdate";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = context;

                    //agregamos los parametros
                    cmd.Parameters.AddWithValue("@IdAlumno", alumno.IdAlumno);
                    cmd.Parameters.AddWithValue("@Nombre", alumno.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", alumno.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", alumno.ApellidoMaterno??"");

                    //abrimos la conexion
                    cmd.Connection.Open();
                    //ejecutamos y guardamos las filas afectadas
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if(filasAfectadas > 0)
                    {
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar";
                    }

                }

            } catch (Exception ex)
            {
                result.Correct=false;
                result.ErrorMessage = ex.InnerException.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Delete(int idAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "AlumnoDelete";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = context;

                    SqlParameter[] collection = new SqlParameter[2];
                    collection[0] = new SqlParameter("@IdAlumno", SqlDbType.VarChar);
                    collection[0].Value = idAlumno;
                    
                    collection[1] = new SqlParameter("@FilasAfectas", SqlDbType.Int);
                    collection[1].Direction = ParameterDirection.Output;

                    cmd.Parameters.AddRange(collection);

                    cmd.Connection.Open();

                    cmd.ExecuteNonQuery();

                    // Obtiene el valor del parámetro de salida
                    int filasAfectadas = (int)cmd.Parameters["@FilasAfectas"].Value;

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "ERROR al eliminar";
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


    }
}
