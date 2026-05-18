namespace Exercicio03_Biblioteca;

public class Livro : ItemBiblioteca, IEmprestimo
{
    public Livro(string titulo, int ano) : base(titulo, ano)
    {
    }

    public override void ExibirDetalhes()
    {
        Console.WriteLine($"Livro: {Titulo}, Ano: {Ano}");
    }

    public void Emprestar()
    {
        Console.WriteLine($"O livro \"{Titulo}\" foi emprestado.");
    }
}
