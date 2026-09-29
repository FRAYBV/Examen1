// AYFR

using JuegoRuleta.Enums;

namespace JuegoRuleta.Entidades
{
    public class Resultado
    {
        public int Numero { get; set; }
        public ColoresRuleta Color { get; set; }
        public bool? EsPar { get; set; } = null;


        public string GetString(int tipo)
        {
            return tipo switch
            {
                0 => Numero.ToString(),
                1 => Color.ToString(),
                2 => EsPar == true ? $"{Numero}: Par" : $"{Numero}: Impar",
                _ => "Tipo no Valido",
            };
        }
    }
}