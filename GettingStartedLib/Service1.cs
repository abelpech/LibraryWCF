using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.ServiceModel;
using GettingStartedLib.CapaDatos;
using GettingStartedLib.CapaEntidades;
namespace GettingStartedLib
{
    public class CalculatorService : ICalculator
    {
        SqlConnection conn;
        public CalculatorService()
        {
            string host = Dns.GetHostName();

            conn = new SqlConnection("server =  " + host + "; database=dbBiblioteca; integrated security = true");
            //conn = new SqlConnection("server =  " + host + "\\SQLEXPRESS; database=dbBiblioteca; integrated security = true");
        }

        public virtual Libro pedirLibro(Bibliotecario bibliotecario, Libro libro, Persona persona)
        {
            libro = persona.pedirLibro(bibliotecario, libro);
            return libro;
        }

        public virtual bool retornarLibro(Libro libro)
        {
            bool retornado = false;
            retornado = libro.validarDisponibilidad();
            return retornado;
        }

        public int ExecSPReturnInt(string querySP, List<string> parametros)
        {
            System.Diagnostics.Debug.WriteLine("\n----- Intentado ejecutar SP: " + querySP + " -----");
            string SPResult = "";
            bool SPResultDetected = false;
            bool intDetected = false;

            int i = 0;
            int a = 0;
            int ResultFromSP = 0;
            // Esta lista contiene los nombres de los parametros de cualquier SP que mandemos llamar.
            List<string> NombreParametros = new List<string>();

            SqlCommand cmd = new SqlCommand(querySP, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();
            // Obtiene los parametros del SP y llena la lista de cmd
            SqlCommandBuilder.DeriveParameters(cmd);

            foreach (SqlParameter p in cmd.Parameters)
            {
                // Nos saltamos el primer parametro por defecto
                if (i != 0)
                {
                    System.Diagnostics.Debug.WriteLine("Leyendo parametro: " + p.ParameterName);
                    NombreParametros.Add(p.ParameterName.ToString());
                }
                i++;
            }
            i = 0;
            cmd.Parameters.Clear();

            // Recorremos cada uno de los parametros que agregamos anteriormente para agregar los que
            // realmente necesitamos a cmd.Parameters.
            foreach (string p in NombreParametros)
            {
                if (p == "@mensaje")
                {
                    cmd.Parameters.Add(p, SqlDbType.VarChar, 150);
                    cmd.Parameters[p].Direction = ParameterDirection.Output;
                    SPResultDetected = true;
                    System.Diagnostics.Debug.WriteLine("Se agrega: " + p + ", " + "' '");
                }
                else
                {
                    // Detecta si el parametro que optuvimos puede ser considerado como int o string.
                    intDetected = int.TryParse(parametros[i], out a);
                    if (intDetected)
                    {
                        cmd.Parameters.AddWithValue(p.ToString(), a);
                        System.Diagnostics.Debug.WriteLine("Se agrega INT: " + p + ", " + parametros[i]);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(p.ToString(), parametros[i]);
                        System.Diagnostics.Debug.WriteLine("Se agrega: " + p + ", " + parametros[i]);
                    }
                }

                i++;
            }

            // Agregamos un parametro OUTPUT para devolver un entero de SP's especiales.
            // Ojo: Necesitas tener un return(@ParameterReturnINT) en tu sp con un valor INT valido.
            var ParameterReturnINT = cmd.Parameters.Add("@ParameterReturnINT", SqlDbType.Int);
            ParameterReturnINT.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteReader();
            conn.Close();

            // Guardamos el dato de los SP en una variable int.
            ResultFromSP = (int)ParameterReturnINT.Value;

            // Si detecta un mensaje lo guarda y lo despliega en consola.
            if (SPResultDetected)
            {
                SPResult = cmd.Parameters["@mensaje"].Value.ToString();
                System.Diagnostics.Debug.WriteLine("BD respondio: " + SPResult);
            }


            // Se limpian variables para uso posterior de la funcion.
            NombreParametros.Clear();
            i = 0;
            System.Diagnostics.Debug.WriteLine("----- SP: " + querySP + ", ¡Ejecutado con exito! -----");
            return ResultFromSP;
        }
    }   
}