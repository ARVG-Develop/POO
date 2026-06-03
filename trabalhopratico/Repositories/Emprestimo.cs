using System;
using System.Collections.Generic;
using System.Linq;
using SistemaBiblioteca.Interfaces;
using SistemaBiblioteca.Models;

namespace SistemaBiblioteca.Repositories
{
    public class EmprestimoRepository : IRepository<Emprestimo>
    {
        private readonly List<Emprestimo> _emprestimos;
        private int _proximoId;

        public EmprestimoRepository()
        {
            _emprestimos = new List<Emprestimo>();
            _proximoId = 1;
        }

        private int GerarId() => _proximoId++;

        public void Adicionar(Emprestimo emprestimo)
        {
            if (emprestimo == null) throw new ArgumentNullException(nameof(emprestimo));
            emprestimo.Id = GerarId();
            _emprestimos.Add(emprestimo);
        }

        public Emprestimo BuscarPorId(int id)
        {
            var emp = _emprestimos.FirstOrDefault(e => e.Id == id);
            if (emp == null)
                throw new KeyNotFoundException($"Empréstimo com ID {id} não encontrado.");
            return emp;
        }

        public List<Emprestimo> BuscarTodos()
        {
            foreach (var e in _emprestimos) e.AtualizarStatus();
            return new List<Emprestimo>(_emprestimos);
        }

        public List<Emprestimo> BuscarAtivos() =>
            _emprestimos.Where(e => e.Status == StatusEmprestimo.Ativo ||
                                    e.Status == StatusEmprestimo.Renovado ||
                                    e.Status == StatusEmprestimo.Atrasado).ToList();

        public List<Emprestimo> BuscarPorAluno(int alunoId) =>
            _emprestimos.Where(e => e.AlunoId == alunoId).ToList();

        public List<Emprestimo> BuscarPorLivro(int livroId) =>
            _emprestimos.Where(e => e.LivroId == livroId).ToList();

        public List<Emprestimo> BuscarAtrasados()
        {
            var lista = _emprestimos.Where(e =>
                e.Status == StatusEmprestimo.Ativo || e.Status == StatusEmprestimo.Renovado).ToList();
            foreach (var e in lista) e.AtualizarStatus();
            return _emprestimos.Where(e => e.Atrasado).ToList();
        }

        public void Atualizar(Emprestimo emprestimo)
        {
            if (emprestimo == null) throw new ArgumentNullException(nameof(emprestimo));
            var index = _emprestimos.FindIndex(e => e.Id == emprestimo.Id);
            if (index == -1)
                throw new KeyNotFoundException($"Empréstimo com ID {emprestimo.Id} não encontrado.");
            _emprestimos[index] = emprestimo;
        }

        public void Remover(int id)
        {
            var emp = BuscarPorId(id);
            if (emp.Status == StatusEmprestimo.Ativo || emp.Status == StatusEmprestimo.Renovado)
                throw new InvalidOperationException("Não é possível remover empréstimo ativo.");
            _emprestimos.Remove(emp);
        }

        public bool Existe(int id) => _emprestimos.Any(e => e.Id == id);

        public bool AlunoTemEmprestimoAtivo(int alunoId, int livroId) =>
            _emprestimos.Any(e => e.AlunoId == alunoId && e.LivroId == livroId &&
                                  (e.Status == StatusEmprestimo.Ativo || e.Status == StatusEmprestimo.Renovado));

        public int ProximoId() => _proximoId;
    }
}