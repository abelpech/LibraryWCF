using System;
using System.ServiceModel;
using GettingStartedLib.CapaDatos;
using GettingStartedLib.CapaEntidades;

namespace GettingStartedLib
{
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    public interface ICalculator
    {
        [OperationContract]
        Libro pedirLibro(Bibliotecario bibliotecario, Libro libro);

        [OperationContract]
        bool retornarLibro(Libro libro);


    }
}