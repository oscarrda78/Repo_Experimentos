using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MetroWeb.Models
{
    public partial class tb_Indi_Indicador
    {
        public bool EsPublico {
            get { return publico == "1" ? true : false; }
            set { publico = value == true ? "1" : "0"; }
        }
        public List<int> sistemasGestion
        {
            get;
            set;
        }
        public string sistemasGestionJoin {
            get { return string.Join(", ", tb_Indi_Indicador_SistemaGestion.Select(t => t.tb_Orga_SistemasGestion).Select(t => t.Nombre)); }
            set {; }
        }
        public string metaMinimaString
        {
            get { return meta_minima.ToString(); }
            set {meta_minima = Convert.ToDecimal(value==""?"0.0":value, new CultureInfo("en-US")); }
        }
        public string metaNominalString
        {
            get { return meta_nominal.ToString(); }
            set { meta_nominal = Convert.ToDecimal(value == "" ? "0.0" : value, new CultureInfo("en-US")); }
        }
        public string metaMaximaString
        {
            get { return meta_maxima.ToString(); }
            set { meta_maxima = Convert.ToDecimal(value == "" ? "0.0" : value, new CultureInfo("en-US")); }
        }
        [Required]
        public string frecuencaString
        {

            get { return frecuencia.ToString(); }
            set { frecuencia = Convert.ToInt32(value); }
        }
    }
}