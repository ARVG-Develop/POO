using System;

namespace SistemaBiblioteca.Models
{
    // Funcionário herda de Usuario, adicionando dados profissionais
    public class Funcionario : Usuario
    {
        private string _matricula;
        private decimal _salario;

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

        public string Cargo { get; set; }

        public decimal Salario
        {
            get => _salario;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Salário não pode ser negativo.");
                _salario = value;
            }
        }

        public DateTime DataAdmissao { get; set; }

        public int AnosServico => DateTime.Now.Year - DataAdmissao.Year -
                                  (DateTime.Now.DayOfYear < DataAdmissao.DayOfYear ? 1 : 0);

        public Funcionario() { }

        public Funcionario(int id, string nome, string cpf, DateTime dataNascimento,
                           string email, string telefone,
                           string matricula, string cargo, decimal salario, DateTime dataAdmissao)
            : base(id, nome, cpf, dataNascimento, email, telefone)
        {
            Matricula = matricula;
            Cargo = cargo;
            Salario = salario;
            DataAdmissao = dataAdmissao;
        }

        public override string ObterTipo() => "Funcionário";

        public override string ObterResumo()
        {
            return $"{base.ObterResumo()} | Matrícula: {Matricula} | Cargo: {Cargo} | Admissão: {DataAdmissao:dd/MM/yyyy}";
        }

        public virtual string ObterPermissoes()
        {
            return "Permissões: Consultar acervo, Registrar empréstimos, Cadastrar alunos";
        }
    }
}