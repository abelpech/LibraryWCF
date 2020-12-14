using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GettingStartedLib.CapaEntidades;

namespace GettingStartedLib.CapaDatos
{
    class PersonaDAO
    {
        #region "PATRON SINGLETON"
        private static PersonaDAO personaDAO = null;
        private PersonaDAO()
        {

        }
        public static PersonaDAO GetInstance()
        {
            if (personaDAO == null)
            {
                personaDAO = new PersonaDAO();
            }
            else
            {
                Debug.WriteLine("Ya existe una instancia del objeto.");
            }
            return personaDAO;
        }
        #endregion

        public DataTable consultarLibros()
        {
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            DataTable dt = null;
            SqlDataReader dr = null;
            try
            {
                conexion = Conexion.GetInstance().ConexionBD();
                cmd = new SqlCommand("spPersona_ConsultaLibros", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                dt = null;
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return dt;

        }

        public DataTable consultarCatalogoDeLibros()
        {
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            DataTable dt = null;
            SqlDataReader dr = null;
            try
            {
                conexion = Conexion.GetInstance().ConexionBD();
                cmd = new SqlCommand("spLibro_ConsultaCatalogo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                dt = null;
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return dt;

        }
    }
}
