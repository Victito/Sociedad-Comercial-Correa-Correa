using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SociedadCorreaCorrea
{
    public static class UserSession
    {
        public static long Id { get; set; }
        public static string NombreUsuario { get; set; } = string.Empty;
        public static string Rol { get; set; } = string.Empty;
    }
}
