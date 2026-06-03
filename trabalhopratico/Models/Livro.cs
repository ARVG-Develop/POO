using System;

namespace SistemaBiblioteca.Models
{
    public class Livro : ItemBiblioteca
    {
        private string _isbn;

        public string Autor { get; set; }
        public string Editora { get; set; }
        public string Genero { get; set; }
        public int NumeroPaginas { get; set; }

        public string Isbn
        {
            get => _isbn;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ISBN não pode ser vazio.");
                string isbn = value.Replace("-", "").Replace(" ", "");
                if (isbn.Length != 10 && isbn.Length != 13)
                    throw new ArgumentException("ISBN deve ter 10 ou 13 dígitos.");
                _isbn = isbn;
            }
        }

        public Livro() { }

        public Livro(int id, string titulo, string autor, string editora,
                     string isbn, int anoPublicacao, string genero,
                     int numeroPaginas, int quantidade)
            : base(id, titulo, anoPublicacao, quantidade)
        {
            Autor = autor;
            Editora = editora;
            Isbn = isbn;
            Genero = genero;
            NumeroPaginas = numeroPaginas;
        }

        public override string ObterCategoria() => "Livro";

        public override string ObterDetalhes()
        {
            return $"Título    : {Titulo}\n" +
                   $"Autor     : {Autor}\n" +
                   $"Editora   : {Editora}\n" +
                   $"ISBN      : {Isbn}\n" +
                   $"Gênero    : {Genero}\n" +
                   $"Páginas   : {NumeroPaginas}\n" +
                   $"Ano       : {AnoPublicacao}\n" +
                   $"Disponível: {QuantidadeDisponivel}/{QuantidadeTotal} exemplares";
        }
    }
}