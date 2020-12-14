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
            DataTable dt = new DataTable();
            try
            {
                conexion = Conexion.GetInstance().ConexionBD();
                cmd = new SqlCommand("spLibro_ConsultaCatalogo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                dt.Load(cmd.ExecuteReader());
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

        public bool retornarLibro(Libro libro, Persona persona)
        {
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            int result = 0;
            bool retornado = false;
            try
            {
                conexion = Conexion.GetInstance().ConexionBD();
                cmd = new SqlCommand("spPrestamo_VencerPrestamo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmCodigoLibro", libro.codigo);
                cmd.Parameters.AddWithValue("@prmMatricula", persona.matricula);
                conexion.Open();
                result = (Int32)cmd.ExecuteNonQuery();
                Debug.WriteLine("Query executed!!: " + Convert.ToString(result));
                if(result > 0)
                {
                    retornado = true;
                }
            }
            catch (Exception ex)
            {
                result = 0;
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return retornado;

        }
    }
}
