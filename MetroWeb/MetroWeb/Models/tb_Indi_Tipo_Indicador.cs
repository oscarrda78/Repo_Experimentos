
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
    
public partial class tb_Indi_Tipo_Indicador
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public tb_Indi_Tipo_Indicador()
    {

        this.tb_Indi_Indicador = new HashSet<tb_Indi_Indicador>();

    }


    public long id { get; set; }

    public string descripcion { get; set; }

    public Nullable<int> id_estado { get; set; }

    public string flag_eliminado { get; set; }

    public string codigo { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<tb_Indi_Indicador> tb_Indi_Indicador { get; set; }

    public virtual tb_Orga_Estados tb_Orga_Estados { get; set; }

}

}
