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
    class ProfesorDAO
    {
        #region "PATRON SINGLETON"
        private static ProfesorDAO profesorDAO = null;
        private ProfesorDAO()
        {

        }
        public static ProfesorDAO GetInstance()
        {
            if (profesorDAO == null)
            {
                profesorDAO = new ProfesorDAO();
            }
            else
            {
                Debug.WriteLine("Ya existe una instancia del objeto.");
            }
            return profesorDAO;
        }
        #endregion
        //Va a retornar un objeto del tipo profesor

        public Profesor AccesoSistema(String matricula, String password)
        {
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            Profesor objProfesor = null;
            SqlDataReader dr = null;
            try
            {
                conexion = Conexion.GetInstance().ConexionBD();
                cmd = new SqlCommand("spAccesoSistema", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmMatricula", matricula);
                cmd.Parameters.AddWithValue("@prmPassword", password);
                cmd.Parameters.AddWithValue("@prmTipo", 2);
                conexion.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    objProfesor = new Profesor();
                    objProfesor.matricula = dr["matricula"].ToString();
                    objProfesor.nombre = dr["usuario"].ToString();
                    objProfesor.apellido = dr["apellido"].ToString();
                    objProfesor.telefono = dr["telefono"].ToString();
                    objProfesor.email = dr["email"].ToString();
                    objProfesor.direccion = dr["direccion"].ToString();
                    objProfesor.password = dr["password"].ToString();
                }
            }
            catch (Exception ex)
            {
                objProfesor = null;
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return objProfesor;

        }
    }
}
