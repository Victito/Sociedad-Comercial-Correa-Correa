using SociedadCorreaCorrea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SociedadCorreaCorrea.ModelsCustoms
{
    public class InformacionEmpleados
    {
        public Empleado Empleado { get; set; }
        public string NotasAdicionales { get; set; } // Propiedad adicional para información extra
        public string InfoTurno { get; set; }
    }


}
