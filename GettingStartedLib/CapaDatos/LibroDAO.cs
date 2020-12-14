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
    class LibroDAO
    {
        #region "PATRON SINGLETON"
        private static LibroDAO libroDAO = null;
        private LibroDAO()
        {

        }
        public static LibroDAO GetInstance()
        {
            if (libroDAO == null)
            {
                libroDAO = new LibroDAO();
            }
            else
            {
                Debug.WriteLine("Ya existe una instancia del objeto.");
            }
            return libroDAO;
        }
        #endregion

        public bool ValidarDisponibilidad(Libro libro)
        {
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            bool disponible = false;
            int numCopias = 0;
            try
            {
                conexion = Conexion.GetInstance().ConexionBD();
                cmd = new SqlCommand("spLibro_ValidarDisponibilidad", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmCodigoLibro", libro.codigo);
                var ReturnValue = cmd.Parameters.Add("@numCopias", SqlDbType.Int);
                ReturnValue.Direction = ParameterDirection.ReturnValue;

                conexion.Open();
                Debug.WriteLine("LIBRO: " + libro.codigo);
                cmd.ExecuteReader();
                numCopias = (int)ReturnValue.Value;
                Debug.WriteLine("LIBRO: numcopias " + numCopias);
                if (numCopias > 0)
                {
                    disponible = true;
                }
            }
            catch (Exception ex)
            {
                disponible = false;
                libro = null;
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return disponible;

        }
    }
}
