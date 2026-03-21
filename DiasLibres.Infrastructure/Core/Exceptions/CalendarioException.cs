using System;

namespace DiasLibres.Infrastructure.Core.Exceptions
{
    public class CalendarioException : Exception
    {
        public CalendarioException() : base() { }

        public CalendarioException(string message) : base(message) { }

        public CalendarioException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class CalendarioYaExisteException : CalendarioException
    {
        public CalendarioYaExisteException(int anio)
            : base($"Ya existe un calendario para el año {anio}") { }
    }
}