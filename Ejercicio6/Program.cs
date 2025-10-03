using System;
using System.Collections.Generic;
using System.Linq;
/*Enunciado:
Un colegio realiza una elección estudiantil con 5 candidatos. Se registran 100 votos numéricos (entre 1 y 5).
Problema a resolver:
1.	Generar un arreglo con 100 votos (puede ser ingresado o aleatorio).
2.	Contar cuántos votos obtuvo cada candidato.
3.	Determinar:
•	Candidato ganador.
•	Si hay empate, mostrar quiénes empataron.
4.	Calcular porcentaje de votos de cada candidato.
5.	Validar que todos los votos sean válidos (entre 1 y 5), descartando inválidos si se da el caso.
*/

class SimuladorVotaciones
{
    static void Main()
    {
        while (!MostrarMenuInicio()) { } // Repite hasta que el usuario seleccione una opción válida

        do
        {
            EjecutarPrograma();
        }
        while (DeseaContinuar());
    }

    static void EjecutarPrograma()
    {
        Console.WriteLine("=== Simulador de Votaciones ===");
        Console.WriteLine("1. Ingresar votos manualmente");
        Console.WriteLine("2. Generar votos aleatoriamente");
        Console.Write("Selecciona el modo de entrada (1 o 2): ");
        string modo = Console.ReadLine()?.Trim();

        const int totalVotos = 100;
        int[] votos = new int[totalVotos];

        if (modo == "1")
        {
            for (int i = 0; i < totalVotos; i++)
            {
                while (true)
                {
                    Console.Write($"Ingrese el voto #{i + 1} (1 a 5): ");
                    string entrada = Console.ReadLine();

                    if (int.TryParse(entrada, out int voto) && voto >= 1 && voto <= 5)
                    {
                        votos[i] = voto;
                        break;
                    }

                    else
                    {
                        MostrarMensaje("Voto inválido. Debe ser un número entre 1 y 5, intente nuevamente el mismo voto.", ConsoleColor.Red);
                        
                    }
                }
            }
        }
        else if (modo == "2")
        {
            Random rnd = new Random();
            for (int i = 0; i < totalVotos; i++)
            {
                votos[i] = rnd.Next(1, 6);
            }
            MostrarMensaje("Votos generados aleatoriamente.", ConsoleColor.Green);
        }


        int[] conteo = new int[5];
        int votosValidos = 0;

        foreach (int voto in votos)
        {
            if (voto >= 1 && voto <= 5)
            {
                conteo[voto - 1]++;
                votosValidos++;
            }
        }

        Console.WriteLine("\n=== Resultados de la Votación ===");
        for (int i = 0; i < conteo.Length; i++)
        {
            double porcentaje = (double)conteo[i] / votosValidos * 100;
            Console.WriteLine($"Candidato {i + 1}: {conteo[i]} votos ({porcentaje:F2}%)");
        }

        int maxVotos = conteo.Max();
        List<int> ganadores = new List<int>();

        for (int i = 0; i < conteo.Length; i++)
        {
            if (conteo[i] == maxVotos)
                ganadores.Add(i + 1);
        }

        if (ganadores.Count == 1)
        {
            MostrarMensaje($"El ganador es el Candidato {ganadores[0]} con {maxVotos} votos.", ConsoleColor.Green);
        }
        else
        {
            MostrarMensaje("Empate entre los siguientes candidatos:", ConsoleColor.Yellow);
            foreach (int g in ganadores)
            {
                Console.WriteLine($"Candidato {g} con {maxVotos} votos");
            }
        }
    }

    static bool MostrarMenuInicio()
    {
        Console.Clear();
        Console.WriteLine("=== MENÚ PRINCIPAL ===");
        Console.WriteLine("1. Iniciar simulador");
        Console.WriteLine("2. Salir");
        Console.Write("Selecciona una opción (1 o 2): ");
        string opcion = Console.ReadLine()?.Trim();

        if (opcion == "1")
        {
            Console.Clear();
            return true;
        }
        else if (opcion == "2")
        {
            MostrarMensaje("Programa cerrado por decisión del usuario.", ConsoleColor.Yellow);
            Environment.Exit(0);
            return false;
        }
        else
        {
            MostrarMensaje("Entrada no válida. Por favor selecciona 1 o 2.", ConsoleColor.Red);
            return false;
        }
    }

    static bool DeseaContinuar()
    {
        while (true)
        {
            Console.WriteLine("\n¿Deseas realizar otra simulación?");
            Console.WriteLine("1. Sí, limpiar consola y repetir");
            Console.WriteLine("2. No, salir del programa");
            Console.Write("Selecciona una opción (1 o 2): ");
            string opcion = Console.ReadLine()?.Trim();

            if (opcion == "1")
            {
                Console.Clear();
                return true;
            }
            else if (opcion == "2")
            {
                MostrarMensaje("Programa finalizado. ¡Hasta pronto!", ConsoleColor.Yellow);
                return false;
            }
            else
            {
                MostrarMensaje("Entrada no válida. Ingresa 1 o 2.", ConsoleColor.Red);
            }
        }
    }

    static void MostrarMensaje(string mensaje, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(mensaje);
        Console.ResetColor();
    }
}
