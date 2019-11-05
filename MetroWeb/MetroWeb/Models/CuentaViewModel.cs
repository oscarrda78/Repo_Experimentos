using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MetroWeb.Models
{
    public class LoginViewModel
    {
        #region Variables
        [Required]
        [Display(Name = "Usuario")]
        [EmailAddress]
        public string Usuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Contrasenia { get; set; }

        [Display(Name = "¿Recordar cuenta?")]
        public bool Recordar { get; set; }
        #endregion
        #region Metodos
        public bool CuentaValida(BDGestionIntegradaEntitiesContext db)
        {
            if (db.tb_Orga_Usuarios.Where(
                u => u.Usuario.ToUpper() == this.Usuario &&
                u.Contrasenia == this.Contrasenia).Count() > 0)
            { 
                
                return true;
            }
            return false;
        }
        #endregion
    }
}