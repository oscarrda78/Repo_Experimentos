using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroWeb.Models
{
    public static class VariablesGlobales
    {
        #region Variables
        #region Aplicacion
        private static string companyName = "MetroWeb";
        private static string aplicationName = "MetroWeb";
        #endregion
        #region ColoresWeb
        private static string colorPrimary = "1089ff";
        private static string darkColorPrimary = "23374d";
        private static string windowColor = "e5e5e5";
        private static string textColorPrimary = "3c4245";
        private static string textColorSecondary = "eeeeee";
        private static string colorSecondary = "5f6769";

        #endregion
        #region Rutas
        private static string loggedInLayoutPath = "~/Views/Shared/_LayoutLoggedIn.cshtml";
        private static string emptyLayoutPath = "~/Views/Shared/_LayoutEmpty.cshtml";
        #endregion
        #endregion

        #region Metodos
        #region Aplicacion
        public static string AplicationName { get { return aplicationName; } set { aplicationName = value; } }
        public static string CompanyName { get { return companyName; } set { companyName = value; } }
        #endregion
        #region ColoresWeb
        public static string ColorPrimary { get { return colorPrimary; } set { colorPrimary=value; } }
        public static string DarkColorPrimary { get { return darkColorPrimary; } set { darkColorPrimary = value; } }
        public static string WindowColor { get { return windowColor; } set { windowColor = value; } }
        public static string TextColorPrimary { get { return textColorPrimary; } set { textColorPrimary = value; } }
        public static string TextColorSecondary { get { return textColorSecondary; } set { textColorSecondary = value; } }
        public static string ColorSecondary { get { return colorSecondary; } set { colorSecondary = value; } }
        #endregion
        #region Rutas
        public static string LoggedInLayoutPath { get { return loggedInLayoutPath; } set { loggedInLayoutPath = value; } }
        public static string EmptyLayoutPath { get { return emptyLayoutPath; } set { emptyLayoutPath = value; } }
        #endregion

        #endregion
    }
}