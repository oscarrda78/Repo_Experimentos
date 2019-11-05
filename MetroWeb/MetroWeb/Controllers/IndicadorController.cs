using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MetroWeb.AuthFolder;
using MetroWeb.Models;
using MetroWeb.Models.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MetroWeb.Controllers
{
    [UserAuthentication]
    public class IndicadorController : Controller
    {
        private BDGestionIntegradaEntitiesContext db = new BDGestionIntegradaEntitiesContext();
        // 1
        public ActionResult Index()
        {
            
            ViewBag.RutaLayout = VariablesGlobales.LoggedInLayoutPath;

            var tb_Indi_Indicador = db.tb_Indi_Indicador.Include(t => t.tb_Indi_Tipo_Indicador).Include(t => t.tb_Orga_Area).Include(t => t.tb_Orga_Estados).Where(t=>t.flag_eliminado=="1");
            return View(tb_Indi_Indicador.ToList());
        }

        // 2
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Indi_Indicador tb_Indi_Indicador = db.tb_Indi_Indicador.Find(id);
            if (tb_Indi_Indicador == null)
            {
                return HttpNotFound();
            }
            
            switch (tb_Indi_Indicador.frecuencia)
            {
                case 1:
                    ViewBag.FrecuenciaMedicion = "Mensual";
                    break;
                case 2:
                    ViewBag.FrecuenciaMedicion = "Bimestral";
                    break;
                case 3:
                    ViewBag.FrecuenciaMedicion = "Trimestral";
                    break;
                case 4:
                    ViewBag.FrecuenciaMedicion = "Mensual";
                    break;
                default:
                    ViewBag.FrecuenciaMedicion="";
                    break;
            }
            ViewBag.IndicadorId = tb_Indi_Indicador.id_indicador;
            ViewBag.EsNumeradorDenominador = tb_Indi_Indicador.nominador_denominador == "0" ? "No" : "Sí";
            ViewBag.UsuarioAlimentador = db.tb_Orga_Usuarios.Where(t => t.Id_Usuario == tb_Indi_Indicador.id_usuario_alimentador).Select(t=>t.Nombres+" "+t.Apellidos).FirstOrDefault().ToString();
            ViewBag.EsPublico = tb_Indi_Indicador.EsPublico ? "Si" : "No";
            return PartialView("_Detalles",tb_Indi_Indicador);
        }

        // 3
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.id_tipo_indicador = new SelectList(db.tb_Indi_Tipo_Indicador, "id", "descripcion");
            ViewBag.id_area = new SelectList(db.tb_Orga_Area, "Id_Area", "Descripcion_Area");
            ViewBag.id_usuario_alimentador = new SelectList(db.tb_Orga_Usuarios, "Id_Usuario", "NombreCompleto");
            ViewBag.sistemas_gestion = new SelectList(db.tb_Orga_SistemasGestion.Where(x=>x.Id_Estado==1).Where(x=>x.Flag_Eliminado=="1"), "Id_SistemaGestion", "Nombre");
            return PartialView("_Agregar");
        }

        // 4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "descripcion_denominador,descripcion_numerador,descripcion_valor_unico,nominador_denominador,id_usuario_alimentador,frecuencaString,metaMinimaString,metaNominalString,metaMaximaString,nombre_indicador,descripcion,abreviatura,id_tipo_indicador,id_estado,id_area,EsPublico,sistemasGestion")] tb_Indi_Indicador tb_Indi_Indicador)
        {
            if (ModelState.IsValid)
            {
                tb_Indi_Indicador.id_estado = 1;
                tb_Indi_Indicador.usuario_creacion = AplicationUser.NombreCompleto;
                tb_Indi_Indicador.fecha_creacion = DateTime.Now;
                string codigo_area = (from t in db.tb_Orga_Area where t.Id_Area == tb_Indi_Indicador.id_area select t.Codigo).First(); 
                string codigo_tipo_indicador = (from t in db.tb_Indi_Tipo_Indicador where t.id==tb_Indi_Indicador.id_tipo_indicador select t.codigo).First();
                tb_Indi_Indicador.flag_eliminado = "1";
                int? correlativo_int= (from t in db.tb_Indi_Indicador where t.id_tipo_indicador == tb_Indi_Indicador.id_tipo_indicador && t.id_area== tb_Indi_Indicador.id_area select t.correlativo_int).Max();
                tb_Indi_Indicador.correlativo_int = correlativo_int == null ? 1 : correlativo_int + 1;
                string correlativo = String.Concat("000", correlativo_int == null ? "1" : (correlativo_int + 1).ToString());
                tb_Indi_Indicador.abreviatura = String.Concat(codigo_tipo_indicador, "-", codigo_area, "-", correlativo.Substring(correlativo.Length-3,3));
                
                List<tb_Indi_Indicador_SistemaGestion> nuevoArr = new List<tb_Indi_Indicador_SistemaGestion>();
                if(tb_Indi_Indicador.sistemasGestion != null)
                {
                    foreach (int i in tb_Indi_Indicador.sistemasGestion)
                    {
                        tb_Indi_Indicador_SistemaGestion nuevo = new tb_Indi_Indicador_SistemaGestion();
                        nuevo.id_sistema_gestion = i;
                        nuevo.usuario_creacion = AplicationUser.NombreCompleto;
                        nuevo.fecha_creacion = DateTime.Now;
                        nuevoArr.Add(nuevo);
                    }
                    tb_Indi_Indicador.tb_Indi_Indicador_SistemaGestion = nuevoArr;
                }
               
                db.tb_Indi_Indicador.Add(tb_Indi_Indicador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty,"Error en el Formulario");
            ViewBag.id_tipo_indicador = new SelectList(db.tb_Indi_Tipo_Indicador, "id", "descripcion", tb_Indi_Indicador.id_tipo_indicador);
            ViewBag.id_area = new SelectList(db.tb_Orga_Area, "Id_Area", "Descripcion_Area", tb_Indi_Indicador.id_area);
            ViewBag.sistemas_gestion = new SelectList(db.tb_Orga_SistemasGestion.Where(x => x.Id_Estado == 1).Where(x => x.Flag_Eliminado == "1"), "Id_SistemaGestion", "Nombre",tb_Indi_Indicador.sistemasGestion);
            return PartialView("_Agregar", tb_Indi_Indicador);
        }


        // 5
        [HttpGet]
        public ActionResult Grafico(int? id)
        {
           
            LineChart newlineChart = new LineChart();
            tb_Indi_Indicador tb_Indi_Indicador = db.tb_Indi_Indicador.Find(id);
            List<int?> aniosSelected = tb_Indi_Indicador.tb_Indi_Indicador_Alimentar.Select(t => t.anio_alimentacion).Distinct().ToList();
            object[] datasets = new object[aniosSelected.Count+1];
            int j = 0;
            foreach (int i in aniosSelected)
            {
                BarChart newBarChart = new BarChart();
                newBarChart.SetData(tb_Indi_Indicador,i);
                datasets[j] = newBarChart;
                j += 1;
            }
            newlineChart.SetData(tb_Indi_Indicador);
            datasets[aniosSelected.Count] = newlineChart;
            return Json(datasets, JsonRequestBehavior.AllowGet);
        }
        // 6
        [HttpGet]
        public ActionResult GraficoFiltrado(int? id,string startDate, string endDate)
        {

            LineChart newlineChart = new LineChart();



            List<string> shortMonthNameList = new List<string>() { "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };


            int anioInicio = Convert.ToInt32(startDate.Split('-')[1]);
            int mesInicio = shortMonthNameList.IndexOf(startDate.Split('-')[0]);
            int anioFin = Convert.ToInt32(endDate.Split('-')[1]);
            int mesFin = shortMonthNameList.IndexOf(endDate.Split('-')[0]);

            tb_Indi_Indicador tb_Indi_Indicador = db.tb_Indi_Indicador.Find(id);
            List<int?> aniosSelected = tb_Indi_Indicador.tb_Indi_Indicador_Alimentar.Where(t=>t.mes_alimentacion>=mesInicio & t.mes_alimentacion<=mesFin).Select(t => t.anio_alimentacion).Distinct().ToList();
            object[] datasets = new object[aniosSelected.Count + 1];
            int j = 0;
            foreach (int i in aniosSelected)
            {

                    BarChart newBarChart = new BarChart();
                    int minMes = (int)tb_Indi_Indicador.tb_Indi_Indicador_Alimentar.Where(t => t.anio_alimentacion==i).Select(t => t.mes_alimentacion).Min();
                    int maxMes = (int)tb_Indi_Indicador.tb_Indi_Indicador_Alimentar.Where(t => t.anio_alimentacion == i).Select(t => t.mes_alimentacion).Max();
                    newBarChart.SetDataFiltrada(tb_Indi_Indicador, i,minMes,maxMes);
                    datasets[j] = newBarChart;
                
            }
            newlineChart.SetData(tb_Indi_Indicador);
            datasets[aniosSelected.Count] = newlineChart;
            return Json(datasets, JsonRequestBehavior.AllowGet);
        }

        // 7
        [HttpGet]
        public ActionResult Alimentar(int? id)
        {
            ViewBag.RutaLayout = VariablesGlobales.EmptyLayoutPath;
            tb_Indi_Indicador tb_Indi_Indicador = db.tb_Indi_Indicador.Find(id);
            ViewBag.NombreIndicador = tb_Indi_Indicador.nombre_indicador;
            ViewBag.IndicadorId = tb_Indi_Indicador.id_indicador;
            ViewBag.Flag =tb_Indi_Indicador.nominador_denominador;

            int step = Convert.ToInt32(tb_Indi_Indicador.frecuencia);


            DateTime fechaActual = DateTime.Now;

            int mesReal = fechaActual.Month;
            int anioReal = fechaActual.Year;

            int mesCreacion = Convert.ToDateTime(tb_Indi_Indicador.fecha_creacion).Month;
            int anioCreacion = Convert.ToDateTime(tb_Indi_Indicador.fecha_creacion).Year;

            int diffMeses = (anioReal - anioCreacion) * 12 + (mesReal - mesCreacion);

            int numMeses = ((int)diffMeses/step) * step;

            int mesActual = (mesCreacion + numMeses) % 12;
            int anioActual = ((int)(mesCreacion + numMeses) / 12) + anioCreacion;

            int mesAnterior = (mesCreacion + numMeses - step) % 12;
            int anioAnterior = ((int)(mesCreacion + numMeses - step) / 12) + anioCreacion;

            string[] monthNames = CultureInfo.CurrentCulture
               .DateTimeFormat.MonthGenitiveNames;

            ViewBag.FechaAnterior= String.Concat(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(monthNames[mesAnterior -1]), "-", anioAnterior);
            ViewBag.FechaActual= String.Concat(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(monthNames[mesActual -1]), "-", anioActual);

            return View("Alimentar");
        }

        // 8
        [HttpPost, ActionName("Alimentar")]
        [ValidateAntiForgeryToken]
        public ActionResult Alimentar([Bind(Include = "FechaAlimentacion,numerador,denominador")]tb_Indi_Indicador_Alimentar tb_Indi_Indicador_Alimentar, int? id_indicador)
        {
            tb_Indi_Indicador tb_Indi_Indicador = db.tb_Indi_Indicador.Find(id_indicador);
            tb_Indi_Indicador_Alimentar.meta_minima = tb_Indi_Indicador.meta_minima;
            tb_Indi_Indicador_Alimentar.meta_nominal = tb_Indi_Indicador.meta_nominal;
            tb_Indi_Indicador_Alimentar.meta_maxima = tb_Indi_Indicador.meta_maxima;
            tb_Indi_Indicador_Alimentar.tb_Indi_Indicador = tb_Indi_Indicador;
            tb_Indi_Indicador_Alimentar.anio_alimentacion = Convert.ToInt32(tb_Indi_Indicador_Alimentar.FechaAlimentacion.Split('-').Last());
            tb_Indi_Indicador_Alimentar.flag_eliminado = "1";
            List<string> monthNames = CultureInfo.CurrentCulture
               .DateTimeFormat.MonthGenitiveNames.ToList();
            string mes_string = tb_Indi_Indicador_Alimentar.FechaAlimentacion.Split('-').First().ToLower();
            int mes = Convert.ToInt32(monthNames.IndexOf(mes_string));
            tb_Indi_Indicador_Alimentar.mes_alimentacion = mes+1;
            var modifyEntries= db.tb_Indi_Indicador_Alimentar.Where(t => t.flag_eliminado == "1" & t.id_indicador==tb_Indi_Indicador.id_indicador 
            & t.mes_alimentacion== tb_Indi_Indicador_Alimentar.mes_alimentacion & t.anio_alimentacion== tb_Indi_Indicador_Alimentar.anio_alimentacion);
            foreach(tb_Indi_Indicador_Alimentar i in modifyEntries)
            {
                i.flag_eliminado = "0";
                db.Entry(i).State = EntityState.Modified;
            }

            
            db.SaveChanges();
            db.tb_Indi_Indicador_Alimentar.Add(tb_Indi_Indicador_Alimentar);
            db.SaveChanges();
            return RedirectToAction("Alimentar", "Indicador", new { id = tb_Indi_Indicador.id_indicador });
        }


        //// GET: Indicador/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tb_Indi_Indicador tb_Indi_Indicador = db.tb_Indi_Indicador.Find(id);
        //    if (tb_Indi_Indicador == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.id_tipo_indicador = new SelectList(db.tb_Indi_Tipo_Indicador, "id", "descripcion", tb_Indi_Indicador.id_tipo_indicador);
        //    ViewBag.id_area = new SelectList(db.tb_Orga_Area, "Id_Area", "Descripcion_Area", tb_Indi_Indicador.id_area);
        //    ViewBag.id_estado = new SelectList(db.tb_Orga_Estados, "Id_Estado", "Descripcion", tb_Indi_Indicador.id_estado);
        //    return View(tb_Indi_Indicador);
        //}

        //// POST: Indicador/Edit/5
        //// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        //// más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "nombre_indicador,descripcion,abreviatura,id_tipo_indicador,id_estado,id_area,EsPublico")] tb_Indi_Indicador tb_Indi_Indicador)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tb_Indi_Indicador).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.id_tipo_indicador = new SelectList(db.tb_Indi_Tipo_Indicador, "id", "descripcion", tb_Indi_Indicador.id_tipo_indicador);
        //    ViewBag.id_area = new SelectList(db.tb_Orga_Area, "Id_Area", "Descripcion_Area", tb_Indi_Indicador.id_area);
        //    ViewBag.id_estado = new SelectList(db.tb_Orga_Estados, "Id_Estado", "Descripcion", tb_Indi_Indicador.id_estado);
        //    return View(tb_Indi_Indicador);
        //}


        // 9
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_Indi_Indicador tb_Indi_Indicador = db.tb_Indi_Indicador.SingleOrDefault(t=> t.id_indicador==id);
            tb_Indi_Indicador.flag_eliminado = "0";
            db.Entry(tb_Indi_Indicador).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
