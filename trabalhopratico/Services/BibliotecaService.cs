using System;
using System.Collections.Generic;
using SistemaBiblioteca.Models;
using SistemaBiblioteca.Repositories;

namespace SistemaBiblioteca.Services
{
    public class BibliotecaService
    {
        private readonly AlunoRepository _alunoRepo;
        private readonly LivroRepository _livroRepo;
        private readonly EmprestimoRepository _emprestimoRepo;

        public BibliotecaService()
        {
            _alunoRepo = new AlunoRepository();
            _livroRepo = new LivroRepository();
            _emprestimoRepo = new EmprestimoRepository();
        }

        // ==================== ALUNOS ====================
        public void CadastrarAluno(Aluno aluno)
        {
            if (aluno == null) throw new ArgumentNullException(nameof(aluno));
            _alunoRepo.Adicionar(aluno);
        }

        public Aluno BuscarAluno(int id) => _alunoRepo.BuscarPorId(id);

        public List<Aluno> ListarAlunos() => _alunoRepo.BuscarTodos();

        public List<Aluno> BuscarAlunosPorNome(string nome) => _alunoRepo.BuscarPorNome(nome);

        public void AtualizarAluno(Aluno aluno) => _alunoRepo.Atualizar(aluno);

        public void RemoverAluno(int id) => _alunoRepo.Remover(id);

        public int ProximoIdAluno() => _alunoRepo.ProximoId();

        // ==================== LIVROS ====================
        public void CadastrarLivro(Livro livro)
        {
            if (livro == null) throw new ArgumentNullException(nameof(livro));
            _livroRepo.Adicionar(livro);
        }

        public Livro BuscarLivro(int id) => _livroRepo.BuscarPorId(id);

        public List<Livro> ListarLivros() => _livroRepo.BuscarTodos();

        public List<Livro> ListarLivrosDisponiveis() => _livroRepo.BuscarDisponiveis();

        public List<Livro> BuscarLivrosPorTitulo(string titulo) => _livroRepo.BuscarPorTitulo(titulo);

        public List<Livro> BuscarLivrosPorAutor(string autor) => _livroRepo.BuscarPorAutor(autor);

        public void AtualizarLivro(Livro livro) => _livroRepo.Atualizar(livro);

        public void RemoverLivro(int id) => _livroRepo.Remover(id);

        public int ProximoIdLivro() => _livroRepo.ProximoId();

        // ==================== EMPRÉSTIMOS ====================
        public Emprestimo RealizarEmprestimo(int alunoId, int livroId)
        {
            var aluno = _alunoRepo.BuscarPorId(alunoId);
            var livro = _livroRepo.BuscarPorId(livroId);

            if (!aluno.Ativo)
                throw new InvalidOperationException($"Aluno '{aluno.Nome}' está inativo e não pode realizar empréstimos.");

            if (!aluno.PodePegarEmprestimo)
                throw new InvalidOperationException(
                    $"Aluno '{aluno.Nome}' atingiu o limite de {Aluno.MaxEmprestimos} empréstimos simultâneos.");

            if (_emprestimoRepo.AlunoTemEmprestimoAtivo(alunoId, livroId))
                throw new InvalidOperationException(
                    $"Aluno '{aluno.Nome}' já possui este livro emprestado.");

            if (!livro.RealizarEmprestimo())
                throw new InvalidOperationException(
                    $"Livro '{livro.Titulo}' não possui exemplares disponíveis.");

            aluno.EmprestimosAtivos++;
            _alunoRepo.Atualizar(aluno);
            _livroRepo.Atualizar(livro);

            var emprestimo = new Emprestimo(0, alunoId, aluno.Nome, livroId, livro.Titulo);
            _emprestimoRepo.Adicionar(emprestimo);

            return emprestimo;
        }

        public void DevolverEmprestimo(int emprestimoId)
        {
            var emprestimo = _emprestimoRepo.BuscarPorId(emprestimoId);

            if (emprestimo.Status == StatusEmprestimo.Devolvido)
                throw new InvalidOperationException("Este empréstimo já foi devolvido.");

            var aluno = _alunoRepo.BuscarPorId(emprestimo.AlunoId);
            var livro = _livroRepo.BuscarPorId(emprestimo.LivroId);

            emprestimo.Devolver();
            livro.RealizarDevolucao();

            if (aluno.EmprestimosAtivos > 0)
                aluno.EmprestimosAtivos--;

            _emprestimoRepo.Atualizar(emprestimo);
            _livroRepo.Atualizar(livro);
            _alunoRepo.Atualizar(aluno);
        }

        public bool RenovarEmprestimo(int emprestimoId)
        {
            var emprestimo = _emprestimoRepo.BuscarPorId(emprestimoId);
            bool renovado = emprestimo.Renovar();
            if (renovado)
                _emprestimoRepo.Atualizar(emprestimo);
            return renovado;
        }

        public List<Emprestimo> ListarEmprestimos() => _emprestimoRepo.BuscarTodos();

        public List<Emprestimo> ListarEmprestimosAtivos() => _emprestimoRepo.BuscarAtivos();

        public List<Emprestimo> ListarEmprestimosAluno(int alunoId) => _emprestimoRepo.BuscarPorAluno(alunoId);

        public List<Emprestimo> ListarEmprestimosAtrasados() => _emprestimoRepo.BuscarAtrasados();

        public Emprestimo BuscarEmprestimo(int id) => _emprestimoRepo.BuscarPorId(id);

        // ==================== RELATÓRIOS POLIMÓRFICOS ====================
        // Demonstra polimorfismo: aceita qualquer ItemBiblioteca e exibe detalhes
        public void ExibirDetalhesItem(ItemBiblioteca item)
        {
            Console.WriteLine($"\n  Categoria: {item.ObterCategoria()}");
            Console.WriteLine($"  {item.ObterDetalhes()}");
        }

        // Demonstra polimorfismo: aceita qualquer Pessoa e exibe resumo
        public void ExibirResumoPessoa(Pessoa pessoa)
        {
            Console.WriteLine($"\n  {pessoa.ObterResumo()}");
        }

        // Demonstra polimorfismo: exibe permissões de qualquer funcionário
        public void ExibirPermissoesFuncionario(Funcionario funcionario)
        {
            Console.WriteLine($"\n  Funcionário: {funcionario.Nome}");
            Console.WriteLine($"  {funcionario.ObterPermissoes()}");
        }
    }
}