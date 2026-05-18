namespace Exercicio03_Biblioteca;

public class Livro : ItemBiblioteca, IEmprestimo
{
    public Livro(string titulo, int ano) : base(titulo, ano)
    {
    }

    public override void ExibirDetalhes()
    {
        Console.WriteLine("Estou no método ExibirDetalhes da classe Livro");
    }

    public void Emprestar()
    {
        Console.WriteLine("Estou no método Emprestar da classe Livro");
    }
}
