using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroWeb.Models.Graphics
{
    public class LineChart
    {
        public string label = "";
        public string type = "";
        public List<decimal> data = new List<decimal>();
        public bool steppedLine = true;
        public int lineTension = 0;
        public bool fill = false;
        public string borderColor = "";

        public void AgregarColor(int r, int g, int b)
        {
            this.borderColor=String.Concat("rgba(", r, ",", g, ",", b, ",0.2)");
        }
        public void SetData(tb_Indi_Indicador tb_Indi_Indicador)
        {
            label = "Meta Nominal";
            type = "line";
            int idant = 0;
            foreach (tb_Indi_Indicador_Alimentar i in tb_Indi_Indicador.tb_Indi_Indicador_Alimentar.Where(T=>T.flag_eliminado=="1").OrderBy(t=>t.mes_alimentacion))
            {
                decimal meta_nominal= i.meta_nominal == null ? 0 : (decimal)i.meta_nominal;
                if(i== tb_Indi_Indicador.tb_Indi_Indicador_Alimentar.Where(T => T.flag_eliminado == "1").OrderBy(t => t.mes_alimentacion).Last())
                {
                    for (int j = idant; j < 12; j++)
                    {
                        data.Add(meta_nominal);
                    }
                }
                else
                {
                    for (int j = idant; j < i.mes_alimentacion; j++)
                    {
                        data.Add(meta_nominal);
                    }
                }
                idant = (int)i.mes_alimentacion;
            }
            steppedLine = true;
            lineTension = 0;
            fill = false;
            this.AgregarColor(19, 132, 190);
        }
        
    }
}