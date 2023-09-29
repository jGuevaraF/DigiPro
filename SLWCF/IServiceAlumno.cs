using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SLWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAlumno" in both code and config file together.
    [ServiceContract]
    public interface IServiceAlumno
    {
        [OperationContract]
        [ServiceKnownType(typeof(ML.Alumno))]//le especificamos que lo debe deserealizar como un ML.Alumno
        SLWCF.Result GetAll();

        [OperationContract]
        SLWCF.Result Add(ML.Alumno alumno);

        [OperationContract]
        SLWCF.Result Update(ML.Alumno alumno);

        [OperationContract]
        [ServiceKnownType (typeof(ML.Alumno))]
        SLWCF.Result GetById(int idAlumno);

        [OperationContract]
        SLWCF.Result Delete(int idAlumno);
    }
}
