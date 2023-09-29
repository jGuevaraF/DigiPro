using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SLWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceAlumno" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceAlumno.svc or ServiceAlumno.svc.cs at the Solution Explorer and start debugging.
    public class ServiceAlumno : IServiceAlumno
    {
        public SLWCF.Result GetAll()
        {
            ML.Result result = BL.Alumno.GetAll();
            return new SLWCF.Result
            {
                Correct = result.Correct,
                Object = result.Object,
                Objects = result.Objects,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex
            };
        }

        public SLWCF.Result Add (ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.Add (alumno);
            return new SLWCF.Result
            {
                Correct = result.Correct,
                Object = result.Object,
                Objects = result.Objects,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex
            };
        }

        public SLWCF.Result Update(ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.Update(alumno);
            return new SLWCF.Result
            {
                Correct = result.Correct,
                Object = result.Object,
                Objects = result.Objects,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex
            };
        }

        public SLWCF.Result GetById(int idAlumno)
        {
            ML.Result result = BL.Alumno.GetById (idAlumno);
            return new SLWCF.Result
            {
                Correct = result.Correct,
                Object = result.Object,
                Objects = result.Objects,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex
            };
        }

        public SLWCF.Result Delete(int idAlumno)
        {
            ML.Result result = BL.Alumno.Delete(idAlumno);
            return new SLWCF.Result
            {
                Correct = result.Correct,
                Object = result.Object,
                Objects = result.Objects,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex
            };
        }
    }
}
