using System;
using SistemaBiblioteca.Menus;

namespace SistemaBiblioteca
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "BiblioSys - Sistema de Gerenciamento de Biblioteca";

            try
            {
                var menu = new MenuPrincipal();
                menu.Iniciar();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  ERRO CRÍTICO: {ex.Message}");
                Console.WriteLine($"  Detalhes: {ex.StackTrace}");
                Console.ResetColor();
                Console.WriteLine("\n  Pressione qualquer tecla para sair...");
                Console.ReadKey();
            }
        }
    }
}