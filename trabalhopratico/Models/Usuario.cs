using System;

namespace SistemaBiblioteca.Models
{
    // NÍVEL 2 da hierarquia
    public class Usuario : Pessoa
    {
        private string _email;
        private string _telefone;
        private DateTime _dataCadastro;

        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Email não pode ser vazio.");
                if (!value.Contains("@") || !value.Contains("."))
                    throw new ArgumentException("Email inválido.");
                _email = value.Trim().ToLower();
            }
        }

        public string Telefone
        {
            get => _telefone;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Telefone não pode ser vazio.");
                string tel = value.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                if (tel.Length < 10 || tel.Length > 11)
                    throw new ArgumentException("Telefone deve ter 10 ou 11 dígitos.");
                _telefone = tel;
            }
        }

        public DateTime DataCadastro
        {
            get => _dataCadastro;
            private set => _dataCadastro = value;
        }

        public bool Ativo { get; set; }

        public string TelefoneFormatado
        {
            get
            {
                if (Telefone.Length == 11)
                    return $"({Telefone.Substring(0, 2)}) {Telefone.Substring(2, 5)}-{Telefone.Substring(7)}";
                return $"({Telefone.Substring(0, 2)}) {Telefone.Substring(2, 4)}-{Telefone.Substring(6)}";
            }
        }

        public Usuario() { }

        public Usuario(int id, string nome, string cpf, DateTime dataNascimento, string email, string telefone)
            : base(id, nome, cpf, dataNascimento)
        {
            Email = email;
            Telefone = telefone;
            DataCadastro = DateTime.Now;
            Ativo = true;
        }

        public override string ObterTipo() => "Usuário";

        public override string ObterResumo()
        {
            string status = Ativo ? "Ativo" : "Inativo";
            return $"{base.ObterResumo()} | Email: {Email} | Tel: {TelefoneFormatado} | Status: {status}";
        }
    }
}