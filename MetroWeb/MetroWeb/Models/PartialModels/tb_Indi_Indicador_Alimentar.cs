using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroWeb.Models
{
    public partial class tb_Indi_Indicador_Alimentar
    {
        private string fechaAlimentacion = "";
        public string FechaAlimentacion
        {
            set { fechaAlimentacion = value; }
            get { return fechaAlimentacion; }
        }
    }
}