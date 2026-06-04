using System;

namespace SistemaBiblioteca.Models
{
    public enum StatusEmprestimo
    {
        Ativo,
        Devolvido,
        Atrasado,
        Renovado
    }

    public class Emprestimo
    {
        private static readonly int DiasEmprestimoPadrao = 14;

        public int Id { get; set; }
        public int AlunoId { get; set; }
        public string AlunoNome { get; set; }
        public int LivroId { get; set; }
        public string LivroTitulo { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataPrevistaDevolucao { get; set; }
        public DateTime? DataDevolucaoReal { get; set; }
        public StatusEmprestimo Status { get; set; }
        public int NumeroRenovacoes { get; set; }
        public const int MaxRenovacoes = 2;

        public bool Atrasado => Status == StatusEmprestimo.Ativo &&
                                DateTime.Now.Date > DataPrevistaDevolucao.Date;

        public int DiasAtraso => Atrasado
            ? (DateTime.Now.Date - DataPrevistaDevolucao.Date).Days
            : 0;

        public decimal MultaPorAtraso => DiasAtraso * 0.50m; 

        public Emprestimo() { }

        public Emprestimo(int id, int alunoId, string alunoNome, int livroId, string livroTitulo)
        {
            if (id <= 0) throw new ArgumentException("ID deve ser maior que zero.");
            if (alunoId <= 0) throw new ArgumentException("ID do aluno inválido.");
            if (livroId <= 0) throw new ArgumentException("ID do livro inválido.");

            Id = id;
            AlunoId = alunoId;
            AlunoNome = alunoNome;
            LivroId = livroId;
            LivroTitulo = livroTitulo;
            DataEmprestimo = DateTime.Now;
            DataPrevistaDevolucao = DateTime.Now.AddDays(DiasEmprestimoPadrao);
            Status = StatusEmprestimo.Ativo;
            NumeroRenovacoes = 0;
        }

        public bool Renovar()
        {
            if (NumeroRenovacoes >= MaxRenovacoes)
                return false;
            if (Status != StatusEmprestimo.Ativo)
                return false;

            DataPrevistaDevolucao = DataPrevistaDevolucao.AddDays(DiasEmprestimoPadrao);
            NumeroRenovacoes++;
            Status = StatusEmprestimo.Renovado;
            return true;
        }

        public void Devolver()
        {
            DataDevolucaoReal = DateTime.Now;
            Status = StatusEmprestimo.Devolvido;
        }

        public void AtualizarStatus()
        {
            if (Status == StatusEmprestimo.Ativo && Atrasado)
                Status = StatusEmprestimo.Atrasado;
        }

        public override string ToString()
        {
            string atraso = Atrasado ? $" *** ATRASADO {DiasAtraso} dia(s) - Multa: R${MultaPorAtraso:F2} ***" : "";
            return $"ID: {Id:D4} | Aluno: {AlunoNome} | Livro: {LivroTitulo} | " +
                   $"Empréstimo: {DataEmprestimo:dd/MM/yyyy} | Devolução: {DataPrevistaDevolucao:dd/MM/yyyy} | " +
                   $"Status: {Status}{atraso}";
        }
    }
}