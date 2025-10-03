// INTERCALAR ARREGLO 
// Solicita dos arreglos de n elementos cada uno. Crea un tercer arreglo que combine ambos intercalando los valores.
// IDENTIFICAR ORIGEN DE CADA VALOR 
//Después de crear el arreglo intercalado, muestra para cada número si proviene de arr1 o arr2. 
using System;
using System.Collections.Generic;
using System.Linq;

public class RetoIntercalarArreglos
{
    // Clase interna para almacenar el valor y su origen
    public class ResultadoItem
    {
        public int Valor { get; set; }
        public string Origen { get; set; }

        public override string ToString()
        {
            return $"{Valor} ({Origen})";
        }
    }

    public static int[] IntercalarArreglos(int[] arr1, int[] arr2, out List<ResultadoItem> origenDeValores)
    {
        int n = arr1.Length;
        // Inicializa el arreglo resultado que tendrá 2*n elementos
        int[] resultado = new int[2 * n];

        // Inicializa la lista de origen
        origenDeValores = new List<ResultadoItem>();

        // Intercala los elementos
        for (int i = 0; i < n; i++)
        {
            // El elemento de arr1 va a la posición par (2*i)
            resultado[2 * i] = arr1[i];
            origenDeValores.Add(new ResultadoItem { Valor = arr1[i], Origen = "arr1" });

            // El elemento de arr2 va a la posición impar (2*i + 1)
            resultado[2 * i + 1] = arr2[i];
            origenDeValores.Add(new ResultadoItem { Valor = arr2[i], Origen = "arr2" });
        }

        return resultado;
    }

    public static void Main(string[] args)
    {
        // --- Ejemplo de uso con los arreglos del enunciado ---
        int[] arr1_ejemplo = { 1, 3, 5, 7, 9 };
        int[] arr2_ejemplo = { 2, 4, 6, 8, 10 };

        Console.WriteLine("Arreglo 1 (arr1): [" + string.Join(", ", arr1_ejemplo) + "]");
        Console.WriteLine("Arreglo 2 (arr2): [" + string.Join(", ", arr2_ejemplo) + "]");

        List<ResultadoItem> origenDeValores;

        // 1. Crear el arreglo intercalado
        int[] arreglo_intercalado = IntercalarArreglos(arr1_ejemplo, arr2_ejemplo, out origenDeValores);

        Console.WriteLine("\n--- Resultado: Intercalar arreglos ---");
        Console.WriteLine($"Arreglo intercalado (resultado) = [{string.Join(", ", arreglo_intercalado)}]");

        // --- 2. Identificar el origen de cada valor ---
        Console.WriteLine("\n--- Origen de cada valor en el resultado ---");

        // Muestra el resultado como se pide en el enunciado
        for (int i = 0; i < origenDeValores.Count; i++)
        {
            // La numeración comienza en 1
            Console.WriteLine($"{i + 1} {origenDeValores[i].ToString()}");
        }
    }
}