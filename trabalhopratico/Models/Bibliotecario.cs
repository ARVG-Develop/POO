using System;
using System.Collections.Generic;

namespace SistemaBiblioteca.Models
{
    // NÍVEL 4 da hierarquia
    public class Bibliotecario : Funcionario
    {
        public string Crb { get; set; }  
        public bool ChefeDivisao { get; set; }
        private List<string> _especialidades;

        public IReadOnlyList<string> Especialidades => _especialidades.AsReadOnly();

        public Bibliotecario() 
        {
            _especialidades = new List<string>();
        }

        public Bibliotecario(int id, string nome, string cpf, DateTime dataNascimento,
                             string email, string telefone,
                             string matricula, decimal salario, DateTime dataAdmissao,
                             string crb, bool chefeDivisao = false)
            : base(id, nome, cpf, dataNascimento, email, telefone,
                   matricula, "Bibliotecário", salario, dataAdmissao)
        {
            Crb = crb;
            ChefeDivisao = chefeDivisao;
            _especialidades = new List<string>();
        }

        public void AdicionarEspecialidade(string especialidade)
        {
            if (string.IsNullOrWhiteSpace(especialidade))
                throw new ArgumentException("Especialidade não pode ser vazia.");
            if (!_especialidades.Contains(especialidade))
                _especialidades.Add(especialidade);
        }

        public override string ObterTipo() => ChefeDivisao ? "Bibliotecário-Chefe" : "Bibliotecário";

        public override string ObterResumo()
        {
            string chefe = ChefeDivisao ? " [CHEFE]" : "";
            return $"{base.ObterResumo()} | CRB: {Crb}{chefe}";
        }

        public override string ObterPermissoes()
        {
            string base_ = base.ObterPermissoes();
            string extras = "Gerenciar acervo, Administrar funcionários, Emitir relatórios, Cancelar empréstimos";
            if (ChefeDivisao)
                extras += ", Configurar sistema, Acesso total";
            return $"{base_}, {extras}";
        }
    }
}