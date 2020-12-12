using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GettingStartedLib.CapaEntidades;
using GettingStartedLib.CapaDatos;

namespace GettingStartedLib.CapaEntidades
{
    public class Prestamo
    {
        public int id_prestamo { get; set; }
        public Libro libro { get; set; }
        public Bibliotecario bibliotecario { get; set; }
        public Persona personaPrestatario { get; set; }
        public DateTime fechaPrestamo { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public int activo { get; set; }

        public Prestamo()
        {

        }

        public Prestamo(int id_prestamo, Libro libro, Bibliotecario bibliotecario, Persona personaPrestatario, DateTime fechaPrestamo, DateTime fechaVencimiento, int activo)
        {
            this.id_prestamo = id_prestamo;
            this.libro = libro;
            this.bibliotecario = bibliotecario;
            this.personaPrestatario = personaPrestatario;
            this.fechaPrestamo = fechaPrestamo;
            this.fechaVencimiento = fechaVencimiento;
            this.activo = activo;
        }

        public bool AltaPrestamo()
        {
            bool altaExitosa = false;
            Prestamo prestamo = this;

            altaExitosa = PrestamoDAO.GetInstance().AltaPrestamo(prestamo);

            return altaExitosa;
        }

        public void VencerPrestamo()
        {
            
        }
    }
}
