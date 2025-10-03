/*
Reto 2: Suma de arreglos invertidos

Enunciado:
El usuario ingresa dos arreglos de n números cada uno. 
Suma ambos arreglos elemento a elemento, pero uno de ellos debe sumarse en orden inverso.

Ejemplo:
arreglo1 = [1, 2, 3, 4, 5]
arreglo2 = [10, 20, 30, 40, 50]

Resultado: [1+50, 2+40, 3+30, 4+20, 5+10] = [51, 42, 33, 24, 15]

Validaciones de entrada numérica:
Antes de guardar cada número, valida que:
- Sea un número válido.
- Esté dentro de un rango determinado (por ejemplo, entre -100 y 100).
- No se repita (evitar duplicados).
*/

using System;
using System.Globalization;
using System.Linq;

class Program
{
    // Colores para mejorar legibilidad en consola
    static class Colores
    {
        public static readonly ConsoleColor Encabezado = ConsoleColor.Cyan;
        public static readonly ConsoleColor Texto      = ConsoleColor.White;
        public static readonly ConsoleColor Valor      = ConsoleColor.Green;
        public static readonly ConsoleColor Alerta     = ConsoleColor.Yellow;
        public static readonly ConsoleColor Error      = ConsoleColor.Red;
    }

    // Límites del tamaño de los arreglos
    const int MinN = 1;
    const int MaxN = 20;

    // Rango válido para cada número
    const int MinValor = -100;
    const int MaxValor = 100;

    static readonly Random Rng = new Random();

    // Variables globales para los arreglos y su tamaño
    static int n = 0;
    static int[]? arreglo1 = null;
    static int[]? arreglo2 = null;

    static void Main()
    {
        Console.ForegroundColor = Colores.Texto;

        // Bucle principal del programa
        while (true)
        {
            Console.Clear();
            MostrarEncabezado("Suma de arreglos invertidos");
            MostrarMenu();

            Console.Write("Elige una opción (1-4): ");
            string? op = Console.ReadLine()?.Trim();

            switch (op)
            {
                case "1": CrearArreglosManuales(); break;
                case "2": GenerarArreglosAutomaticos(); break;
                case "3": SumarArreglos(); break;
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

    // Menú principal
    static void MostrarMenu()
    {
        Console.WriteLine("1) Ingresar los arreglos manualmente");
        Console.WriteLine("2) Generar los arreglos automáticamente");
        Console.WriteLine("3) Calcular la suma");
        Console.WriteLine("4) Salir");
        Console.WriteLine();
    }

    // Opción 1: ingreso manual de valores, con validaciones
    static void CrearArreglosManuales()
    {
        Console.Write($"¿De qué tamaño serán los arreglos? ({MinN}..{MaxN}): ");
        n = LeerEnteroEnRango(MinN, MaxN, "tamaño");

        Console.WriteLine();
        arreglo1 = LeerArregloEnteros(n, "primer arreglo");
        Console.WriteLine();
        arreglo2 = LeerArregloEnteros(n, "segundo arreglo");

        Ok("Arreglos ingresados correctamente.");
        Pausa();
    }

    // Opción 2: generación automática de arreglos sin duplicados
    static void GenerarArreglosAutomaticos()
    {
        Console.Write($"¿De qué tamaño serán los arreglos? ({MinN}..{MaxN}): ");
        n = LeerEnteroEnRango(MinN, MaxN, "tamaño");

        arreglo1 = GenerarArregloAleatorio(n);
        arreglo2 = GenerarArregloAleatorio(n);

        Console.WriteLine();
        Console.ForegroundColor = Colores.Encabezado;
        Console.WriteLine("Se generaron los siguientes arreglos:");
        Console.ResetColor();
        Console.WriteLine($"arreglo1 = {Formatear(arreglo1)}");
        Console.WriteLine($"arreglo2 = {Formatear(arreglo2)}");

        Ok("Listos para sumar en la opción 3.");
        Pausa();
    }

    // Opción 3: cálculo de la suma con detalle en el mismo formato del ejemplo
    static void SumarArreglos()
    {
        if (!ValidarArreglos()) return;

        var a1 = arreglo1!;
        var a2 = arreglo2!;
        int[] resultado = new int[n];

        for (int i = 0; i < n; i++)
            resultado[i] = a1[i] + a2[n - 1 - i];

        // Creamos el detalle de las operaciones: [a+b, ...]
        string detalle = "[" + string.Join(", ", Enumerable.Range(0, n)
                                    .Select(i => $"{a1[i]}+{a2[n - 1 - i]}")) + "]";

        string resStr = Formatear(resultado);

        Console.WriteLine();
        Console.ForegroundColor = Colores.Encabezado;
        Console.WriteLine("Resultado final:");
        Console.ResetColor();

        Console.WriteLine($"arreglo1 = {Formatear(a1)}");
        Console.WriteLine($"arreglo2 = {Formatear(a2)}");
        Console.WriteLine($"Resultado: {detalle} = {resStr}");

        Pausa();
    }

    // Genera n números aleatorios dentro del rango permitido, sin repetidos
    static int[] GenerarArregloAleatorio(int n)
    {
        return Enumerable.Range(MinValor, MaxValor - MinValor + 1)
                         .OrderBy(_ => Rng.Next())
                         .Take(n)
                         .ToArray();
    }

    // Lectura de arreglo con validación de número, rango y duplicados
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
                    Aviso("Ese número ya fue ingresado en este arreglo, prueba con otro.");
                    continue;
                }

                v[i] = valor;
                break;
            }
        }
        return v;
    }

    // Se asegura de que haya arreglos válidos antes de sumarlos
    static bool ValidarArreglos()
    {
        if (n <= 0 || arreglo1 is null || arreglo2 is null)
        {
            Error("Primero debes crear o generar los arreglos (opciones 1 o 2).");
            Pausa();
            return false;
        }
        return true;
    }

    // Leer un número en un rango (para pedir el tamaño n)
    static int LeerEnteroEnRango(int min, int max, string etiqueta)
    {
        while (true)
        {
            string? s = Console.ReadLine();
            if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out int v) ||
                int.TryParse(s, out v))
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

    // Confirmación S/N al salir
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

    // Formatea un arreglo en formato [a, b, c]
    // Aquí se usa LINQ: Select convierte cada número en string antes de unirlos
    static string Formatear(int[] xs)
        => "[" + string.Join(", ", xs.Select(x => x.ToString())) + "]";

    // Encabezado bonito para cada sección
    static void MostrarEncabezado(string titulo)
    {
        Console.ForegroundColor = Colores.Encabezado;
        Console.WriteLine(new string('=', titulo.Length));
        Console.WriteLine(titulo);
        Console.WriteLine(new string('=', titulo.Length));
        Console.ResetColor();
        Console.WriteLine();
    }

    // Mensajes auxiliares con colores
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
