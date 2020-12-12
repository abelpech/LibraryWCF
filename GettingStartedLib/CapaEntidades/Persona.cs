using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GettingStartedLib.CapaEntidades;

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

        public virtual Libro pedirLibro(Bibliotecario bibliotecario, Libro libro)
        {
            Prestamo prestamo = null;
            //bool disponible = false;

            prestamo = bibliotecario.prestarLibro(libro);
            if(prestamo != null)
            {
                prestamo.personaPrestatario = this;
                prestamo.fechaVencimiento = DateTime.Today.AddDays(7);
            }

            return libro;
        }

        public virtual bool retornarLibro(Libro libro)
        {
            bool retornado = false;
            retornado = libro.validarDisponibilidad();
            return retornado;
        }

        public virtual void consultarLibros()
        {
            
        }

        public virtual void consultarCatalogoDeLibros()
        {

        }

        public virtual void pagarMulta()
        {

        }
    }
}
