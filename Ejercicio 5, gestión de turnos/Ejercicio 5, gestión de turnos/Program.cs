/*Reto '5' no 6, no se como renombrar esta cosa sin que explote : Gestión de turnos en una clínica
Enunciado:
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
5.	Calcular la edad promedio general y por grupo.*/

using System;
class Program
{
    static void Main()
    {
        Console.WriteLine(new string('=', 18) + "\nGestión de turnos.\n" + new string('=', 18));
        const int totalTurnos = 20;
        int[] edades = new int[totalTurnos];
        int niños = 0, jóvenes = 0, adultos = 0, mayores = 0;
        int sumaNiños = 0, sumaJóvenes = 0, sumaAdultos = 0, sumaMayores = 0;

        for (int i = 0; i < totalTurnos; i++)
        {
            int edad;
            while (true)
            {

                Console.Write($"Ingrese la edad del paciente {i + 1}: ");

                string? input = Console.ReadLine();
                if (!int.TryParse(input, out edad) || edad < 0 || edad > 120)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Edad no válida. Por favor, ingrese un número entre 0 y 120.");
                    Console.ResetColor();
                }
                else
                {
                    break;
                }
            }
            edades[i] = edad;
            if (edad >= 0 && edad <= 12)
            {
                niños++;
                sumaNiños += edad;
            }
            else if (edad >= 13 && edad <= 25)
            {
                jóvenes++;
                sumaJóvenes += edad;
            }
            else if (edad >= 26 && edad <= 60)
            {
                adultos++;
                sumaAdultos += edad;
            }
            else if (edad > 60)
            {
                mayores++;
                sumaMayores += edad;
            }
        }

        Console.WriteLine("\n" + new string('=', 30));
        Console.WriteLine($"\nDistribución de edades:");
        Console.WriteLine($"Niños (0-12): {niños}");
        Console.WriteLine($"Jóvenes (13-25): {jóvenes}");
        Console.WriteLine($"Adultos (26-60): {adultos}");
        Console.WriteLine($"Mayores (>60): {mayores}");
        Console.WriteLine("\n" + new string('=', 30));


        if (mayores > 5)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ha un alto riegos porque hay más de 5 personas mayores.");
            Console.ResetColor();
        }
        // calcular promedios
        double promedioGeneral = (double)(sumaNiños + sumaJóvenes + sumaAdultos + sumaMayores) / totalTurnos;
        double promedioNiños = niños > 0 ? (double)sumaNiños / niños : 0;
        double promedioJóvenes = jóvenes > 0 ? (double)sumaJóvenes / jóvenes : 0;
        double promedioAdultos = adultos > 0 ? (double)sumaAdultos / adultos : 0;
        double promedioMayores = mayores > 0 ? (double)sumaMayores / mayores : 0;
        //imprimir todo
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\nEdad promedio general: {promedioGeneral:F2}");
        Console.WriteLine($"Promedio Niños: {promedioNiños:F2}");
        Console.WriteLine($"Promedio Jóvenes: {promedioJóvenes:F2}");
        Console.WriteLine($"Promedio Adultos: {promedioAdultos:F2}");
        Console.WriteLine($"Promedio Mayores: {promedioMayores:F2}");
        Console.ResetColor();
    }
}