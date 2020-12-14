using System;
using System.Data;
using System.ServiceModel;
using GettingStartedLib.CapaDatos;
using GettingStartedLib.CapaEntidades;

namespace GettingStartedLib
{
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    public interface ICalculator
    {
        [OperationContract]
        Prestamo pedirLibro(Bibliotecario bibliotecario, Libro libro, Persona persona);

        [OperationContract]
        bool retornarLibro(Libro libro, Persona persona);
        
        [OperationContract]
        DataTable consultarLibros(Persona persona);

        [OperationContract]
        DataTable consultarCatalogoDeLibros(Persona persona);

    }
}