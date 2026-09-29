// AYFR

using JuegoRuleta.Enums;

namespace JuegoRuleta.Entidades
{
    public class Giro
    {
        private static int id = 0;

        public int NumGiro { get; private set; }
        public DateTime FechaHora { get; private set; }
        public ModalidadRuleta ModalidadElegida { get; set; }
        public Resultado ResultadoElegido { get; set; } = null!;
        public bool? Gano { get; set; } = null;

        public Giro()
        {
            id++;
            NumGiro = id;
            FechaHora = DateTime.UtcNow;
        }

        public override string ToString()
        {
            string fechaFormato = FechaHora.ToString("dd/MM/yyyy HH:mm");
            string modalidadFormato = ModalidadElegida.ToString().ToLower();
            return $"No.Giro: #{NumGiro} | Fecha : {fechaFormato}\n" +
                   $"Modalidad: {modalidadFormato}\n" +
                   $"Resultado: {ResultadoElegido.GetString((int)ModalidadElegida)}\n" +
                   $"{(Gano == true ? "Ganaste" : "Perdiste")}!\n";
        }

        public static void ReiniciarContador()

        {
            id = 0;
        }
    }
}