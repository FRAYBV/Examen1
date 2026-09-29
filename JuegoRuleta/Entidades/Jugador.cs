// AYFR

using System.Dynamic;
using JuegoRuleta.Enums;

namespace JuegoRuleta.Entidades
{
    class Jugador
    {
        public List<Giro> Giros { get; set; } = [];
        public required string Nombre { get; set; }
        public decimal Saldo { get; set; } = 300.00m;

    }
}