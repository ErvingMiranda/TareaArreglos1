/*
Reto 1: Conversor de temperaturas
Enunciado:
Solicita al usuario ingresar 7 temperaturas registradas durante una semana (en °C) y guárdalas en un arreglo. Luego, crea otro arreglo donde se almacenen esas mismas temperaturas convertidas a Fahrenheit.

Muestra ambos arreglos en paralelo (°C ↔ °F).
•	Cálculo de estadísticas avanzadas
Agrega cálculos adicionales como:
•	Temperatura promedio.
•	Día más frío y día más caluroso.
•	Cuántos días estuvieron por encima de 30 °C.
•	Cuántos días estuvieron bajo cero.
*/
using System;
using System.Linq;

class Program
{
    // Colores centralizados para mejorar la legibilidad en consola
    static class Colores
    {
        public static readonly ConsoleColor Encabezado = ConsoleColor.Cyan;
        public static readonly ConsoleColor Texto      = ConsoleColor.White;
        public static readonly ConsoleColor Alerta     = ConsoleColor.Yellow;
        public static readonly ConsoleColor Valor      = ConsoleColor.Green;
        public static readonly ConsoleColor Error      = ConsoleColor.Red;
    }

    // Rango válido para las temperaturas (ajustable según la necesidad)
    // Aquí se permite desde -100 °C (condiciones extremas polares) hasta 60 °C (olas de calor)
    const double MinC = -100.0;
    const double MaxC = 60.0;

    // Arreglo para almacenar las 7 temperaturas de la semana
    static double[] temperaturasC = new double[7];
    static bool datosCargados = false; // Bandera que indica si ya se ingresaron datos

    static void Main()
    {
        Console.ForegroundColor = Colores.Texto;
        while (true)
        {
            Console.Clear();
            MostrarEncabezado("CONVERSOR DE TEMPERATURAS (°C a °F)");
            MostrarMenu();

            Console.Write("Elige una opción (1-5): ");
            string op = Console.ReadLine()!.Trim();

            switch (op)
            {
                case "1": IngresarTemperaturas();  break;
                case "2": MostrarTabla();          break;
                case "3": MostrarEstadisticas();   break;
                case "4": ReiniciarDatos();        break;
                case "5":
                    Console.Write("¿Seguro que deseas salir? (S/N): ");
                    if (LeerConfirmacion()) return; // Salida del programa
                    break;
                default:
                    Aviso("Opción inválida. Intenta de nuevo.");
                    Pausa();
                    break;
            }
        }
    }

    // Muestra el menú principal
    static void MostrarMenu()
    {
        Console.WriteLine("1) Ingresar temperaturas de la semana (°C)");
        Console.WriteLine("2) Mostrar tabla °C a °F");
        Console.WriteLine("3) Ver estadísticas");
        Console.WriteLine("4) Reiniciar datos");
        Console.WriteLine("5) Salir");
        Console.WriteLine();
    }

    // Opción 1: Ingreso de temperaturas (con validación de rango)
    static void IngresarTemperaturas()
    {
        Console.Clear();
        MostrarEncabezado("INGRESO DE TEMPERATURAS (°C)");

        for (int i = 0; i < temperaturasC.Length; i++)
        {
            Console.Write($"Ingrese la temperatura del día {i + 1} en °C (entre {MinC} y {MaxC}): ");
            temperaturasC[i] = LeerNumeroEnRango(MinC, MaxC, "temperatura");
        }

        datosCargados = true; // Se confirma que ya hay datos cargados

        Console.ForegroundColor = Colores.Valor;
        Console.WriteLine("\nTemperaturas registradas correctamente.");
        Console.ResetColor();

        Pausa();
    }

    // Opción 2: Mostrar tabla con conversión °C ↔ °F
    static void MostrarTabla()
    {
        if (!VerificarDatos()) return;

        Console.Clear();
        MostrarEncabezado("TABLA DE CONVERSIÓN °C a °F");

        // Convertimos las temperaturas a Fahrenheit
        var temperaturasF = temperaturasC.Select(c => CelsiusAFahrenheit(c)).ToArray();

        // Encabezado de tabla
        Console.WriteLine(" Día |    °C   |    °F  ");
        Console.WriteLine("-------------------------");

        // Imprimir cada fila
        for (int i = 0; i < temperaturasC.Length; i++)
        {
            Console.WriteLine($"{(i + 1),3} | {temperaturasC[i],7:F2} | {temperaturasF[i],7:F2}");
        }

        Pausa();
    }

    // Opción 3: Mostrar estadísticas
    static void MostrarEstadisticas()
    {
        if (!VerificarDatos()) return;

        Console.Clear();
        MostrarEncabezado("ESTADÍSTICAS");

        // Mostrar todas las temperaturas ingresadas
        Console.WriteLine("Temperaturas registradas (°C):");
        Console.WriteLine("-------------------------------");
        for (int i = 0; i < temperaturasC.Length; i++)
        {
            Console.WriteLine($"Día {i + 1}: {temperaturasC[i]:F2} °C");
        }
        Console.WriteLine();

        // Cálculos principales
        double promedio = temperaturasC.Average();
        int idxMin = IndiceMin(temperaturasC); // índice del día más frío
        int idxMax = IndiceMax(temperaturasC); // índice del día más caluroso

        // Conteo de días extremos
        int diasSobre30 = temperaturasC.Count(t => t > 30); // días con más de 30 °C
        int diasBajoCero = temperaturasC.Count(t => t < 0); // días bajo 0 °C

        // Mostrar resultados
        Console.ForegroundColor = Colores.Valor;
        Console.WriteLine($"Promedio semanal: {promedio:F2} °C");
        Console.WriteLine($"Día más frío: Día {idxMin + 1} ({temperaturasC[idxMin]:F2} °C)");
        Console.WriteLine($"Día más caluroso: Día {idxMax + 1} ({temperaturasC[idxMax]:F2} °C)");
        Console.WriteLine($"Días sobre 30 °C: {diasSobre30}");
        Console.WriteLine($"Días bajo cero: {diasBajoCero}");
        Console.ResetColor();

        Pausa();
    }

    // Opción 4: Reiniciar datos
    static void ReiniciarDatos()
    {
        Console.Write("Esto borrará las temperaturas actuales. ¿Confirmas? (S/N): ");
        if (!LeerConfirmacion()) return;

        temperaturasC = new double[7]; // reinicia el arreglo
        datosCargados = false;

        Console.ForegroundColor = Colores.Valor;
        Console.WriteLine("Datos reiniciados.");
        Console.ResetColor();
        Pausa();
    }

    // Utilidades de impresión y control de flujo
    static void MostrarEncabezado(string titulo)
    {
        Console.ForegroundColor = Colores.Encabezado;
        Console.WriteLine(new string('=', titulo.Length));
        Console.WriteLine(titulo);
        Console.WriteLine(new string('=', titulo.Length));
        Console.ResetColor();
        Console.WriteLine();
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

    // Verifica si hay datos cargados antes de operar
    static bool VerificarDatos()
    {
        if (!datosCargados)
        {
            Error("No hay datos cargados. Ve a '1) Ingresar temperaturas' primero.");
            Pausa();
            return false;
        }
        return true;
    }

    // Conversión °C → °F
    static double CelsiusAFahrenheit(double c) => (c * 9 / 5) + 32;

    // Devuelve índice del valor mínimo
    static int IndiceMin(double[] arr)
    {
        int idx = 0;
        for (int i = 1; i < arr.Length; i++)
            if (arr[i] < arr[idx]) idx = i;
        return idx;
    }

    // Devuelve índice del valor máximo
    static int IndiceMax(double[] arr)
    {
        int idx = 0;
        for (int i = 1; i < arr.Length; i++)
            if (arr[i] > arr[idx]) idx = i;
        return idx;
    }

    // Lectura validada: solo números y dentro de rango
    static double LeerNumeroEnRango(double min, double max, string etiqueta)
    {
        while (true)
        {
            string? s = Console.ReadLine();

            if (double.TryParse(s, System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture, out double v) || 
                double.TryParse(s, out v))
            {
                if (v < min || v > max)
                {
                    Error($"Valor fuera de rango. {etiqueta} debe estar entre {min} y {max} °C.");
                    Console.Write("Intenta de nuevo: ");
                    continue;
                }
                return v; // dato válido
            }

            Aviso("Entrada inválida. Intenta de nuevo: ");
            Console.Write("> ");
        }
    }

    // Lectura de confirmación (S/N)
    static bool LeerConfirmacion()
    {
        while (true)
        {
            string? s = Console.ReadLine();
            if (s == null) continue;
            s = s.Trim().ToUpperInvariant();
            if (s == "S") return true;
            if (s == "N") return false;

            Aviso("Por favor, escribe S o N:");
            Console.Write("> ");
        }
    }
}
