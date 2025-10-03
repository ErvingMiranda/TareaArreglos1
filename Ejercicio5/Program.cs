using System;
/*Enunciado:
Una clínica asigna 20 turnos diarios. Los pacientes se registran con su edad. El objetivo es analizar la distribución de edades.
Problema a resolver:
1.	Ingresar las edades de 20 pacientes.
2.	Clasificar en grupos:
•	Niños (0–12)
•	Jóvenes (13–25)
•	Adultos (26–60)
•	Mayores (>60)
3.	Mostrar cuántos hay en cada grupo.
4.	Detectar si hay más de 5 personas mayores (alerta por alto riesgo).
5.	Calcular la edad promedio general y por grupo.
*/

class GestionTurnosClinica
{
    static void Main()
    {
        // Menú de inicio
        if (!MostrarMenuInicio())
        {
            MostrarMensaje("Programa cerrado por decisión del usuario.", ConsoleColor.Yellow);
            return;
        }

        // Ciclo principal del programa
        do
        {
            EjecutarPrograma();
        }
        while (DeseaContinuar());
    }

    /// <summary>
    /// Función principal que gestiona el registro y análisis de edades de pacientes.
    /// </summary>
    static void EjecutarPrograma()
    {
        Console.WriteLine("=== Gestión de Turnos en Clínica ===");

        const int totalTurnos = 20;
        int[] edades = new int[totalTurnos];

        // Contadores por grupo
        int niños = 0, jovenes = 0, adultos = 0, adultosMayores = 0;

        // Sumas por grupo para calcular promedios
        int sumaNiños = 0, sumaJovenes = 0, sumaAdultos = 0, sumaAdultosMayores = 0;

        // Ciclo para ingresar las edades de los pacientes
        for (int i = 0; i < totalTurnos; i++)
        {
            while (true)
            {
                Console.Write($"Ingrese la edad del paciente #{i + 1}: ");
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out int edad) && edad >= 0 && edad <= 120)
                {
                    edades[i] = edad;

                    // Clasificación por grupo y acumulación
                    if (edad <= 12)
                    {
                        niños++;
                        sumaNiños += edad;
                    }
                    else if (edad <= 25)
                    {
                        jovenes++;
                        sumaJovenes += edad;
                    }
                    else if (edad <= 60)
                    {
                        adultos++;
                        sumaAdultos += edad;
                    }
                    else
                    {
                        adultosMayores++;
                        sumaAdultosMayores += edad;
                    }

                    MostrarMensaje("Edad registrada correctamente.", ConsoleColor.Green);
                    break;
                }
                else
                {
                    MostrarMensaje("Edad inválida. Ingrese un número entre 0 y 120.", ConsoleColor.Red);
                }
            }
        }

        // Mostrar resultados
        Console.WriteLine("\n=== Distribución de Edades ===");
        Console.WriteLine($"Niños (0-12): {niños}");
        Console.WriteLine($"Jóvenes (13-25): {jovenes}");
        Console.WriteLine($"Adultos (26-60): {adultos}");
        Console.WriteLine($"Adultos mayores (>60): {adultosMayores}");

        // Alerta por alto riesgo
        if (adultosMayores > 5)
        {
            MostrarMensaje("Alerta: hay más de 5 adultos mayores. Riesgo elevado.", ConsoleColor.Yellow);
        }

        // Cálculo de promedios
        double promedioGeneral = CalcularPromedio(sumaNiños + sumaJovenes + sumaAdultos + sumaAdultosMayores, totalTurnos);
        double promedioNiños = CalcularPromedio(sumaNiños, niños);
        double promedioJovenes = CalcularPromedio(sumaJovenes, jovenes);
        double promedioAdultos = CalcularPromedio(sumaAdultos, adultos);
        double promedioAdultosMayores = CalcularPromedio(sumaAdultosMayores, adultosMayores);

        Console.WriteLine("\n=== Promedios de Edad ===");
        Console.WriteLine($"Promedio general: {promedioGeneral:F2}");
        Console.WriteLine($"Promedio niños: {promedioNiños:F2}");
        Console.WriteLine($"Promedio jóvenes: {promedioJovenes:F2}");
        Console.WriteLine($"Promedio adultos: {promedioAdultos:F2}");
        Console.WriteLine($"Promedio adultos mayores: {promedioAdultosMayores:F2}");
        if (adultosMayores > 5)
        {
            MostrarMensaje("ALERTA: Hay más de 5 adultos mayores registrados. Riesgo elevado en la clínica.", ConsoleColor.Red);
        }

    }


    /// Muestra el menú principal para iniciar o salir del programa.

    static bool MostrarMenuInicio()
    {
        Console.Clear();
        Console.WriteLine("=== MENÚ PRINCIPAL ===");
        Console.WriteLine("1. Iniciar programa");
        Console.WriteLine("2. Salir");
        Console.Write("Selecciona una opción (1 o 2): ");
        string opcion = Console.ReadLine();

        if (opcion == "1")
        {
            Console.Clear();
            return true;
        }
        else if (opcion == "2")
        {
            return false;
        }
        else
        {
            MostrarMensaje("Opción no válida. Se cerrará el programa por seguridad.", ConsoleColor.Red);
            return false;
        }
    }

    static bool DeseaContinuar()
    {
        while (true)
        {
            Console.WriteLine("\n¿Deseas continuar?");
            Console.WriteLine("1. Sí, limpiar consola y repetir");
            Console.WriteLine("2. No, salir del programa");
            Console.Write("Selecciona una opción (1 o 2): ");
            string opcion = Console.ReadLine();

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


    /// Muestra un mensaje en consola con el color indicado.

    static void MostrarMensaje(string mensaje, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(mensaje);
        Console.ResetColor();
    }

    /// Calcula el promedio de edad, evitando división por cero.
    static double CalcularPromedio(int suma, int cantidad)
    {
        return cantidad > 0 ? (double)suma / cantidad : 0;
    }
}
