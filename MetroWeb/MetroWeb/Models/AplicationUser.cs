using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroWeb.Models
{
    public static class AplicationUser
    {
        private static string usuario = "";
        private static string nombreCompleto = "";
        private static string puesto = "";
        private static string rool = "";

        public static string NombreCompleto {
            get { return nombreCompleto; }
            set { nombreCompleto = value; }
        }
        public static string Puesto {
            get { return puesto; }
            set { puesto = value; }
        }
        public static string Rool {
            get { return rool; }
            set { rool = value; }
        }
        public static string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }
    }
}