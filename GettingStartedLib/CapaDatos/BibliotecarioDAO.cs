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
    class BibliotecarioDAO
    {
        #region "PATRON SINGLETON"
        private static BibliotecarioDAO bibliotecarioDAO = null;
        private BibliotecarioDAO()
        {

        }
        public static BibliotecarioDAO GetInstance()
        {
            if (bibliotecarioDAO == null)
            {
                bibliotecarioDAO = new BibliotecarioDAO();
            }
            else
            {
                Debug.WriteLine("Ya existe una instancia del objeto.");
            }
            return bibliotecarioDAO;
        }
        #endregion

        public Bibliotecario AccesoSistema(String matricula, String password)
        {
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            Bibliotecario objBibliotecario = null;
            SqlDataReader dr = null;
            try
            {
                conexion = Conexion.GetInstance().ConexionBD();
                cmd = new SqlCommand("spValidarProfesor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmMatricula", matricula);
                cmd.Parameters.AddWithValue("@prmPassword", password);
                cmd.Parameters.AddWithValue("@prmTipo", 1);
                conexion.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    objBibliotecario = new Bibliotecario();
                    objBibliotecario.matricula = dr["matricula"].ToString();
                    objBibliotecario.nombre = dr["usuario"].ToString();
                    objBibliotecario.apellido = dr["apellido"].ToString();
                    objBibliotecario.telefono = dr["telefono"].ToString();
                    objBibliotecario.email = dr["email"].ToString();
                    objBibliotecario.direccion = dr["direccion"].ToString();
                    objBibliotecario.password = dr["password"].ToString();
                }
            }
            catch (Exception ex)
            {
                objBibliotecario = null;
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return objBibliotecario;

        }

        public Persona PrestarLibro(Persona persona)
        {
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            Bibliotecario objBibliotecario = null;
            SqlDataReader dr = null;
            try
            {
                conexion = Conexion.GetInstance().ConexionBD();
                cmd = new SqlCommand("spValidarProfesor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmMatricula", objBibliotecario.matricula);
                cmd.Parameters.AddWithValue("@prmPassword", objBibliotecario.password);
                cmd.Parameters.AddWithValue("@prmTipo", 1);
                conexion.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    objBibliotecario = new Bibliotecario();
                    objBibliotecario.matricula = dr["matricula"].ToString();
                    objBibliotecario.nombre = dr["usuario"].ToString();
                    objBibliotecario.apellido = dr["apellido"].ToString();
                    objBibliotecario.telefono = dr["telefono"].ToString();
                    objBibliotecario.email = dr["email"].ToString();
                    objBibliotecario.direccion = dr["direccion"].ToString();
                    objBibliotecario.password = dr["password"].ToString();
                }
            }
            catch (Exception ex)
            {
                objBibliotecario = null;
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return persona;
        }

        public DataTable consultaPrestamos()
        {
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            DataTable dt = null;
            SqlDataReader dr = null;
            try
            {
                conexion = Conexion.GetInstance().ConexionBD();
                cmd = new SqlCommand("spBibliotecario_ConsultaPrestamos", conexion);
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
