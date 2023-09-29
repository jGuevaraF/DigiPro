using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Materia
    {
        public int IdMateria { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Solo se permiten letras.")]
        public string Nombre { get; set; }

        [Required]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Solo se permiten números y decimales.")]
        public decimal Costo { get; set; }

        public List<object> Materias { get; set; }
    }
}
