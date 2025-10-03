// Suma de arreglos invertidos
//El usuario ingresa dos arreglos de n números cada uno. 
//Suma ambos arreglos elemento a elemento, pero uno de ellos debe sumarse en orden inverso
// 	"Validaciones de entrada numérica"
//Antes de guardar cada número, valida que:
//	Sea un número válido.
//	Esté dentro de un rango determinado (por ejemplo, entre -100 y 100).
//No se repita (evitar duplicados).

using System;
using System.Collections.Generic;
using System.Linq;

public class ArreglosInvertidos
{
    // Define los parámetros de validación
    private const int MinRange = -100;
    private const int MaxRange = 100;

    // --- Lógica de Validación de Entrada ---

    /// <summary>
    /// Pide un número al usuario y asegura que cumple con todas las reglas de validación.
    /// </summary>
    /// <param name="existingNumbers">Lista de números ya ingresados para verificar duplicados.</param>
    /// <returns>Un número entero válido.</returns>

    private static int GetValidNumber(List<int> existingNumbers)
    {
        int number;
        bool isValid;

        do
        {
            Console.Write("Ingrese un número: ");
            string input = Console.ReadLine();
            isValid = true;

            // 1. Validar que sea un número válido (TryParse)
            if (!int.TryParse(input, out number))
            {
                Console.WriteLine(" Error: Por favor, ingrese un valor numérico entero válido.");
                isValid = false;
                continue;
            }

            // 2. Validar el rango
            if (number < MinRange || number > MaxRange)
            {
                Console.WriteLine($" Error: El número debe estar entre {MinRange} y {MaxRange}.");
                isValid = false;
                continue;
            }

            // 3. Validar duplicados (No se repita)
            if (existingNumbers.Contains(number))
            {
                Console.WriteLine($"Error: El número {number} ya ha sido ingresado. Evite duplicados.");
                isValid = false;
                continue;
            }

        } while (!isValid);

        // Si es válido, se agrega a la lista y se retorna
        existingNumbers.Add(number);
        return number;
    }

    // --- Lógica Principal del Reto ---

    public static void Main(string[] args)
    {
        Console.WriteLine("=== Reto 2: Suma de Arreglos Invertidos ===");

        int n;
        bool isLengthValid = false;

        // Solicitar la longitud (N) de los arreglos
        do
        {
            Console.Write("Ingrese el tamaño (N) de los arreglos: ");
            if (int.TryParse(Console.ReadLine(), out n) && n > 0)
            {
                isLengthValid = true;
            }
            else
            {
                Console.WriteLine("Error: Ingrese un número entero positivo para la longitud.");
            }
        } while (!isLengthValid);

        // Inicializar arreglos y listas para validación
        int[] arr1 = new int[n];
        int[] arr2 = new int[n];
        List<int> allNumbers = new List<int>(); // Usada para validar duplicados globales

        Console.WriteLine("\n--- Ingreso para arr1 ---");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Elemento {i + 1}/{n} para arr1 (Rango: [{MinRange}, {MaxRange}], Sin duplicados):");
            arr1[i] = GetValidNumber(allNumbers);
        }

        // NOTA: Reiniciar el control de duplicados para arr2 según la regla 
        // de "no se repita" *dentro* de cada arreglo, o mantenerlo global 
        // si la regla es para la totalidad de números ingresados. 
        // Siguiendo la interpretación de "Antes de guardar cada número, valida que..." 
        // lo mantendremos global (`allNumbers`).

        Console.WriteLine("\n--- Ingreso para arr2 ---");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Elemento {i + 1}/{n} para arr2 (Rango: [{MinRange}, {MaxRange}], Sin duplicados):");
            arr2[i] = GetValidNumber(allNumbers);
        }

        // --- Cálculo de la Suma Invertida ---

        int[] resultado = new int[n];

        // Opción eficiente: Iterar arr1 de forma normal e iterar arr2 de forma inversa.
        // arr2Index inverso: comienza en (n-1) y decrece a 0.

        for (int i = 0; i < n; i++)
        {
            // El índice para arr2 será N - 1 - i. 
            // Si N=5 y i=0, el índice es 4. Si i=4, el índice es 0.
            int arr2IndexInverted = n - 1 - i;

            resultado[i] = arr1[i] + arr2[arr2IndexInverted];
        }

        // --- Mostrar Resultado ---

        Console.WriteLine("\n==============================");
        Console.WriteLine($"arr1: [{string.Join(", ", arr1)}]");
        // Muestra arr2 en el orden en que se usó para la suma (invertido)
        Console.WriteLine($"arr2 (Invertido): [{string.Join(", ", arr2.Reverse())}]");
        Console.WriteLine("------------------------------");
        Console.WriteLine($"Resultado: [{string.Join(", ", resultado)}]");
        Console.WriteLine("==============================");
    }
}
