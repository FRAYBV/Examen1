// AYFR

using JuegoRuleta.DatosRuleta;
using JuegoRuleta.Entidades;
using JuegoRuleta.Enums;

namespace JuegoRuleta.Util
{
    public static class ReglasJuego
    {
        private static readonly Random _random = new Random();

        public static int GenerarNumero()
        {
            return _random.Next(0, 37);
        }

        public static ColoresRuleta ObtenerColor(int numero)
        {
            return NumerosRuleta.Casillas[numero];
        }

        public static bool? EsPar(int numero)
        {
            if (numero == 0) return null;
            return numero % 2 == 0;
        }

        public static bool VerificarGanador(Apuesta apuesta, Resultado resultado)
        {
            return apuesta.Modalidad switch
            {
                ModalidadRuleta.NUMERO => apuesta.NumeroEspecifico == resultado.Numero,
                ModalidadRuleta.COLOR => apuesta.ColorElegido == resultado.Color,
                ModalidadRuleta.PARIMPAR => apuesta.EsParElegido == resultado.EsPar,
                _ => false
            };
        }

        public static decimal ObtenerMultiplicador(ModalidadRuleta modalidad)
        {
            return modalidad switch
            {
                ModalidadRuleta.NUMERO => 10m,
                ModalidadRuleta.COLOR => 5m,
                ModalidadRuleta.PARIMPAR => 2m,
                _ => 0m
            };
        }
    }
}