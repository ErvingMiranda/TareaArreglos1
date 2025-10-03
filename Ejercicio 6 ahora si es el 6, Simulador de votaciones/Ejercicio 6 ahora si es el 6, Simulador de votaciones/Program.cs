/*Reto 6: Simulador de votaciones
Enunciado:
Un colegio realiza una elección estudiantil con 5 candidatos. Se registran 100 votos numéricos (entre 1 y 5).
Problema a resolver:
1.	Generar un arreglo con 100 votos (puede ser ingresado o aleatorio).
2.	Contar cuántos votos obtuvo cada candidato.
3.	Determinar:
•	Candidato ganador.
•	Si hay empate, mostrar quiénes empataron.
4.	Calcular porcentaje de votos de cada candidato.
5.	Validar que todos los votos sean válidos (entre 1 y 5), descartando inválidos si se da el caso.*/

using System;
class Program
{
    static void Main()
    {
        Random rand = new Random();
        int[] votos = new int[100];
        int[] conteoVotos = new int[5]; // Índices 0-4 para candidatos 1-5
        int votosInvalidos = 0;

        // Generar votos aleatorios entre 1 y 6 para simular posibles votos inválidos
        for (int i = 0; i < votos.Length; i++)
        {
            votos[i] = rand.Next(1, 8); // Genera un número entre 1 y 7 (para simular votos inválidos)
        }

        // Contar votos para cada candidato y validar votos
        foreach (int voto in votos)
        {
            if (voto >= 1 && voto <= 5)
            {
                conteoVotos[voto - 1]++;
            }
            else
            {
                votosInvalidos++;
            }
        }

        // Mostrar mensaje de error en rojo si hay votos inválidos
        if (votosInvalidos > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Se detectaron {votosInvalidos} votos inválidos y fueron descartados.");
            Console.ResetColor();
        }

        // Determinar el candidato ganador y si hay empate
        int maxVotos = 0;
        for (int i = 0; i < conteoVotos.Length; i++)
        {
            if (conteoVotos[i] > maxVotos)
            {
                maxVotos = conteoVotos[i];
            }
        }
        var ganadores = new System.Collections.Generic.List<int>();
        for (int i = 0; i < conteoVotos.Length; i++)
        {
            if (conteoVotos[i] == maxVotos)
            {
                ganadores.Add(i + 1); // +1 para ajustar al número del candidato
            }
        }

        // Mostrar resultados
        Console.WriteLine("Resultados de la votación:");
        int totalVotosValidos = 0;
        for (int i = 0; i < conteoVotos.Length; i++)
        {
            totalVotosValidos += conteoVotos[i];
        }
        for (int i = 0; i < conteoVotos.Length; i++)
        {
            double porcentaje = totalVotosValidos > 0 ? (double)conteoVotos[i] / totalVotosValidos * 100 : 0;
            Console.WriteLine($"Candidato {i + 1}: {conteoVotos[i]} votos ({porcentaje:F2}%)");
        }
        if (ganadores.Count == 1)
        {
            Console.WriteLine($"El candidato ganador es el Candidato {ganadores[0]} con {maxVotos} votos.");
        }
        else
        {
            Console.WriteLine("Hay un empate entre los siguientes candidatos: " + string.Join(", ", ganadores) + $" con {maxVotos} votos cada uno.");
        }
    }
}