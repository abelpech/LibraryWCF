using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingStartedLib.CapaDatos
{
    class Conexion
    {
        #region "PATRON SINGLETON"
        private static Conexion conexion = null;
        private Conexion()
        {

        }
        public static Conexion GetInstance()
        {
            if (conexion == null)
            {
                conexion = new Conexion();
            }
            else
            {
                Debug.WriteLine("Ya existe una instancia del objeto.");
            }
            return conexion;
        }
        #endregion

        public SqlConnection ConexionBD()
        {
            SqlConnection conexionbd = new SqlConnection();
            string hostName = System.Environment.MachineName;
            //conexionbd.ConnectionString = "Data Source="+hostName+"\\SQLEXPRESS;Initial Catalog=dbBiblioteca;Integrated Security=True";
            conexionbd.ConnectionString = "Data Source=" + hostName + ";Initial Catalog=dbBiblioteca;Integrated Security=True";
            return conexionbd;
        }
    }
}
