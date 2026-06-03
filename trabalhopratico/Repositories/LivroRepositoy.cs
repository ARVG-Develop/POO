using System;
using System.Collections.Generic;
using System.Linq;
using SistemaBiblioteca.Interfaces;
using SistemaBiblioteca.Models;

namespace SistemaBiblioteca.Repositories
{
    public class LivroRepository : IRepository<Livro>
    {
        private readonly List<Livro> _livros;
        private int _proximoId;

        public LivroRepository()
        {
            _livros = new List<Livro>();
            _proximoId = 1;
            SeedDados();
        }

        private void SeedDados()
        {
            _livros.AddRange(new[]
            {
                new Livro(GerarId(), "Clean Code", "Robert C. Martin", "Alta Books",
                          "9788576082675", 2008, "Programação", 431, 3),
                new Livro(GerarId(), "O Algoritmo da Vida", "Brian Christian", "Intrínseca",
                          "9788551000000", 2016, "Ciência", 368, 2),
                new Livro(GerarId(), "Design Patterns", "Gang of Four", "Bookman",
                          "9780201633610", 1994, "Programação", 395, 2),
                new Livro(GerarId(), "Estruturas de Dados e Algoritmos em Java", "Robert Lafore", "Ciência Moderna",
                          "9788573935653", 2004, "Programação", 800, 4),
                new Livro(GerarId(), "Inteligência Artificial", "Stuart Russell", "Elsevier",
                          "9788535237016", 2013, "IA", 988, 2),
            });
        }

        private int GerarId() => _proximoId++;

        public void Adicionar(Livro livro)
        {
            if (livro == null) throw new ArgumentNullException(nameof(livro));
            if (_livros.Any(l => l.Isbn == livro.Isbn))
                throw new InvalidOperationException($"Já existe um livro com o ISBN {livro.Isbn}.");

            livro.Id = GerarId();
            _livros.Add(livro);
        }

        public Livro BuscarPorId(int id)
        {
            var livro = _livros.FirstOrDefault(l => l.Id == id);
            if (livro == null)
                throw new KeyNotFoundException($"Livro com ID {id} não encontrado.");
            return livro;
        }

        public List<Livro> BuscarTodos() => new List<Livro>(_livros);

        public List<Livro> BuscarDisponiveis() => _livros.Where(l => l.QuantidadeDisponivel > 0).ToList();

        public List<Livro> BuscarPorTitulo(string titulo) =>
            _livros.Where(l => l.Titulo.Contains(titulo, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Livro> BuscarPorAutor(string autor) =>
            _livros.Where(l => l.Autor.Contains(autor, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Livro> BuscarPorGenero(string genero) =>
            _livros.Where(l => l.Genero.Contains(genero, StringComparison.OrdinalIgnoreCase)).ToList();

        public void Atualizar(Livro livro)
        {
            if (livro == null) throw new ArgumentNullException(nameof(livro));
            var index = _livros.FindIndex(l => l.Id == livro.Id);
            if (index == -1)
                throw new KeyNotFoundException($"Livro com ID {livro.Id} não encontrado.");
            _livros[index] = livro;
        }

        public void Remover(int id)
        {
            var livro = BuscarPorId(id);
            if (livro.QuantidadeDisponivel < livro.QuantidadeTotal)
                throw new InvalidOperationException("Não é possível remover livro com exemplares emprestados.");
            _livros.Remove(livro);
        }

        public bool Existe(int id) => _livros.Any(l => l.Id == id);

        public int ProximoId() => _proximoId;
    }
}