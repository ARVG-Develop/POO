using System;
using System.Collections.Generic;
using System.Linq;
using SistemaBiblioteca.Interfaces;
using SistemaBiblioteca.Models;

namespace SistemaBiblioteca.Repositories
{
    public class AlunoRepository : IRepository<Aluno>
    {
        private readonly List<Aluno> _alunos;
        private int _proximoId;

        public AlunoRepository()
        {
            _alunos = new List<Aluno>();
            _proximoId = 1;
            SeedDados();
        }

        private void SeedDados()
        {
            _alunos.AddRange(new[]
            {
                new Aluno(GerarId(), "Ana Clara Souza", "12345678901", new DateTime(2002, 5, 14),
                          "CC2022001", "Ciência da Computação", "ana@email.com", "31987654321", 2022),
                new Aluno(GerarId(), "Bruno Lima", "98765432100", new DateTime(2001, 8, 22),
                          "SI2021002", "Sistemas de Informação", "bruno@email.com", "31912345678", 2021),
                new Aluno(GerarId(), "Carla Mendes", "45612378900", new DateTime(2003, 3, 10),
                          "EC2023003", "Engenharia de Computação", "carla@email.com", "31998887766", 2023),
            });
        }

        private int GerarId() => _proximoId++;

        public void Adicionar(Aluno aluno)
        {
            if (aluno == null) throw new ArgumentNullException(nameof(aluno));
            if (_alunos.Any(a => a.Cpf == aluno.Cpf))
                throw new InvalidOperationException($"Já existe um aluno com o CPF {aluno.CpfFormatado}.");
            if (_alunos.Any(a => a.Matricula == aluno.Matricula))
                throw new InvalidOperationException($"Já existe um aluno com a matrícula {aluno.Matricula}.");

            aluno.Id = GerarId();
            _alunos.Add(aluno);
        }

        public Aluno BuscarPorId(int id)
        {
            var aluno = _alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null)
                throw new KeyNotFoundException($"Aluno com ID {id} não encontrado.");
            return aluno;
        }

        public List<Aluno> BuscarTodos() => new List<Aluno>(_alunos);

        public List<Aluno> BuscarPorNome(string nome) =>
            _alunos.Where(a => a.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Aluno> BuscarPorCurso(string curso) =>
            _alunos.Where(a => a.Curso.Contains(curso, StringComparison.OrdinalIgnoreCase)).ToList();

        public Aluno BuscarPorMatricula(string matricula) =>
            _alunos.FirstOrDefault(a => a.Matricula.Equals(matricula, StringComparison.OrdinalIgnoreCase));

        public void Atualizar(Aluno aluno)
        {
            if (aluno == null) throw new ArgumentNullException(nameof(aluno));
            var index = _alunos.FindIndex(a => a.Id == aluno.Id);
            if (index == -1)
                throw new KeyNotFoundException($"Aluno com ID {aluno.Id} não encontrado.");
            _alunos[index] = aluno;
        }

        public void Remover(int id)
        {
            var aluno = BuscarPorId(id);
            if (aluno.EmprestimosAtivos > 0)
                throw new InvalidOperationException($"Não é possível remover aluno com empréstimos ativos.");
            _alunos.Remove(aluno);
        }

        public bool Existe(int id) => _alunos.Any(a => a.Id == id);

        public int ProximoId() => _proximoId;
    }
}