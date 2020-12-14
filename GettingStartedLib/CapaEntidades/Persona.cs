using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GettingStartedLib.CapaEntidades;
using GettingStartedLib.CapaDatos;

namespace GettingStartedLib.CapaEntidades
{
    public class Persona
    {
        public String matricula { get; set; }
        public String nombre { get; set; }
        public String apellido { get; set; }
        public String telefono { get; set; }
        public String email { get; set; }
        public String direccion { get; set; }
        public String password { get; set; }

        public Persona()
        {

        }
        public Persona(string matricula, string nombre, string apellido, string telefono, string email, string direccion, string password)
        {
            this.matricula = matricula;
            this.nombre = nombre;
            this.apellido = apellido;
            this.telefono = telefono;
            this.email = email;
            this.direccion = direccion;
            this.password = password;
        }

        public virtual Prestamo pedirLibro(Bibliotecario bibliotecario, Libro libro)
        {
            Prestamo prestamo = null;

            prestamo = bibliotecario.prestarLibro(libro);
            if(prestamo != null)
            {
                prestamo.personaPrestatario = this;
                prestamo.fechaVencimiento = DateTime.Today.AddDays(7);
                PrestamoDAO.GetInstance().AltaPrestamo(prestamo);
            }

            return prestamo;
        }

        public virtual bool retornarLibro(Libro libro)
        {
            bool retornado = false;
            retornado = PersonaDAO.GetInstance().retornarLibro(libro, this);
            return retornado;
        }

        public virtual DataTable consultarLibros()
        {
            DataTable dt = null;

            dt = PersonaDAO.GetInstance().consultarLibros();
            return dt;
        }

        public virtual DataTable consultarCatalogoDeLibros()
        {
            DataTable dt = null;

            dt = PersonaDAO.GetInstance().consultarCatalogoDeLibros();
            return dt;
        }

        public virtual void pagarMulta()
        {
            //Pertenece a otro modulo.
        }

        public static implicit operator Persona(CalculatorService v)
        {
            throw new NotImplementedException();
        }
    }
}
