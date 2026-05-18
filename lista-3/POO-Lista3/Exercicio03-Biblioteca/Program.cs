using Exercicio03_Biblioteca;

Livro livro = new Livro("O Senhor dos Anéis", 1954);
Revista revista = new Revista("Super Interessante", 2023);

List<ItemBiblioteca> acervo = new List<ItemBiblioteca>();
acervo.Add(livro);
acervo.Add(revista);

Console.WriteLine("=== BIBLIOTECA ===\n");

foreach (ItemBiblioteca item in acervo)
{
    item.ExibirDetalhes();

    if (item is IEmprestimo emprestavel)
    {
        emprestavel.Emprestar();
    }

    Console.WriteLine();
}
