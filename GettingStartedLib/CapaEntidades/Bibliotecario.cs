using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GettingStartedLib.CapaEntidades;
using GettingStartedLib.CapaDatos;
using System.Diagnostics;

namespace GettingStartedLib.CapaEntidades
{
    public class Bibliotecario : Persona
    {
        public Bibliotecario() : base()
        {
            
        }
        public Bibliotecario(string matricula, string nombre, string apellido, string telefono, string email, string direccion, string password) : base(matricula, nombre, apellido, telefono, email, direccion, password)
        {
            this.matricula = matricula;
            this.nombre = nombre;
            this.apellido = apellido;
            this.telefono = telefono;
            this.email = email;
            this.direccion = direccion;
            this.password = password;
        }


        public Prestamo prestarLibro(Libro libro)
        {
            Bibliotecario bibliotecario = this;
            Prestamo prestamo = new Prestamo();
            bool disponible = false;

            disponible = libro.validarDisponibilidad();
            if (disponible)
            {
                prestamo.libro = libro;
                prestamo.bibliotecario = bibliotecario;

            } else {
                prestamo = null;
            }

            return prestamo;
        }

        public void ponerMulta(Persona persona)
        {
            // TO DO
            // Conectar con sistema de cobranza
        }

        public DataTable consultaPrestamos()
        {
            DataTable dt = null;
            dt = BibliotecarioDAO.GetInstance().consultaPrestamos();

            return dt;
        }
    }
}
