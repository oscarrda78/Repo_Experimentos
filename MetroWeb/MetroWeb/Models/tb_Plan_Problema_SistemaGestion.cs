
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------


namespace MetroWeb.Models
{

using System;
    using System.Collections.Generic;
    
public partial class tb_Plan_Problema_SistemaGestion
{

    public int id_problema_sistemagestion { get; set; }

    public Nullable<int> id_problema { get; set; }

    public Nullable<int> id_sistema_gestion { get; set; }

    public string flag_eliminado { get; set; }



    public virtual tb_Orga_SistemasGestion tb_Orga_SistemasGestion { get; set; }

    public virtual tb_Plan_Problema tb_Plan_Problema { get; set; }

}

}
