using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetroWeb.Models;

namespace MetroWeb.Models
{
    public partial class tb_Orga_Usuarios
    {
        public string NombreCompleto
        {
            get { return string.Concat(Nombres, " ", Apellidos); }
            set { }
        }
    }
}