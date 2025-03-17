namespace TrabajoFinal.Servicios.Excepciones
{
    public class CantidadCartasMazoInvalidaException : Exception
    {
        public int CantidadCartas { get; }

        public CantidadCartasMazoInvalidaException(int cantidadCartas)
            : base($"Un mazo debe contener exactamente 15 cartas, pero tiene {cantidadCartas}.")
        {
            CantidadCartas = cantidadCartas;
        }
    }

}
