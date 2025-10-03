/*Reto 4: Longitud de cadena
Enunciado:
Crea dos arreglos unidimensionales que tengan el mismo tamaño (lo pedirá por teclado), en uno de ellos almacenaras nombres de personas como cadenas, 
en el otro arreglo ira almacenando la longitud de los nombres. Muestra por pantalla el nombre y la longitud que tiene.
•	Validación del nombre ingresado
Asegúrate de que:
•	El nombre no esté vacío.
•	No contenga números ni símbolos.
•	Sea mayor a 2 caracteres.
•	Mostrar clasificación por longitud
Clasifica los nombres en:
•	Cortos (1 – 4 letras)
•	Medios (5 – 7 letras)
•	Largos (8 + letras)*/

using System;
class Program
{
    static void Main()
    {
        Console.WriteLine(new string ('=', 20) + "\nLongitud de cadena.\n" + new string('=', 20));
        Console.WriteLine("Ingrese el tamaño de los arreglos:");
        int size;
        while (!int.TryParse(Console.ReadLine(), out size) || size <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Por favor, ingrese un número entero positivo.");
            Console.ResetColor();   
        }
        string[] nombres = new string[size]; //arreglo para nombres
        int[] longitudes = new int[size]; //arreglo para longitudes
        for (int i = 0; i < size; i++)
        {
            string ? nombre;
            while (true)
            {
                Console.WriteLine($"Ingrese el nombre de la persona {i + 1}:");
                nombre = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("El nombre no puede estar vacío.");
                    Console.ResetColor();
                    continue;
                }
                if (nombre.Length <= 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("El nombre debe tener más de 2 caracteres.");
                    Console.ResetColor();
                    continue;
                    
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(nombre, @"[^a-zA-Z\s]")) // Verifica si contiene caracteres no alfabéticose
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("El nombre no puede contener números ni símbolos.");
                    Console.ResetColor();
                    continue;
                }
                break;
            }
            nombres[i] = nombre;
            longitudes[i] = nombre.Length;
        }
        Console.WriteLine("\nNombres y sus longitudes:");
        for (int i = 0; i < size; i++)
        {
            Console.WriteLine($"{nombres[i]} - Longitud: {longitudes[i]}");
            if (longitudes[i] <= 4)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Clasificación: Corto");
                Console.ResetColor();
            }
            else if (longitudes[i] <= 7)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Clasificación: Medio");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Clasificación: Largo");
                Console.ResetColor();
            }
        }
    }
}