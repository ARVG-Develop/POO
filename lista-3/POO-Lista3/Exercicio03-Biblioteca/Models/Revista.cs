namespace Exercicio03_Biblioteca;

public class Revista : ItemBiblioteca, IEmprestimo
{
    public Revista(string titulo, int ano) : base(titulo, ano)
    {
    }

    public override void ExibirDetalhes()
    {
        Console.WriteLine($"Revista: {Titulo}, Ano: {Ano}");
    }

    public void Emprestar()
    {
        Console.WriteLine($"A revista \"{Titulo}\" foi emprestada.");
    }
}
