using System;

namespace SistemaBiblioteca.Models
{
    // Aluno herda de Pessoa diretamente, pois não é um usuário do sistema administrativo
    public class Aluno : Pessoa
    {
        private string _matricula;
        private int _anoIngresso;

        public string Matricula
        {
            get => _matricula;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Matrícula não pode ser vazia.");
                _matricula = value.Trim().ToUpper();
            }
        }

        public string Curso { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public int AnoIngresso
        {
            get => _anoIngresso;
            set
            {
                if (value < 2000 || value > DateTime.Now.Year)
                    throw new ArgumentException($"Ano de ingresso deve ser entre 2000 e {DateTime.Now.Year}.");
                _anoIngresso = value;
            }
        }

        public bool Ativo { get; set; }
        public int EmprestimosAtivos { get; set; }
        public const int MaxEmprestimos = 3;

        public bool PodePegarEmprestimo => Ativo && EmprestimosAtivos < MaxEmprestimos;

        public Aluno() { }

        public Aluno(int id, string nome, string cpf, DateTime dataNascimento,
                     string matricula, string curso, string email, string telefone, int anoIngresso)
            : base(id, nome, cpf, dataNascimento)
        {
            Matricula = matricula;
            Curso = curso;
            Email = email;
            Telefone = telefone;
            AnoIngresso = anoIngresso;
            Ativo = true;
            EmprestimosAtivos = 0;
        }

        public override string ObterTipo() => "Aluno";

        public override string ObterResumo()
        {
            string status = Ativo ? "Ativo" : "Inativo";
            string emprestimos = $"{EmprestimosAtivos}/{MaxEmprestimos}";
            return $"{base.ObterResumo()} | Matrícula: {Matricula} | Curso: {Curso} | Empréstimos: {emprestimos} | Status: {status}";
        }
    }
}