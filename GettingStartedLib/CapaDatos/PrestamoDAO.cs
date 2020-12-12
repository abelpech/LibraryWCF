using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GettingStartedLib.CapaDatos;
using GettingStartedLib.CapaEntidades;

namespace GettingStartedLib.CapaDatos
{
    class PrestamoDAO
    {
        #region "PATRON SINGLETON"
        private static PrestamoDAO prestamoDAO = null;
        private PrestamoDAO()
        {

        }
        public static PrestamoDAO GetInstance()
        {
            if (prestamoDAO == null)
            {
                prestamoDAO = new PrestamoDAO();
            }
            else
            {
                Debug.WriteLine("Ya existe una instancia del objeto.");
            }
            return prestamoDAO;
        }
        #endregion

        public bool AltaPrestamo(Prestamo prestamo)
        {
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            int result = 0;
            bool altaExitosa = false;
            try
            {
                conexion = Conexion.GetInstance().ConexionBD();
                cmd = new SqlCommand("spValidarProfesor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmMatriculaBibliotecario", prestamo.bibliotecario.matricula);
                cmd.Parameters.AddWithValue("@prmMatriculaPrestatario", prestamo.personaPrestatario.matricula);
                cmd.Parameters.AddWithValue("@prmCodigoLibro", prestamo.libro.codigo);
                cmd.Parameters.AddWithValue("@prmFechaVencimiento", prestamo.fechaVencimiento);
                conexion.Open();
                result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    Debug.WriteLine("Prestamo creado de manera exitosa.");
                    altaExitosa = true;
                }
            }
            catch (Exception ex)
            {
                prestamo = null;
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return altaExitosa;
        }
    }
}
