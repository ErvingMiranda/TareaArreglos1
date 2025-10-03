/*
Reto 3: Intercalar arreglos

Enunciado:
Solicita dos arreglos de n elementos cada uno. 
Crea un tercer arreglo que combine ambos intercalando los valores, por ejemplo:

arr1 = [1, 3, 5, 7, 9]
arr2 = [2, 4, 6, 8, 10]
resultado = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]

Identificar origen de cada valor:
Después de crear el arreglo intercalado, muestra para cada número si proviene de arr1 o arr2.

Ejemplo:
1 (arr1)
2 (arr2)
3 (arr1)
4 (arr2)
...
*/

using System;
using System.Globalization;
using System.Linq;

class Program
{
    static class Colores
    {
        public static readonly ConsoleColor Encabezado = ConsoleColor.Cyan;
        public static readonly ConsoleColor Texto      = ConsoleColor.White;
        public static readonly ConsoleColor Valor      = ConsoleColor.Green;
        public static readonly ConsoleColor Alerta     = ConsoleColor.Yellow;
        public static readonly ConsoleColor Error      = ConsoleColor.Red;
    }

    // Límites
    const int MinN = 1;
    const int MaxN = 20;
    const int MinValor = -100;
    const int MaxValor = 100;

    static int n = 0;
    static int[]? arr1 = null;
    static int[]? arr2 = null;

    static void Main()
    {
        Console.ForegroundColor = Colores.Texto;

        while (true)
        {
            Console.Clear();
            MostrarEncabezado("Intercalar arreglos");
            MostrarMenu();

            Console.Write("Elige una opción (1-4): ");
            string? op = Console.ReadLine()?.Trim();

            switch (op)
            {
                case "1": CrearArreglosManuales(); break;
                case "2": GenerarArreglosAutomaticos(); break;
                case "3": IntercalarArreglos(); break;
                case "4":
                    Console.Write("¿Seguro que deseas salir? (S/N): ");
                    if (LeerConfirmacionSN()) return;
                    break;
                default:
                    Aviso("Opción inválida, intenta de nuevo.");
                    Pausa();
                    break;
            }
        }
    }

    static void MostrarMenu()
    {
        Console.WriteLine("1) Ingresar los arreglos manualmente");
        Console.WriteLine("2) Generar los arreglos automáticamente");
        Console.WriteLine("3) Intercalar y mostrar origen de valores");
        Console.WriteLine("4) Salir");
        Console.WriteLine();
    }

    // Entrada manual con validación
    static void CrearArreglosManuales()
    {
        Console.Write($"¿De qué tamaño serán los arreglos? ({MinN}..{MaxN}): ");
        n = LeerEnteroEnRango(MinN, MaxN, "tamaño");

        Console.WriteLine();
        arr1 = LeerArregloEnteros(n, "primer arreglo");
        Console.WriteLine();
        arr2 = LeerArregloEnteros(n, "segundo arreglo");

        Ok("Arreglos ingresados correctamente.");
        Pausa();
    }

    // Automáticos con números aleatorios (sin duplicados)
    static void GenerarArreglosAutomaticos()
    {
        var rng = new Random();

        Console.Write($"¿De qué tamaño serán los arreglos? ({MinN}..{MaxN}): ");
        n = LeerEnteroEnRango(MinN, MaxN, "tamaño");

        arr1 = Enumerable.Range(MinValor, MaxValor - MinValor + 1)
                         .OrderBy(_ => rng.Next())
                         .Take(n)
                         .ToArray();

        arr2 = Enumerable.Range(MinValor, MaxValor - MinValor + 1)
                         .OrderBy(_ => rng.Next())
                         .Take(n)
                         .ToArray();

        Console.WriteLine();
        Console.WriteLine($"arr1 = {Formatear(arr1)}");
        Console.WriteLine($"arr2 = {Formatear(arr2)}");

        Ok("Listos para intercalar en la opción 3.");
        Pausa();
    }

    // Intercala los valores y muestra el origen de cada número
    static void IntercalarArreglos()
    {
        if (!ValidarArreglos()) return;

        var intercalado = new int[n * 2];
        for (int i = 0; i < n; i++)
        {
            intercalado[2 * i]     = arr1![i];
            intercalado[2 * i + 1] = arr2![i];
        }

        Console.WriteLine();
        Console.ForegroundColor = Colores.Encabezado;
        Console.WriteLine("Resultado:");
        Console.ResetColor();
        Console.WriteLine($"arr1 = {Formatear(arr1!)}");
        Console.WriteLine($"arr2 = {Formatear(arr2!)}");
        Console.WriteLine($"intercalado = {Formatear(intercalado)}");

        Console.WriteLine("\nOrigen de cada valor:");
        for (int i = 0; i < intercalado.Length; i++)
        {
            string origen = (i % 2 == 0) ? "arr1" : "arr2";
            Console.WriteLine($"{intercalado[i]} ({origen})");
        }

        Pausa();
    }

    static int[] LeerArregloEnteros(int n, string nombre)
    {
        Console.WriteLine($"Ingresa {n} números enteros para el {nombre}:");
        var v = new int[n];

        for (int i = 0; i < n; i++)
        {
            while (true)
            {
                Console.Write($"{nombre}[{i}]: ");
                string? s = Console.ReadLine();

                if (!int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out int valor))
                {
                    Aviso("Entrada inválida. Debe ser un número entero.");
                    continue;
                }

                if (valor < MinValor || valor > MaxValor)
                {
                    Aviso($"El número debe estar entre {MinValor} y {MaxValor}.");
                    continue;
                }

                if (v.Take(i).Contains(valor))
                {
                    Aviso("Ese número ya fue ingresado en este arreglo.");
                    continue;
                }

                v[i] = valor;
                break;
            }
        }
        return v;
    }

    static bool ValidarArreglos()
    {
        if (n <= 0 || arr1 is null || arr2 is null)
        {
            Error("Primero debes crear o generar los arreglos (opciones 1 o 2).");
            Pausa();
            return false;
        }
        return true;
    }

    static int LeerEnteroEnRango(int min, int max, string etiqueta)
    {
        while (true)
        {
            string? s = Console.ReadLine();
            if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out int v))
            {
                if (v < min || v > max)
                {
                    Error($"El {etiqueta} debe estar entre {min} y {max}.");
                    Console.Write("Intenta de nuevo: ");
                    continue;
                }
                return v;
            }
            Aviso("Entrada inválida. Ingresa un número entero.");
            Console.Write("Intenta de nuevo: ");
        }
    }

    static bool LeerConfirmacionSN()
    {
        while (true)
        {
            string? s = Console.ReadLine();
            if (s == null) continue;
            s = s.Trim().ToUpperInvariant();
            if (s == "S") return true;
            if (s == "N") return false;

            Aviso("Por favor escribe S o N: ");
            Console.Write("> ");
        }
    }

    static string Formatear(int[] xs)
        => "[" + string.Join(", ", xs.Select(x => x.ToString())) + "]";

    static void MostrarEncabezado(string titulo)
    {
        Console.ForegroundColor = Colores.Encabezado;
        Console.WriteLine(new string('=', titulo.Length));
        Console.WriteLine(titulo);
        Console.WriteLine(new string('=', titulo.Length));
        Console.ResetColor();
        Console.WriteLine();
    }

    static void Ok(string msg)
    {
        Console.ForegroundColor = Colores.Valor;
        Console.WriteLine(msg);
        Console.ResetColor();
    }

    static void Aviso(string msg)
    {
        Console.ForegroundColor = Colores.Alerta;
        Console.WriteLine(msg);
        Console.ResetColor();
    }

    static void Error(string msg)
    {
        Console.ForegroundColor = Colores.Error;
        Console.WriteLine(msg);
        Console.ResetColor();
    }

    static void Pausa()
    {
        Console.Write("\nPresiona una tecla para continuar...");
        Console.ReadKey(true);
    }
}
