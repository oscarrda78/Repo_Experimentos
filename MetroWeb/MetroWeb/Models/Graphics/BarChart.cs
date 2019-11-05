using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroWeb.Models.Graphics
{
    public class BarChart
    {
        
        public  string label = "";
        public List<decimal?> data = new List<decimal?>() {  null,null,null,null,null,null,null, null, null, null, null, null };
        public List<string> backgroundColor = new List<string>() { "", "", "", "", "", "", "", "", "", "", "", "" };
        
        public List<string> bordergroundColor = new List<string>() { "", "", "", "", "", "", "", "", "", "", "", "" };
        public int borderWidth = 1;
       
        public void AgregarColor(int id,int r, int g, int b)
        {
            
            this.backgroundColor[id] = (String.Concat("rgba(", r,",", g, ",", b, ",0.2)"));
            this.bordergroundColor[id] = (String.Concat("rgba(", r, ",", g, ",", b, ",1)"));
        }

        public void SetData(tb_Indi_Indicador tb_Indi_Indicador, int anioSelected)
        {
            this.label = anioSelected.ToString();



            foreach (tb_Indi_Indicador_Alimentar i in tb_Indi_Indicador.tb_Indi_Indicador_Alimentar.Where(t=>t.flag_eliminado=="1" & t.anio_alimentacion == anioSelected))
            {
                decimal denominador = i.denominador == null ? 0 : (decimal)i.denominador;
                decimal numerador = i.numerador == null ? 0 : (decimal)i.numerador;
                if (tb_Indi_Indicador.nominador_denominador == "0")
                {
                    if (i.mes_alimentacion != null)
                    {
                        this.data[(int)i.mes_alimentacion - 1] = numerador;

                        if (numerador < i.meta_minima)
                        {
                            this.AgregarColor((int)i.mes_alimentacion - 1, 255, 99, 132);
                        }
                        else if (i.meta_minima <= numerador && numerador < i.meta_nominal)
                        {
                            this.AgregarColor((int)i.mes_alimentacion - 1, 255, 206, 86);
                        }
                        else if (i.meta_nominal <= numerador && numerador < i.meta_maxima)
                        {
                            this.AgregarColor((int)i.mes_alimentacion - 1, 75, 192, 192);
                        }
                        else
                        {
                            this.AgregarColor((int)i.mes_alimentacion - 1, 54, 162, 235);
                        }
                    }
                }
                else
                {
                    if (denominador == 0)
                    {
                        denominador = (decimal)0.00001;
                    }
                    decimal resultado = decimal.Round((numerador / denominador) * 100,2);

                    if (i.mes_alimentacion != null)
                    {
                        this.data[(int)i.mes_alimentacion - 1] = resultado;

                        if (resultado < i.meta_minima)
                        {
                            this.AgregarColor((int)i.mes_alimentacion - 1,255, 99, 132);
                        }
                        else if (i.meta_minima <= resultado && resultado < i.meta_nominal)
                        {
                            this.AgregarColor((int)i.mes_alimentacion - 1,255, 206, 86);
                        }
                        else if (i.meta_nominal <= resultado && resultado < i.meta_maxima)
                        {
                            this.AgregarColor((int)i.mes_alimentacion - 1,75, 192, 192);
                        }
                        else
                        {
                            this.AgregarColor((int)i.mes_alimentacion - 1,54, 162, 235);
                        }
                    }
                }
            }
            this.borderWidth = 1;
        }

        public void SetDataFiltrada(tb_Indi_Indicador tb_Indi_Indicador, int anioSelected, int mesInicio, int mesFin)
        {
            this.label = anioSelected.ToString();



            foreach (tb_Indi_Indicador_Alimentar i in tb_Indi_Indicador.tb_Indi_Indicador_Alimentar.Where(t => t.flag_eliminado == "1" & t.anio_alimentacion == anioSelected & t.mes_alimentacion>= mesInicio & t.mes_alimentacion<=mesFin))
            {
                decimal denominador = i.denominador == null ? 0 : (decimal)i.denominador;
                decimal numerador = i.numerador == null ? 0 : (decimal)i.numerador;
                if (tb_Indi_Indicador.nominador_denominador == "0")
                {
                    if (i.mes_alimentacion != null)
                    {
                        this.data[(int)i.mes_alimentacion - 1] = numerador;

                        if (numerador < i.meta_minima)
                        {
                            this.AgregarColor((int)i.mes_alimentacion - 1, 255, 99, 132);
                        }
                        else if (i.meta_minima <= numerador && numerador < i.meta_nominal)
                        {
                            this.AgregarColor((int)i.mes_alimentacion - 1, 255, 206, 86);
                        }
                        else if (i.meta_nominal <= numerador && numerador < i.meta_maxima)
                        {
                            this.AgregarColor((int)i.mes_alimentacion - 1, 75, 192, 192);
                        }
                        else
                        {
                            this.AgregarColor((int)i.mes_alimentacion - 1, 54, 162, 235);
                        }
                    }
                }
                else
                {
                    if (denominador == 0)
                    {
                        denominador = (decimal)0.00001;
                    }
                    decimal resultado = decimal.Round((numerador / denominador) * 100, 2);

                    if (i.mes_alimentacion != null)
                    {
                        this.data[(int)i.mes_alimentacion - 1] = resultado;

                        if (resultado < i.meta_minima)
                        {
                            this.AgregarColor((int)i.mes_alimentacion - 1, 255, 99, 132);
                        }
                        else if (i.meta_minima <= resultado && resultado < i.meta_nominal)
                        {
                            this.AgregarColor((int)i.mes_alimentacion - 1, 255, 206, 86);
                        }
                        else if (i.meta_nominal <= resultado && resultado < i.meta_maxima)
                        {
                            this.AgregarColor((int)i.mes_alimentacion - 1, 75, 192, 192);
                        }
                        else
                        {
                            this.AgregarColor((int)i.mes_alimentacion - 1, 54, 162, 235);
                        }
                    }
                }
            }
            this.borderWidth = 1;
        }
    }
}