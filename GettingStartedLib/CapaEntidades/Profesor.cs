using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GettingStartedLib.CapaEntidades;

namespace GettingStartedLib.CapaEntidades
{
    public class Profesor : Persona
    {
        public Profesor() : base()
        {
            
        }
        public Profesor(string matricula, string nombre, string apellido, string telefono, string email, string direccion, string password) : base(matricula, nombre, apellido, telefono, email, direccion, password)
        {
            this.matricula = matricula;
            this.nombre = nombre;
            this.apellido = apellido;
            this.telefono = telefono;
            this.email = email;
            this.direccion = direccion;
            this.password = password;
        }

    }
}
