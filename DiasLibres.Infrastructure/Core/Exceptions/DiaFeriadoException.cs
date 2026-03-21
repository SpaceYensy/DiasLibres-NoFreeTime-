using System;

namespace DiasLibres.Infrastructure.Core.Exceptions
{
    public class DiaFeriadoException : Exception
    {
        public DiaFeriadoException() : base() { }

        public DiaFeriadoException(string message) : base(message) { }

        public DiaFeriadoException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class FeriadoDuplicadoException : DiaFeriadoException
    {
        public FeriadoDuplicadoException(DateTime fecha)
            : base($"Ya existe un feriado en la fecha {fecha:yyyy-MM-dd}") { }
    }

    public class FeriadoNoEncontradoException : DiaFeriadoException
    {
        public FeriadoNoEncontradoException(int id)
            : base($"No se encontró el feriado con ID {id}") { }
    }
}
