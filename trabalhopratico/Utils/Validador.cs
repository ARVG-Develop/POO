using System;
using System.Text.RegularExpressions;

namespace SistemaBiblioteca.Utils
{
    public static class Validador
    {
        public static int LerInteiroPositivo(string mensagem)
        {
            while (true)
            {
                Console.Write(mensagem);
                string entrada = Console.ReadLine();
                if (int.TryParse(entrada, out int valor) && valor > 0)
                    return valor;
                Console.WriteLine("  [!] Digite um número inteiro positivo válido.");
            }
        }

        public static int LerInteiro(string mensagem, int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                Console.Write(mensagem);
                string entrada = Console.ReadLine();
                if (int.TryParse(entrada, out int valor) && valor >= min && valor <= max)
                    return valor;
                Console.WriteLine($"  [!] Digite um número entre {min} e {max}.");
            }
        }

        public static decimal LerDecimal(string mensagem)
        {
            while (true)
            {
                Console.Write(mensagem);
                string entrada = Console.ReadLine();
                if (decimal.TryParse(entrada, out decimal valor) && valor >= 0)
                    return valor;
                Console.WriteLine("  [!] Digite um valor decimal válido (ex: 1500,00).");
            }
        }

        public static string LerStringObrigatoria(string mensagem, int minLen = 1)
        {
            while (true)
            {
                Console.Write(mensagem);
                string entrada = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(entrada) && entrada.Length >= minLen)
                    return entrada;
                Console.WriteLine($"  [!] Campo obrigatório. Mínimo {minLen} caractere(s).");
            }
        }

        public static DateTime LerData(string mensagem)
        {
            while (true)
            {
                Console.Write(mensagem);
                string entrada = Console.ReadLine();
                if (DateTime.TryParseExact(entrada, "dd/MM/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime data))
                    return data;
                Console.WriteLine("  [!] Data inválida. Use o formato dd/MM/yyyy.");
            }
        }

        public static string LerEmail(string mensagem)
        {
            while (true)
            {
                Console.Write(mensagem);
                string entrada = Console.ReadLine()?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(entrada) &&
                    entrada.Contains("@") && entrada.Contains("."))
                    return entrada;
                Console.WriteLine("  [!] Email inválido.");
            }
        }

        public static string LerCpf(string mensagem)
        {
            while (true)
            {
                Console.Write(mensagem);
                string entrada = Console.ReadLine()?.Replace(".", "").Replace("-", "").Trim();
                if (!string.IsNullOrWhiteSpace(entrada) && entrada.Length == 11 &&
                    long.TryParse(entrada, out _))
                    return entrada;
                Console.WriteLine("  [!] CPF inválido. Digite 11 dígitos numéricos.");
            }
        }

        public static bool ConfirmarOperacao(string mensagem)
        {
            Console.Write($"{mensagem} (S/N): ");
            string resposta = Console.ReadLine()?.Trim().ToUpper();
            return resposta == "S";
        }

        public static void PausarTela()
        {
            Console.WriteLine("\n  Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public static void ExibirSucesso(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  [✓] {mensagem}");
            Console.ResetColor();
        }

        public static void ExibirErro(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  [✗] ERRO: {mensagem}");
            Console.ResetColor();
        }

        public static void ExibirAviso(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  [!] AVISO: {mensagem}");
            Console.ResetColor();
        }

        public static void ExibirInfo(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"  [i] {mensagem}");
            Console.ResetColor();
        }
    }
}