namespace Exercicio03_Biblioteca;

public abstract class ItemBiblioteca
{
    public string Titulo { get; set; }
    public int Ano { get; set; }

    public ItemBiblioteca(string titulo, int ano)
    {
        Titulo = titulo;
        Ano = ano;
    }

    public abstract void ExibirDetalhes();
}
