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

        // Lê uma data pedindo dia, mês e ano separadamente
        public static DateTime LerData(string mensagem)
        {
            Console.WriteLine(mensagem);
            while (true)
            {
                int dia = LerInteiro("    Dia  : ", 1, 31);
                int mes = LerInteiro("    Mes  : ", 1, 12);
                int ano = LerInteiro("    Ano  : ", 1900, DateTime.Now.Year);
                try
                {
                    return new DateTime(ano, mes, dia);
                }
                catch
                {
                    Console.WriteLine("  [!] Data invalida. Verifique o dia, mes e ano informados.");
                }
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
            Console.WriteLine($"\n  [OK] {mensagem}");
        }

        public static void ExibirErro(string mensagem)
        {
            Console.WriteLine($"\n  [ERRO] {mensagem}");
        }

        public static void ExibirAviso(string mensagem)
        {
            Console.WriteLine($"\n  [AVISO] {mensagem}");
        }

        public static void ExibirInfo(string mensagem)
        {
            Console.WriteLine($"  [INFO] {mensagem}");
        }
    }
}
