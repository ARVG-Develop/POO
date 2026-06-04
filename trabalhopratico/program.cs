using System;
using SistemaBiblioteca.Menus;

namespace SistemaBiblioteca
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var menu = new MenuPrincipal();
                menu.Iniciar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n  ERRO: {ex.Message}");
                Console.WriteLine($"  Detalhes: {ex.StackTrace}");
                Console.WriteLine("\n  Pressione Enter para sair...");
                Console.ReadLine();
            }
        }
    }
}
