using System;

namespace SistemaBiblioteca.Models
{
    // Classe base para todas as pessoas do sistema (alunos e funcionários)
    public abstract class Pessoa
    {
        private int _id;
        private string _nome;
        private string _cpf;
        private DateTime _dataNascimento;

        public int Id
        {
            get => _id;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("ID deve ser maior que zero.");
                _id = value;
            }
        }

        public string Nome
        {
            get => _nome;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nome não pode ser vazio.");
                if (value.Length < 3)
                    throw new ArgumentException("Nome deve ter ao menos 3 caracteres.");
                _nome = value.Trim();
            }
        }

        public string Cpf
        {
            get => _cpf;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("CPF não pode ser vazio.");
                string cpfLimpo = value.Replace(".", "").Replace("-", "").Trim();
                if (cpfLimpo.Length != 11)
                    throw new ArgumentException("CPF deve conter 11 dígitos.");
                _cpf = cpfLimpo;
            }
        }

        public DateTime DataNascimento
        {
            get => _dataNascimento;
            set
            {
                if (value >= DateTime.Now)
                    throw new ArgumentException("Data de nascimento deve ser no passado.");
                if (value < new DateTime(1900, 1, 1))
                    throw new ArgumentException("Data de nascimento inválida.");
                _dataNascimento = value;
            }
        }

        public string CpfFormatado => $"{Cpf.Substring(0, 3)}.{Cpf.Substring(3, 3)}.{Cpf.Substring(6, 3)}-{Cpf.Substring(9, 2)}";

        public int Idade => DateTime.Now.Year - DataNascimento.Year -
                            (DateTime.Now.DayOfYear < DataNascimento.DayOfYear ? 1 : 0);

        protected Pessoa() { }

        protected Pessoa(int id, string nome, string cpf, DateTime dataNascimento)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }

        public abstract string ObterTipo();

        public virtual string ObterResumo()
        {
            return $"[{ObterTipo()}] ID: {Id} | {Nome} | CPF: {CpfFormatado} | Idade: {Idade} anos";
        }

        public override string ToString() => ObterResumo();
    }
}