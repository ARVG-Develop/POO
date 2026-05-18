namespace Exercicio03_Biblioteca;

public class Revista : ItemBiblioteca, IEmprestimo
{
    public Revista(string titulo, int ano) : base(titulo, ano)
    {
    }

    public override void ExibirDetalhes()
    {
        Console.WriteLine("Estou no método ExibirDetalhes da classe Revista");
    }

    public void Emprestar()
    {
        Console.WriteLine("Estou no método Emprestar da classe Revista");
    }
}
