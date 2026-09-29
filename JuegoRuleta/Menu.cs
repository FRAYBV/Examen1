// AYFR

using System;
using System.Linq;
using JuegoRuleta.Entidades;
using JuegoRuleta.Enums;
using JuegoRuleta.Util;

namespace JuegoRuleta
{
    internal class Menu
    {
        private readonly string Titulo;
        private readonly string[] Opciones;
        private Jugador jugador;

        public Menu(string titulo, string[] opciones, string nombreJugador)
        {
            Titulo = titulo;
            Opciones = opciones;
            jugador = new Jugador { Nombre = nombreJugador, Saldo = 300.00m };
        }

        public void MostrarMenu()
        {
            bool continuar = true;
            while (continuar)
            {
                Console.Clear();
                Console.WriteLine(Titulo);
                Console.WriteLine(new string('=', Titulo.Length));
                Console.WriteLine($"Jugador: {jugador.Nombre}");
                Console.WriteLine($"Saldo Actual: ${jugador.Saldo:F2}");
                Console.WriteLine(new string('-', Titulo.Length));

                for (int i = 0; i < Opciones.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {Opciones[i]}");
                }
                Console.WriteLine("0. Salir");
                Console.Write("\nSeleccione una opcion: ");

                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "0":
                        continuar = false;
                        break;
                    case "1":
                        MostrarApostarNumero();
                        break;
                    case "2":
                        MostrarApostarColor();
                        break;
                    case "3":
                        MostrarApostarParImpar();
                        break;
                    case "4":
                        MostrarHistorialGiros();
                        break;
                    case "5":
                        MostrarEstadisticas();
                        break;
                    default:
                        Console.WriteLine("\nOpcion Invalida");
                        Console.WriteLine("Presione cualquier tecla para continuar.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void MostrarApostarNumero()
        {
            Console.Clear();
            Console.WriteLine("Apostar a Numero Especifico");
            Console.WriteLine(new string('=', 40));

            if (jugador.Saldo < 10)
            {
                Console.WriteLine("\nSaldo insuficiente (Minimo $10).");
                Console.WriteLine("Presione cualquier tecla pra continuar.");
                Console.ReadKey();
                return;
            }

            try
            {
                Console.Write("\nIngrese el numero (0-36): ");
                int numero = int.Parse(Console.ReadLine() ?? "-1");

                if (numero < 0 || numero > 36)
                {
                    Console.WriteLine("\nNumero invalido (Entre 0 y 36).");
                    Console.WriteLine("Presione cualquier tecla para continuar.");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Ingrese su apuesta (multiplo de 10): ");
                decimal monto = decimal.Parse(Console.ReadLine() ?? "0");

                var apuesta = new Apuesta(monto, ModalidadRuleta.NUMERO, numero: numero);
                jugador.Saldo -= apuesta.Monto;

                int numeroGanador = ReglasJuego.GenerarNumero();
                var resultado = new Resultado
                {
                    Numero = numeroGanador,
                    Color = ReglasJuego.ObtenerColor(numeroGanador),
                    EsPar = ReglasJuego.EsPar(numeroGanador)
                };

                bool gano = ReglasJuego.VerificarGanador(apuesta, resultado);
                decimal ganancia = 0;

                if (gano)
                {
                    ganancia = monto * ReglasJuego.ObtenerMultiplicador(ModalidadRuleta.NUMERO);
                    jugador.Saldo += ganancia;
                }

                var giro = new Giro
                {
                    ModalidadElegida = ModalidadRuleta.NUMERO,
                    ResultadoElegido = resultado,
                    Gano = gano
                };
                jugador.Giros.Add(giro);

                Console.WriteLine($"\nNumero ganador: {numeroGanador} ({resultado.Color})");
                Console.WriteLine($"Su apuesta: {numero}");

                if (gano)
                {
                    Console.WriteLine($"\nGANASTE: ${ganancia:F2}");
                }
                else
                {
                    Console.WriteLine($"\nPerdio: ${monto:F2}");
                }

                Console.WriteLine($"Saldo actual: ${jugador.Saldo:F2}");
            }
            catch (FormatException)
            {
                Console.WriteLine("\nError: Ingrese un valor valido.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar.");
            Console.ReadKey();
        }

        private void MostrarApostarColor()
        {
            Console.Clear();
            Console.WriteLine("Apostar a Color");
            Console.WriteLine(new string('=', 40));

            if (jugador.Saldo < 10)
            {
                Console.WriteLine("\nSaldo insuficiente (Minimo $10).");
                Console.WriteLine("Presione cualquier tecla para continuar.");
                Console.ReadKey();
                return;
            }

            try
            {
                Console.WriteLine("\nColores disponibles:");
                Console.WriteLine("1. ROJO");
                Console.WriteLine("2. NEGRO");
                Console.Write("\nSeleccione color: ");
                string opcionColor = Console.ReadLine() ?? "0";

                ColoresRuleta colorElegido = opcionColor switch
                {
                    "1" => ColoresRuleta.ROJO,
                    "2" => ColoresRuleta.NEGRO,
                    _ => throw new ArgumentException("Color invalido")
                };

                Console.Write("Ingrese su apuesta (multiplo de 10): ");
                decimal monto = decimal.Parse(Console.ReadLine() ?? "0");

                var apuesta = new Apuesta(monto, ModalidadRuleta.COLOR, color: colorElegido);
                jugador.Saldo -= apuesta.Monto;

                int numeroGanador = ReglasJuego.GenerarNumero();
                var resultado = new Resultado
                {
                    Numero = numeroGanador,
                    Color = ReglasJuego.ObtenerColor(numeroGanador),
                    EsPar = ReglasJuego.EsPar(numeroGanador)
                };

                bool gano = ReglasJuego.VerificarGanador(apuesta, resultado);
                decimal ganancia = 0;

                if (gano)
                {
                    ganancia = monto * ReglasJuego.ObtenerMultiplicador(ModalidadRuleta.COLOR);
                    jugador.Saldo += ganancia;
                }

                var giro = new Giro
                {
                    ModalidadElegida = ModalidadRuleta.COLOR,
                    ResultadoElegido = resultado,
                    Gano = gano
                };
                jugador.Giros.Add(giro);

                Console.WriteLine($"\nNumero ganador: {numeroGanador} ({resultado.Color})");
                Console.WriteLine($"Su apuesta: {colorElegido}");

                if (gano)
                {
                    Console.WriteLine($"\nGANASTE: ${ganancia:F2}");
                }
                else
                {
                    Console.WriteLine($"\nPerdio: ${monto:F2}");
                }

                Console.WriteLine($"Saldo actual: ${jugador.Saldo:F2}");
            }
            catch (FormatException)
            {
                Console.WriteLine("\nError: Ingrese un valor valido.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar.");
            Console.ReadKey();
        }

        private void MostrarApostarParImpar()
        {
            Console.Clear();
            Console.WriteLine("Apostar a Par/Impar");
            Console.WriteLine(new string('=', 40));

            if (jugador.Saldo < 10)
            {
                Console.WriteLine("\nSaldo insuficiente (Minimo $10).");
                Console.WriteLine("Presione cualquier tecla para continuar.");
                Console.ReadKey();
                return;
            }

            try
            {
                Console.WriteLine("\nOpciones:");
                Console.WriteLine("1. PAR");
                Console.WriteLine("2. IMPAR");
                Console.Write("\nSeleccione (1-2): ");
                string opcion = Console.ReadLine() ?? "0";

                bool esParApostado = opcion switch
                {
                    "1" => true,
                    "2" => false,
                    _ => throw new ArgumentException("Opcion invalida")
                };

                Console.Write("Ingrese su apuesta (multiplo de 10): ");
                decimal monto = decimal.Parse(Console.ReadLine() ?? "0");

                var apuesta = new Apuesta(monto, ModalidadRuleta.PARIMPAR, esPar: esParApostado);
                jugador.Saldo -= apuesta.Monto;

                int numeroGanador = ReglasJuego.GenerarNumero();
                var resultado = new Resultado
                {
                    Numero = numeroGanador,
                    Color = ReglasJuego.ObtenerColor(numeroGanador),
                    EsPar = ReglasJuego.EsPar(numeroGanador)
                };

                bool gano = ReglasJuego.VerificarGanador(apuesta, resultado);
                decimal ganancia = 0;

                if (gano)
                {
                    ganancia = monto * ReglasJuego.ObtenerMultiplicador(ModalidadRuleta.PARIMPAR);
                    jugador.Saldo += ganancia;
                }

                var giro = new Giro
                {
                    ModalidadElegida = ModalidadRuleta.PARIMPAR,
                    ResultadoElegido = resultado,
                    Gano = gano
                };
                jugador.Giros.Add(giro);

                string tipoGanador = resultado.EsPar == true ? "PAR" :
                                    resultado.EsPar == false ? "IMPAR" : "CERO";

                Console.WriteLine($"\nNumero ganador: {numeroGanador} ({tipoGanador})");
                Console.WriteLine($"Su apuesta: {(esParApostado ? "PAR" : "IMPAR")}");

                if (gano)
                {
                    Console.WriteLine($"\nGANASTE: ${ganancia:F2}");
                }
                else
                {
                    Console.WriteLine($"\nPerdio: ${monto:F2}");
                }

                Console.WriteLine($"Saldo actual: ${jugador.Saldo:F2}");
            }
            catch (FormatException)
            {
                Console.WriteLine("\nError: Ingrese un valor valido.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar.");
            Console.ReadKey();
        }

        private void MostrarHistorialGiros()
        {
            Console.Clear();
            Console.WriteLine("Historial de Giros");
            Console.WriteLine(new string('=', 40));

            if (jugador.Giros.Count == 0)
            {
                Console.WriteLine("\nNo hay giros registrados.");
            }
            else
            {
                foreach (var giro in jugador.Giros)
                {
                    Console.WriteLine(giro.ToString());
                    Console.WriteLine(new string('-', 40));
                }
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar.");
            Console.ReadKey();
        }

        private void MostrarEstadisticas()
        {
            Console.Clear();
            Console.WriteLine("Estadisticas del Juego");
            Console.WriteLine(new string('=', 40));

            int totalGiros = jugador.Giros.Count;
            int girosGanados = jugador.Giros.Count(g => g.Gano == true);
            int girosPerdidos = jugador.Giros.Count(g => g.Gano == false);
            decimal saldoInicial = 300.00m;

            Console.WriteLine($"\nJugador: {jugador.Nombre}");
            Console.WriteLine($"Total de giros: {totalGiros}");
            Console.WriteLine($"Giros ganados: {girosGanados}");
            Console.WriteLine($"Giros perdidos: {girosPerdidos}");
            Console.WriteLine($"Saldo actual: ${jugador.Saldo:F2}");
            Console.WriteLine($"Saldo inicial: ${saldoInicial:F2}");
            Console.WriteLine($"Balance: ${jugador.Saldo - saldoInicial:F2}");

            Console.WriteLine("\nPresione cualquier tecla para continuar.");
            Console.ReadKey();
        }
    }
}