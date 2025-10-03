// Solicita al usuario ingresar 7 temperaturas registradas durante una semana (en °C) y guárdalas en un arreglo
// crea otro arreglo donde se almacenen esas mismas temperaturas convertidas a Fahrenheit.
// Muestra ambos arreglos en paralelo (°C ↔ °F).
// Agrega cálculos adicionales como:
// Temperatura promedio.
// Día más frío y día más caluroso.
//	Cuántos días estuvieron por encima de 30 °C.
//Cuántos días estuvieron bajo cero.

using System;
using System.Linq;

public class ConversorTemperaturas
{
    // Función auxiliar para mostrar la temperatura con un color específico
    public static void MostrarTemperaturaConColor(double temperatura)
    {
        if (temperatura >= 30) // Caliente (rojo/amarillo)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else if (temperatura <= 10) // Frío (azul/cyan)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }
        else // Moderado (verde)
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        Console.Write($"{temperatura:F2}");
        Console.ResetColor(); // Importante: restablecer el color original después de escribir
    }

    public static void Main(string[] args)
    {
        bool continuar = true;

        Console.Title = " Conversor de Temperaturas C#";
        Console.WriteLine("-------------------------------------------------------");
        Console.WriteLine(" CONVERSOR Y ESTADÍSTICAS DE TEMPERATURAS SEMANALES ");
        Console.WriteLine("-------------------------------------------------------");

        do
        {
            // Definición de constantes y arreglos
            const int NumeroDeDias = 7;
            double[] temperaturasCelsius = new double[NumeroDeDias];
            double[] temperaturasFahrenheit = new double[NumeroDeDias];
            string[] diasSemana = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };

            Console.WriteLine("\n--- INGRESO DE TEMPERATURAS DE LA SEMANA ---");

            // 1. Solicitud de temperaturas y almacenamiento
            for (int i = 0; i < NumeroDeDias; i++)
            {
                Console.Write($"Ingresa la temperatura del {diasSemana[i]} (en °C): ");
                string input = Console.ReadLine();

                if (double.TryParse(input, out double tempC))
                {
                    temperaturasCelsius[i] = tempC;
                    // Conversión de Celsius a Fahrenheit: F = (C * 9/5) + 32
                    temperaturasFahrenheit[i] = (tempC * 9 / 5) + 32;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Entrada inválida. Por favor, ingresa un número válido.");
                    Console.ResetColor();
                    i--; // Retrocede el contador para reintentar la entrada del mismo día
                }
            }

            // 2. Mostrar ambos arreglos en paralelo (°C ↔ °F) con colores
            Console.WriteLine("\n\n--- TEMPERATURAS REGISTRADAS (C ↔ F) ---");
            Console.WriteLine("{0,-10} | {1,-14} | {2,-14}", "Día", "°C", "°F");
            Console.WriteLine("------------------------------------------");

            for (int i = 0; i < NumeroDeDias; i++)
            {
                Console.Write("{0,-10} | ", diasSemana[i]);
                // Muestra Celsius con color
                MostrarTemperaturaConColor(temperaturasCelsius[i]);
                Console.Write(" °C    | ");
                // Muestra Fahrenheit con color (usando la misma lógica de colores basada en Celsius)
                MostrarTemperaturaConColor(temperaturasCelsius[i]);
                Console.WriteLine(" °F");
            }
            Console.WriteLine("------------------------------------------");

            // --- Cálculo y Muestra de Estadísticas Avanzadas ---
            double promedioCelsius = temperaturasCelsius.Average();
            double tempMin = temperaturasCelsius.Min();
            double tempMax = temperaturasCelsius.Max();

            int indiceDiaFrio = Array.IndexOf(temperaturasCelsius, tempMin);
            int indiceDiaCaluroso = Array.IndexOf(temperaturasCelsius, tempMax);

            int diasMayorA30 = temperaturasCelsius.Count(temp => temp > 30);
            int diasBajoCero = temperaturasCelsius.Count(temp => temp < 0);

            Console.WriteLine("\n--- ESTADÍSTICAS ADICIONALES ---");
            Console.WriteLine($"\n Temperatura promedio de la semana: {promedioCelsius:F2} °C");

            Console.Write(" Día más frío: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{diasSemana[indiceDiaFrio]} con {tempMin:F2} °C");
            Console.ResetColor();

            Console.Write(" Día más caluroso: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{diasSemana[indiceDiaCaluroso]} con {tempMax:F2} °C");
            Console.ResetColor();

            Console.WriteLine($"\n Días por encima de 30 °C: {diasMayorA30} día(s)");
            Console.WriteLine($" Días bajo cero (< 0 °C): {diasBajoCero} día(s)");

            // Preguntar al usuario si desea continuar
            Console.WriteLine("\n------------------------------------------");
            Console.Write("¿Deseas ingresar otra semana de temperaturas? (S/N): ");
            string respuesta = Console.ReadLine();

            if (respuesta.Trim().ToUpper() != "S")
            {
                continuar = false;
            }

            Console.Clear(); // Limpia la consola antes de la siguiente iteración o de salir

        } while (continuar);

        Console.WriteLine("Programa finalizado. ¡Gracias por usar el conversor!");
        Console.ReadKey();
    }
}
