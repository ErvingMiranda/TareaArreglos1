using System;
using System.Text.RegularExpressions;

class ProgramaLongitudNombres
{
    static void Main()
    {
        // Mostrar menú principal al iniciar el programa
        if (!MostrarMenuInicio())
        {
            MostrarMensaje("Programa cerrado por decisión del usuario.", ConsoleColor.Yellow);
            return;
        }

        // Ciclo principal del programa: se repite si el usuario desea continuar
        do
        {
            EjecutarPrograma();
        }
        while (DeseaContinuar());
    }

    /// Función principal que ejecuta el ejercicio de longitud de nombres.
    static void EjecutarPrograma()
    {
        Console.WriteLine("=== Ejercicio 4: Longitud de cadena ===");

        int cantidad = 0;

        // Solicita al usuario cuántos nombres desea ingresar, validando que sea un número positivo
        while (true)
        {
            Console.Write("¿Cuántos nombres deseas ingresar? ");
            string entrada = Console.ReadLine();

            if (int.TryParse(entrada, out cantidad) && cantidad > 0)
                break;
            else
                MostrarMensaje("Ingresa un número entero positivo.", ConsoleColor.Red);
        }

        // Inicialización de arreglos para almacenar nombres y sus longitudes
        string[] nombres = new string[cantidad];
        int[] longitudes = new int[cantidad];

        // Ciclo para ingresar y validar cada nombre
        for (int i = 0; i < cantidad; i++)
        {
            while (true)
            {
                Console.Write($"Ingresa el nombre #{i + 1}: ");
                string nombre = Console.ReadLine().Trim();

                // Validación: no puede estar vacío
                if (string.IsNullOrEmpty(nombre))
                {
                    MostrarMensaje("El nombre no puede estar vacío.", ConsoleColor.Red);
                    continue;
                }

                // Validación: mínimo 3 caracteres
                if (nombre.Length < 3)
                {
                    MostrarMensaje("El nombre debe tener al menos 3 letras.", ConsoleColor.Red);
                    continue;
                }

                // Validación: solo letras y espacios (sin números ni símbolos)
                if (!Regex.IsMatch(nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                {
                    MostrarMensaje("El nombre solo debe contener letras y espacios.", ConsoleColor.Red);
                    continue;
                }

                // Si pasa todas las validaciones, se guarda el nombre y su longitud (sin contar espacios)
                nombres[i] = nombre;
                longitudes[i] = nombre.Replace(" ", "").Length;
                MostrarMensaje("Nombre válido registrado.", ConsoleColor.Green);
                break;
            }
        }

        // Mostrar resultados con clasificación por longitud
        Console.WriteLine("\n=== Resultados ===");
        for (int i = 0; i < cantidad; i++)
        {
            string clasificacion = longitudes[i] <= 4 ? "Corto"
                                : longitudes[i] <= 7 ? "Medio"
                                : "Largo";

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Nombre: {nombres[i]} | Longitud: {longitudes[i]} | Clasificación: {clasificacion}");
            Console.ResetColor();
        }
    }


    /// Muestra el menú principal y permite al usuario iniciar o salir del programa
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
            MostrarMensaje(" Opción no válida. Se correra el programa nuevamente.", ConsoleColor.Red);
            pausa();
            return MostrarMenuInicio();
        }
    }


    /// Pregunta al usuario si desea continuar o salir del programa
    static bool DeseaContinuar()
    {
        while (true)
        {
            Console.WriteLine("\n¿Deseas continuar? 1.Si/2.No");
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

    /// Muestra un mensaje en consola con el color indicado
    static void MostrarMensaje(string mensaje, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(mensaje);
        Console.ResetColor();
    }
    static void pausa()
    {
        Console.WriteLine("Presiona Enter para continuar...");
        Console.ReadLine();

    }   
}