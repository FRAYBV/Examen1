// AYFR

using JuegoRuleta.Enums;

namespace JuegoRuleta.Entidades
{
    public class Apuesta
    {
        public decimal Monto { get; private set; }
        public ModalidadRuleta Modalidad { get; private set; }
        public int? NumeroEspecifico { get; private set; }
        public ColoresRuleta? ColorElegido { get; private set; }
        public bool? EsParElegido { get; private set; }

        public Apuesta(decimal monto, ModalidadRuleta modalidad, int? numero = null, ColoresRuleta? color = null, bool? esPar = null)
        {
            if (monto <= 0 || (int)monto % 10 != 0)
                throw new ArgumentException("El monto debe ser > a 0 y múltiplo de 10.");

            Monto = monto;
            Modalidad = modalidad;
            NumeroEspecifico = numero;
            ColorElegido = color;
            EsParElegido = esPar;
        }
    }
}