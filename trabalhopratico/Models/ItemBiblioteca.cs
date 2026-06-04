using System;

namespace SistemaBiblioteca.Models
{
    // Classe base para qualquer item que possa ser emprestado na biblioteca
    public abstract class ItemBiblioteca
    {
        private int _id;
        private string _titulo;
        private int _anoPublicacao;

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

        public string Titulo
        {
            get => _titulo;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Título não pode ser vazio.");
                _titulo = value.Trim();
            }
        }

        public int AnoPublicacao
        {
            get => _anoPublicacao;
            set
            {
                if (value < 1400 || value > DateTime.Now.Year)
                    throw new ArgumentException("Ano de publicação inválido.");
                _anoPublicacao = value;
            }
        }

        public bool Disponivel { get; set; }
        public int QuantidadeTotal { get; set; }
        public int QuantidadeDisponivel { get; set; }

        protected ItemBiblioteca() { }

        protected ItemBiblioteca(int id, string titulo, int anoPublicacao, int quantidade)
        {
            Id = id;
            Titulo = titulo;
            AnoPublicacao = anoPublicacao;
            QuantidadeTotal = quantidade;
            QuantidadeDisponivel = quantidade;
            Disponivel = quantidade > 0;
        }

        public abstract string ObterCategoria();
        public abstract string ObterDetalhes();

        public bool RealizarEmprestimo()
        {
            if (QuantidadeDisponivel <= 0)
                return false;
            QuantidadeDisponivel--;
            Disponivel = QuantidadeDisponivel > 0;
            return true;
        }

        public void RealizarDevolucao()
        {
            if (QuantidadeDisponivel < QuantidadeTotal)
            {
                QuantidadeDisponivel++;
                Disponivel = true;
            }
        }

        public override string ToString() =>
            $"[{ObterCategoria()}] ID: {Id} | {Titulo} ({AnoPublicacao}) | Disponível: {QuantidadeDisponivel}/{QuantidadeTotal}";
    }
}