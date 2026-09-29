// AYFR

using System;
using System.Text;

namespace JuegoRuleta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.Clear();
            Console.WriteLine("Juego Ruleta");
            Console.WriteLine(new string('=', 30));
            Console.Write("Ingrese su nombre: ");
            string nombre = Console.ReadLine() ?? "Jugador";

            string titulo = "JUEGO RULETA";
            string[] opciones = {
                "Apostar a Numero",
                "Apostar a Color",
                "Apostar a Par/Impar",
                "Ver historial de giros",
                "Ver estadisticas"
            };

            Menu menu = new(titulo, opciones, nombre);
            menu.MostrarMenu();

            Console.Clear();
            Console.WriteLine("Gracias por jugar.");
        }
    }
}