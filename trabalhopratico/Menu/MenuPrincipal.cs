using System;
using System.Collections.Generic;
using SistemaBiblioteca.Models;
using SistemaBiblioteca.Utils;

namespace SistemaBiblioteca.Menus
{
    public class MenuPrincipal
    {
        // Listas que armazenam os dados em memória
        private readonly List<Aluno> _alunos;
        private readonly List<Livro> _livros;
        private readonly List<Emprestimo> _emprestimos;

        private int _proximoIdAluno;
        private int _proximoIdLivro;
        private int _proximoIdEmprestimo;

        private const string LINHA      = "  ====================================================";
        private const string LINHA_FINA = "  ----------------------------------------------------";

        public MenuPrincipal()
        {
            _alunos      = new List<Aluno>();
            _livros      = new List<Livro>();
            _emprestimos = new List<Emprestimo>();
            _proximoIdAluno      = 1;
            _proximoIdLivro      = 1;
            _proximoIdEmprestimo = 1;

            InicializarAlunos();
            InicializarLivros();
        }

        // Cadastra alguns alunos iniciais para facilitar os testes
        private void InicializarAlunos()
        {
            _alunos.Add(new Aluno(_proximoIdAluno++, "Ana Clara Souza", "12345678901",
                new DateTime(2002, 5, 14), "CC2022001", "Ciência da Computação",
                "ana@email.com", "31987654321", 2022));
            _alunos.Add(new Aluno(_proximoIdAluno++, "Bruno Lima", "98765432100",
                new DateTime(2001, 8, 22), "SI2021002", "Sistemas de Informação",
                "bruno@email.com", "31912345678", 2021));
            _alunos.Add(new Aluno(_proximoIdAluno++, "Carla Mendes", "45612378900",
                new DateTime(2003, 3, 10), "EC2023003", "Engenharia de Computação",
                "carla@email.com", "31998887766", 2023));
        }

        // Cadastra alguns livros iniciais para facilitar os testes
        private void InicializarLivros()
        {
            _livros.Add(new Livro(_proximoIdLivro++, "Clean Code", "Robert C. Martin",
                "Alta Books", "9788576082675", 2008, "Programação", 431, 3));
            _livros.Add(new Livro(_proximoIdLivro++, "O Algoritmo da Vida", "Brian Christian",
                "Intrinseca", "9788551000000", 2016, "Ciência", 368, 2));
            _livros.Add(new Livro(_proximoIdLivro++, "Design Patterns", "Gang of Four",
                "Bookman", "9780201633610", 1994, "Programação", 395, 2));
            _livros.Add(new Livro(_proximoIdLivro++, "Estruturas de Dados e Algoritmos em Java",
                "Robert Lafore", "Ciência Moderna", "9788573935653", 2004, "Programação", 800, 4));
            _livros.Add(new Livro(_proximoIdLivro++, "Inteligência Artificial", "Stuart Russell",
                "Elsevier", "9788535237016", 2013, "IA", 988, 2));
        }

        // ==================== INICIAR ====================

        public void Iniciar()
        {
            ExibirBoasVindas();
            bool executando = true;
            while (executando)
            {
                ExibirMenuPrincipal();
                int opcao = Validador.LerInteiro("  Opcao: ", 0, 5);
                executando = ProcessarOpcaoPrincipal(opcao);
            }
        }

        private void ExibirBoasVindas()
        {
            Console.WriteLine();
            Console.WriteLine("  ====================================================");
            Console.WriteLine("       SISTEMA DE GERENCIAMENTO DE BIBLIOTECA");
            Console.WriteLine("                   BiblioSys v1.0");
            Console.WriteLine("  ====================================================");
            Console.WriteLine();
        }

        private void ExibirMenuPrincipal()
        {
            Console.WriteLine();
            Console.WriteLine(LINHA);
            Console.WriteLine("                  MENU PRINCIPAL");
            Console.WriteLine(LINHA);
            Console.WriteLine();
            Console.WriteLine("  [1]  Gerenciar Alunos");
            Console.WriteLine("  [2]  Gerenciar Livros");
            Console.WriteLine("  [3]  Gerenciar Emprestimos");
            Console.WriteLine("  [4]  Relatorios");
            Console.WriteLine("  [5]  Permissoes de Funcionarios");
            Console.WriteLine("  [0]  Sair");
            Console.WriteLine();
            Console.WriteLine(LINHA_FINA);
        }

        private bool ProcessarOpcaoPrincipal(int opcao)
        {
            switch (opcao)
            {
                case 1: MenuAlunos(); break;
                case 2: MenuLivros(); break;
                case 3: MenuEmprestimos(); break;
                case 4: MenuRelatorios(); break;
                case 5: ExibirPermissoesFuncionarios(); break;
                case 0:
                    Console.WriteLine("\n  Encerrando sistema. Ate logo!\n");
                    return false;
            }
            return true;
        }

        // ==================== MENU ALUNOS ====================

        private void MenuAlunos()
        {
            bool voltar = false;
            while (!voltar)
            {
                ExibirCabecalho("GERENCIAMENTO DE ALUNOS");
                Console.WriteLine("  [1]  Cadastrar Aluno");
                Console.WriteLine("  [2]  Listar Alunos");
                Console.WriteLine("  [3]  Buscar Aluno por ID");
                Console.WriteLine("  [4]  Buscar Aluno por Nome");
                Console.WriteLine("  [5]  Atualizar Aluno");
                Console.WriteLine("  [6]  Remover Aluno");
                Console.WriteLine("  [0]  Voltar");
                Console.WriteLine();

                int opcao = Validador.LerInteiro("  Opcao: ", 0, 6);
                switch (opcao)
                {
                    case 1: CadastrarAluno(); break;
                    case 2: ListarAlunos(); break;
                    case 3: BuscarAlunoPorId(); break;
                    case 4: BuscarAlunoPorNome(); break;
                    case 5: AtualizarAluno(); break;
                    case 6: RemoverAluno(); break;
                    case 0: voltar = true; break;
                }
            }
        }

        private void CadastrarAluno()
        {
            ExibirCabecalho("CADASTRAR ALUNO");
            try
            {
                string nome      = Validador.LerStringObrigatoria("  Nome completo: ", 3);
                string cpf       = Validador.LerCpf("  CPF (somente numeros): ");
                DateTime nasc    = Validador.LerData("  Data de nascimento:");
                string matricula = Validador.LerStringObrigatoria("  Matricula: ");
                string curso     = Validador.LerStringObrigatoria("  Curso: ");
                string email     = Validador.LerEmail("  Email: ");
                string telefone  = Validador.LerStringObrigatoria("  Telefone: ");
                int anoIngresso  = Validador.LerInteiro(
                    $"  Ano de ingresso ({2000}-{DateTime.Now.Year}): ", 2000, DateTime.Now.Year);

                // Verifica CPF e matricula duplicados antes de cadastrar
                foreach (Aluno a in _alunos)
                {
                    if (a.Cpf == cpf)
                        throw new Exception("Ja existe um aluno com o CPF informado.");
                    if (a.Matricula == matricula.Trim().ToUpper())
                        throw new Exception($"Ja existe um aluno com a matricula {matricula}.");
                }

                var aluno = new Aluno(_proximoIdAluno++, nome, cpf, nasc,
                                      matricula, curso, email, telefone, anoIngresso);
                _alunos.Add(aluno);
                Validador.ExibirSucesso($"Aluno '{nome}' cadastrado com ID {aluno.Id}!");
            }
            catch (Exception ex)
            {
                Validador.ExibirErro(ex.Message);
            }
            Validador.PausarTela();
        }

        private void ListarAlunos()
        {
            ExibirCabecalho("LISTA DE ALUNOS");
            if (_alunos.Count == 0)
            {
                Validador.ExibirAviso("Nenhum aluno cadastrado.");
            }
            else
            {
                Console.WriteLine($"  Total: {_alunos.Count} aluno(s)\n");
                Console.WriteLine(LINHA_FINA);
                foreach (Aluno a in _alunos)
                    Console.WriteLine($"  {a}");
                Console.WriteLine(LINHA_FINA);
            }
            Validador.PausarTela();
        }

        private void BuscarAlunoPorId()
        {
            ExibirCabecalho("BUSCAR ALUNO POR ID");
            try
            {
                int id = Validador.LerInteiroPositivo("  ID do aluno: ");
                Aluno encontrado = null;
                foreach (Aluno a in _alunos)
                    if (a.Id == id) { encontrado = a; break; }

                if (encontrado == null)
                    throw new Exception($"Aluno com ID {id} nao encontrado.");

                Console.WriteLine();
                Console.WriteLine(LINHA_FINA);
                Console.WriteLine($"  {encontrado}");
                Console.WriteLine(LINHA_FINA);
            }
            catch (Exception ex)
            {
                Validador.ExibirErro(ex.Message);
            }
            Validador.PausarTela();
        }

        private void BuscarAlunoPorNome()
        {
            ExibirCabecalho("BUSCAR ALUNO POR NOME");
            string nome = Validador.LerStringObrigatoria("  Nome (parcial): ");

            List<Aluno> resultado = new List<Aluno>();
            foreach (Aluno a in _alunos)
                if (a.Nome.IndexOf(nome, StringComparison.OrdinalIgnoreCase) >= 0)
                    resultado.Add(a);

            if (resultado.Count == 0)
                Validador.ExibirAviso("Nenhum aluno encontrado.");
            else
            {
                Console.WriteLine($"\n  {resultado.Count} resultado(s):\n");
                foreach (Aluno a in resultado)
                    Console.WriteLine($"  {a}");
            }
            Validador.PausarTela();
        }

        private void AtualizarAluno()
        {
            ExibirCabecalho("ATUALIZAR ALUNO");
            try
            {
                int id = Validador.LerInteiroPositivo("  ID do aluno a atualizar: ");
                Aluno aluno = null;
                foreach (Aluno a in _alunos)
                    if (a.Id == id) { aluno = a; break; }

                if (aluno == null)
                    throw new Exception($"Aluno com ID {id} nao encontrado.");

                Console.WriteLine($"\n  Aluno atual: {aluno.Nome}");
                Console.WriteLine("  (Pressione Enter para manter o valor atual)\n");

                Console.Write($"  Novo nome [{aluno.Nome}]: ");
                string nome = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(nome)) aluno.Nome = nome;

                Console.Write($"  Novo email [{aluno.Email}]: ");
                string email = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(email)) aluno.Email = email;

                Console.Write($"  Novo telefone [{aluno.Telefone}]: ");
                string tel = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(tel)) aluno.Telefone = tel;

                Console.Write($"  Novo curso [{aluno.Curso}]: ");
                string curso = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(curso)) aluno.Curso = curso;

                Console.Write($"  Ativo? [{(aluno.Ativo ? "S" : "N")}] (S/N): ");
                string ativo = Console.ReadLine()?.Trim().ToUpper();
                if (ativo == "S") aluno.Ativo = true;
                else if (ativo == "N") aluno.Ativo = false;

                Validador.ExibirSucesso("Aluno atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Validador.ExibirErro(ex.Message);
            }
            Validador.PausarTela();
        }

        private void RemoverAluno()
        {
            ExibirCabecalho("REMOVER ALUNO");
            try
            {
                int id = Validador.LerInteiroPositivo("  ID do aluno a remover: ");
                Aluno aluno = null;
                foreach (Aluno a in _alunos)
                    if (a.Id == id) { aluno = a; break; }

                if (aluno == null)
                    throw new Exception($"Aluno com ID {id} nao encontrado.");
                if (aluno.EmprestimosAtivos > 0)
                    throw new Exception("Nao e possivel remover aluno com emprestimos ativos.");

                Console.WriteLine($"\n  Aluno: {aluno.Nome} | Matricula: {aluno.Matricula}");

                if (Validador.ConfirmarOperacao("  Confirmar remocao?"))
                {
                    _alunos.Remove(aluno);
                    Validador.ExibirSucesso("Aluno removido com sucesso!");
                }
                else
                    Validador.ExibirAviso("Operacao cancelada.");
            }
            catch (Exception ex)
            {
                Validador.ExibirErro(ex.Message);
            }
            Validador.PausarTela();
        }

        // ==================== MENU LIVROS ====================

        private void MenuLivros()
        {
            bool voltar = false;
            while (!voltar)
            {
                ExibirCabecalho("GERENCIAMENTO DE LIVROS");
                Console.WriteLine("  [1]  Cadastrar Livro");
                Console.WriteLine("  [2]  Listar Livros");
                Console.WriteLine("  [3]  Livros Disponiveis");
                Console.WriteLine("  [4]  Buscar por Titulo");
                Console.WriteLine("  [5]  Buscar por Autor");
                Console.WriteLine("  [6]  Ver Detalhes do Livro");
                Console.WriteLine("  [7]  Atualizar Livro");
                Console.WriteLine("  [8]  Remover Livro");
                Console.WriteLine("  [0]  Voltar");
                Console.WriteLine();

                int opcao = Validador.LerInteiro("  Opcao: ", 0, 8);
                switch (opcao)
                {
                    case 1: CadastrarLivro(); break;
                    case 2: ListarLivros(false); break;
                    case 3: ListarLivros(true); break;
                    case 4: BuscarLivroPorTitulo(); break;
                    case 5: BuscarLivroPorAutor(); break;
                    case 6: VerDetalhesLivro(); break;
                    case 7: AtualizarLivro(); break;
                    case 8: RemoverLivro(); break;
                    case 0: voltar = true; break;
                }
            }
        }

        private void CadastrarLivro()
        {
            ExibirCabecalho("CADASTRAR LIVRO");
            try
            {
                string titulo  = Validador.LerStringObrigatoria("  Titulo: ");
                string autor   = Validador.LerStringObrigatoria("  Autor: ");
                string editora = Validador.LerStringObrigatoria("  Editora: ");
                string isbn    = Validador.LerStringObrigatoria("  ISBN (10 ou 13 digitos): ");
                int ano        = Validador.LerInteiro(
                    $"  Ano de publicacao (1400-{DateTime.Now.Year}): ", 1400, DateTime.Now.Year);
                string genero  = Validador.LerStringObrigatoria("  Genero: ");
                int paginas    = Validador.LerInteiroPositivo("  Numero de paginas: ");
                int qtd        = Validador.LerInteiroPositivo("  Quantidade de exemplares: ");

                // Verifica ISBN duplicado
                string isbnLimpo = isbn.Replace("-", "").Replace(" ", "");
                foreach (Livro l in _livros)
                    if (l.Isbn == isbnLimpo)
                        throw new Exception($"Ja existe um livro com o ISBN {isbn}.");

                var livro = new Livro(_proximoIdLivro++, titulo, autor, editora,
                                      isbn, ano, genero, paginas, qtd);
                _livros.Add(livro);
                Validador.ExibirSucesso($"Livro '{titulo}' cadastrado com ID {livro.Id}!");
            }
            catch (Exception ex)
            {
                Validador.ExibirErro(ex.Message);
            }
            Validador.PausarTela();
        }

        private void ListarLivros(bool somenteDisponiveis)
        {
            string titulo = somenteDisponiveis ? "LIVROS DISPONIVEIS" : "TODOS OS LIVROS";
            ExibirCabecalho(titulo);

            List<Livro> lista = new List<Livro>();
            foreach (Livro l in _livros)
                if (!somenteDisponiveis || l.QuantidadeDisponivel > 0)
                    lista.Add(l);

            if (lista.Count == 0)
                Validador.ExibirAviso("Nenhum livro encontrado.");
            else
            {
                Console.WriteLine($"  Total: {lista.Count} livro(s)\n");
                Console.WriteLine(LINHA_FINA);
                foreach (Livro l in lista)
                    Console.WriteLine($"  {l}");
                Console.WriteLine(LINHA_FINA);
            }
            Validador.PausarTela();
        }

        private void BuscarLivroPorTitulo()
        {
            ExibirCabecalho("BUSCAR LIVRO POR TITULO");
            string busca = Validador.LerStringObrigatoria("  Titulo (parcial): ");
            List<Livro> resultado = new List<Livro>();
            foreach (Livro l in _livros)
                if (l.Titulo.IndexOf(busca, StringComparison.OrdinalIgnoreCase) >= 0)
                    resultado.Add(l);
            ExibirListaLivros(resultado);
            Validador.PausarTela();
        }

        private void BuscarLivroPorAutor()
        {
            ExibirCabecalho("BUSCAR LIVRO POR AUTOR");
            string busca = Validador.LerStringObrigatoria("  Autor (parcial): ");
            List<Livro> resultado = new List<Livro>();
            foreach (Livro l in _livros)
                if (l.Autor.IndexOf(busca, StringComparison.OrdinalIgnoreCase) >= 0)
                    resultado.Add(l);
            ExibirListaLivros(resultado);
            Validador.PausarTela();
        }

        private void ExibirListaLivros(List<Livro> livros)
        {
            if (livros.Count == 0)
                Validador.ExibirAviso("Nenhum livro encontrado.");
            else
            {
                Console.WriteLine($"\n  {livros.Count} resultado(s):\n");
                foreach (Livro l in livros)
                    Console.WriteLine($"  {l}");
            }
        }

        private void VerDetalhesLivro()
        {
            ExibirCabecalho("DETALHES DO LIVRO");
            try
            {
                int id = Validador.LerInteiroPositivo("  ID do livro: ");
                Livro livro = null;
                foreach (Livro l in _livros)
                    if (l.Id == id) { livro = l; break; }

                if (livro == null)
                    throw new Exception($"Livro com ID {id} nao encontrado.");

                Console.WriteLine();
                Console.WriteLine(LINHA_FINA);
                // Usa o método da classe base ItemBiblioteca para exibir os detalhes
                Console.WriteLine($"  Categoria: {livro.ObterCategoria()}");
                Console.WriteLine($"  {livro.ObterDetalhes()}");
                Console.WriteLine(LINHA_FINA);
            }
            catch (Exception ex)
            {
                Validador.ExibirErro(ex.Message);
            }
            Validador.PausarTela();
        }

        private void AtualizarLivro()
        {
            ExibirCabecalho("ATUALIZAR LIVRO");
            try
            {
                int id = Validador.LerInteiroPositivo("  ID do livro a atualizar: ");
                Livro livro = null;
                foreach (Livro l in _livros)
                    if (l.Id == id) { livro = l; break; }

                if (livro == null)
                    throw new Exception($"Livro com ID {id} nao encontrado.");

                Console.WriteLine($"\n  Livro atual: {livro.Titulo}");
                Console.WriteLine("  (Pressione Enter para manter o valor atual)\n");

                Console.Write($"  Novo titulo [{livro.Titulo}]: ");
                string t = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(t)) livro.Titulo = t;

                Console.Write($"  Novo autor [{livro.Autor}]: ");
                string a = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(a)) livro.Autor = a;

                Console.Write($"  Nova editora [{livro.Editora}]: ");
                string e = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(e)) livro.Editora = e;

                Console.Write($"  Novo genero [{livro.Genero}]: ");
                string g = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(g)) livro.Genero = g;

                Validador.ExibirSucesso("Livro atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Validador.ExibirErro(ex.Message);
            }
            Validador.PausarTela();
        }

        private void RemoverLivro()
        {
            ExibirCabecalho("REMOVER LIVRO");
            try
            {
                int id = Validador.LerInteiroPositivo("  ID do livro a remover: ");
                Livro livro = null;
                foreach (Livro l in _livros)
                    if (l.Id == id) { livro = l; break; }

                if (livro == null)
                    throw new Exception($"Livro com ID {id} nao encontrado.");
                if (livro.QuantidadeDisponivel < livro.QuantidadeTotal)
                    throw new Exception("Nao e possivel remover livro com exemplares emprestados.");

                Console.WriteLine($"\n  Livro: {livro.Titulo} | Autor: {livro.Autor}");

                if (Validador.ConfirmarOperacao("  Confirmar remocao?"))
                {
                    _livros.Remove(livro);
                    Validador.ExibirSucesso("Livro removido com sucesso!");
                }
                else
                    Validador.ExibirAviso("Operacao cancelada.");
            }
            catch (Exception ex)
            {
                Validador.ExibirErro(ex.Message);
            }
            Validador.PausarTela();
        }

        // ==================== MENU EMPRÉSTIMOS ====================

        private void MenuEmprestimos()
        {
            bool voltar = false;
            while (!voltar)
            {
                ExibirCabecalho("GERENCIAMENTO DE EMPRESTIMOS");
                Console.WriteLine("  [1]  Novo Emprestimo");
                Console.WriteLine("  [2]  Devolver Livro");
                Console.WriteLine("  [3]  Renovar Emprestimo");
                Console.WriteLine("  [4]  Listar Todos os Emprestimos");
                Console.WriteLine("  [5]  Emprestimos Ativos");
                Console.WriteLine("  [6]  Emprestimos de um Aluno");
                Console.WriteLine("  [7]  Emprestimos Atrasados");
                Console.WriteLine("  [0]  Voltar");
                Console.WriteLine();

                int opcao = Validador.LerInteiro("  Opcao: ", 0, 7);
                switch (opcao)
                {
                    case 1: NovoEmprestimo(); break;
                    case 2: DevolverLivro(); break;
                    case 3: RenovarEmprestimo(); break;
                    case 4: ListarEmprestimos(false); break;
                    case 5: ListarEmprestimos(true); break;
                    case 6: EmprestimosDoAluno(); break;
                    case 7: EmprestimosAtrasados(); break;
                    case 0: voltar = true; break;
                }
            }
        }

        private void NovoEmprestimo()
        {
            ExibirCabecalho("NOVO EMPRESTIMO");
            try
            {
                int alunoId = Validador.LerInteiroPositivo("  ID do aluno: ");
                int livroId = Validador.LerInteiroPositivo("  ID do livro: ");

                // Busca o aluno e o livro nas listas
                Aluno aluno = null;
                foreach (Aluno a in _alunos)
                    if (a.Id == alunoId) { aluno = a; break; }
                if (aluno == null)
                    throw new Exception($"Aluno com ID {alunoId} nao encontrado.");

                Livro livro = null;
                foreach (Livro l in _livros)
                    if (l.Id == livroId) { livro = l; break; }
                if (livro == null)
                    throw new Exception($"Livro com ID {livroId} nao encontrado.");

                // Validacoes de negocio
                if (!aluno.Ativo)
                    throw new Exception($"Aluno '{aluno.Nome}' esta inativo.");
                if (!aluno.PodePegarEmprestimo)
                    throw new Exception(
                        $"Aluno '{aluno.Nome}' atingiu o limite de {Aluno.MaxEmprestimos} emprestimos.");

                // Verifica se o aluno ja tem este livro emprestado
                foreach (Emprestimo e in _emprestimos)
                    if (e.AlunoId == alunoId && e.LivroId == livroId &&
                        (e.Status == StatusEmprestimo.Ativo || e.Status == StatusEmprestimo.Renovado))
                        throw new Exception($"Aluno '{aluno.Nome}' ja possui este livro emprestado.");

                if (!livro.RealizarEmprestimo())
                    throw new Exception($"Livro '{livro.Titulo}' nao possui exemplares disponiveis.");

                aluno.EmprestimosAtivos++;

                var emprestimo = new Emprestimo(_proximoIdEmprestimo++,
                    alunoId, aluno.Nome, livroId, livro.Titulo);
                _emprestimos.Add(emprestimo);

                Validador.ExibirSucesso($"Emprestimo registrado! ID: {emprestimo.Id:D4}");
                Console.WriteLine($"  Devolucao prevista: {emprestimo.DataPrevistaDevolucao:dd/MM/yyyy}");
            }
            catch (Exception ex)
            {
                Validador.ExibirErro(ex.Message);
            }
            Validador.PausarTela();
        }

        private void DevolverLivro()
        {
            ExibirCabecalho("DEVOLVER LIVRO");
            try
            {
                int id = Validador.LerInteiroPositivo("  ID do emprestimo: ");
                Emprestimo emp = null;
                foreach (Emprestimo e in _emprestimos)
                    if (e.Id == id) { emp = e; break; }

                if (emp == null)
                    throw new Exception($"Emprestimo com ID {id} nao encontrado.");
                if (emp.Status == StatusEmprestimo.Devolvido)
                    throw new Exception("Este emprestimo ja foi devolvido.");

                Console.WriteLine($"\n  Aluno: {emp.AlunoNome}");
                Console.WriteLine($"  Livro: {emp.LivroTitulo}");

                if (emp.Atrasado)
                    Validador.ExibirAviso(
                        $"Emprestimo atrasado! {emp.DiasAtraso} dia(s) - Multa: R${emp.MultaPorAtraso:F2}");

                if (Validador.ConfirmarOperacao("  Confirmar devolucao?"))
                {
                    emp.Devolver();

                    // Devolve o exemplar ao livro e atualiza o contador do aluno
                    foreach (Livro l in _livros)
                        if (l.Id == emp.LivroId) { l.RealizarDevolucao(); break; }

                    foreach (Aluno a in _alunos)
                        if (a.Id == emp.AlunoId && a.EmprestimosAtivos > 0)
                        { a.EmprestimosAtivos--; break; }

                    Validador.ExibirSucesso("Devolucao registrada com sucesso!");
                }
                else
                    Validador.ExibirAviso("Operacao cancelada.");
            }
            catch (Exception ex)
            {
                Validador.ExibirErro(ex.Message);
            }
            Validador.PausarTela();
        }

        private void RenovarEmprestimo()
        {
            ExibirCabecalho("RENOVAR EMPRESTIMO");
            try
            {
                int id = Validador.LerInteiroPositivo("  ID do emprestimo: ");
                Emprestimo emp = null;
                foreach (Emprestimo e in _emprestimos)
                    if (e.Id == id) { emp = e; break; }

                if (emp == null)
                    throw new Exception($"Emprestimo com ID {id} nao encontrado.");

                Console.WriteLine($"\n  Aluno: {emp.AlunoNome} | Livro: {emp.LivroTitulo}");
                Console.WriteLine($"  Renovacoes: {emp.NumeroRenovacoes}/{Emprestimo.MaxRenovacoes}");

                if (emp.Renovar())
                    Validador.ExibirSucesso(
                        $"Emprestimo renovado! Nova devolucao: {emp.DataPrevistaDevolucao:dd/MM/yyyy}");
                else
                    Validador.ExibirAviso(
                        "Nao foi possivel renovar. Limite de renovacoes atingido ou status invalido.");
            }
            catch (Exception ex)
            {
                Validador.ExibirErro(ex.Message);
            }
            Validador.PausarTela();
        }

        private void ListarEmprestimos(bool somenteAtivos)
        {
            string titulo = somenteAtivos ? "EMPRESTIMOS ATIVOS" : "TODOS OS EMPRESTIMOS";
            ExibirCabecalho(titulo);

            // Atualiza o status de cada emprestimo antes de exibir
            foreach (Emprestimo e in _emprestimos)
                e.AtualizarStatus();

            List<Emprestimo> lista = new List<Emprestimo>();
            foreach (Emprestimo e in _emprestimos)
                if (!somenteAtivos ||
                    e.Status == StatusEmprestimo.Ativo ||
                    e.Status == StatusEmprestimo.Renovado ||
                    e.Status == StatusEmprestimo.Atrasado)
                    lista.Add(e);

            if (lista.Count == 0)
                Validador.ExibirAviso("Nenhum emprestimo encontrado.");
            else
            {
                Console.WriteLine($"  Total: {lista.Count} emprestimo(s)\n");
                Console.WriteLine(LINHA_FINA);
                foreach (Emprestimo e in lista)
                    Console.WriteLine($"  {e}");
                Console.WriteLine(LINHA_FINA);
            }
            Validador.PausarTela();
        }

        private void EmprestimosDoAluno()
        {
            ExibirCabecalho("EMPRESTIMOS POR ALUNO");
            try
            {
                int alunoId = Validador.LerInteiroPositivo("  ID do aluno: ");
                Aluno aluno = null;
                foreach (Aluno a in _alunos)
                    if (a.Id == alunoId) { aluno = a; break; }

                if (aluno == null)
                    throw new Exception($"Aluno com ID {alunoId} nao encontrado.");

                List<Emprestimo> lista = new List<Emprestimo>();
                foreach (Emprestimo e in _emprestimos)
                    if (e.AlunoId == alunoId) lista.Add(e);

                Console.WriteLine($"\n  Aluno: {aluno.Nome}");
                Console.WriteLine($"  Total de emprestimos: {lista.Count}\n");
                Console.WriteLine(LINHA_FINA);
                foreach (Emprestimo e in lista)
                    Console.WriteLine($"  {e}");
                Console.WriteLine(LINHA_FINA);
            }
            catch (Exception ex)
            {
                Validador.ExibirErro(ex.Message);
            }
            Validador.PausarTela();
        }

        private void EmprestimosAtrasados()
        {
            ExibirCabecalho("EMPRESTIMOS ATRASADOS");

            foreach (Emprestimo e in _emprestimos)
                e.AtualizarStatus();

            List<Emprestimo> lista = new List<Emprestimo>();
            foreach (Emprestimo e in _emprestimos)
                if (e.Atrasado) lista.Add(e);

            if (lista.Count == 0)
            {
                Console.WriteLine("\n  Nenhum emprestimo atrasado!");
            }
            else
            {
                Console.WriteLine($"\n  ATENCAO: {lista.Count} emprestimo(s) atrasado(s)!\n");
                Console.WriteLine(LINHA_FINA);
                decimal totalMultas = 0;
                foreach (Emprestimo e in lista)
                {
                    Console.WriteLine($"  {e}");
                    totalMultas += e.MultaPorAtraso;
                }
                Console.WriteLine(LINHA_FINA);
                Console.WriteLine($"\n  Total de multas acumuladas: R${totalMultas:F2}");
            }
            Validador.PausarTela();
        }

        // ==================== RELATÓRIOS ====================

        private void MenuRelatorios()
        {
            ExibirCabecalho("RELATORIOS");

            int alunosAtivos = 0;
            foreach (Aluno a in _alunos)
                if (a.Ativo) alunosAtivos++;

            int livrosDisponiveis = 0;
            foreach (Livro l in _livros)
                if (l.QuantidadeDisponivel > 0) livrosDisponiveis++;

            int empAtivos = 0, empAtrasados = 0;
            foreach (Emprestimo e in _emprestimos)
            {
                e.AtualizarStatus();
                if (e.Status == StatusEmprestimo.Ativo ||
                    e.Status == StatusEmprestimo.Renovado ||
                    e.Status == StatusEmprestimo.Atrasado)
                    empAtivos++;
                if (e.Atrasado) empAtrasados++;
            }

            Console.WriteLine("  +---------------------------------------------------+");
            Console.WriteLine("  |              RESUMO DO SISTEMA                    |");
            Console.WriteLine("  +---------------------------------------------------+");
            Console.WriteLine($"  |  Alunos cadastrados     : {_alunos.Count,-5}                    |");
            Console.WriteLine($"  |  Alunos ativos          : {alunosAtivos,-5}                    |");
            Console.WriteLine($"  |  Livros no acervo       : {_livros.Count,-5}                    |");
            Console.WriteLine($"  |  Livros disponiveis     : {livrosDisponiveis,-5}                    |");
            Console.WriteLine($"  |  Total de emprestimos   : {_emprestimos.Count,-5}                    |");
            Console.WriteLine($"  |  Emprestimos ativos     : {empAtivos,-5}                    |");
            Console.WriteLine($"  |  Emprestimos atrasados  : {empAtrasados,-5}                    |");
            Console.WriteLine("  +---------------------------------------------------+");

            Validador.PausarTela();
        }

        // ==================== PERMISSÕES DE FUNCIONÁRIOS ====================

        private void ExibirPermissoesFuncionarios()
        {
            ExibirCabecalho("PERMISSOES DE FUNCIONARIOS");

            Console.WriteLine("  Tipos de pessoa no sistema e suas permissoes de acesso:\n");

            // Criando objetos de diferentes classes para exibir os tipos e permissoes
            var funcionario = new Funcionario(99, "Joao Gestor", "11122233344",
                new DateTime(1985, 6, 15), "joao@bib.com", "31911111111",
                "FUNC001", "Assistente", 2800m, new DateTime(2018, 3, 1));

            var bibliotecario = new Bibliotecario(100, "Maria Silva", "55566677788",
                new DateTime(1978, 9, 20), "maria@bib.com", "31922222222",
                "BIB001", 4500m, new DateTime(2010, 1, 15), "CRB-6/1234", true);

            var aluno = new Aluno(1, "Pedro Alves", "99988877766",
                new DateTime(2001, 4, 10), "CC2020001", "CC",
                "pedro@email.com", "31933333333", 2020);

            Console.WriteLine("  -- Tipo de cada pessoa no sistema --");
            // Lista de Pessoa para percorrer objetos de tipos diferentes
            var pessoas = new List<Pessoa> { funcionario, bibliotecario, aluno };
            foreach (Pessoa p in pessoas)
                Console.WriteLine($"  {p.ObterTipo(),-20} -> {p.Nome}");

            Console.WriteLine("\n  -- Permissoes por cargo --");
            // Lista de Funcionario para chamar ObterPermissoes() em cada um
            var funcionarios = new List<Funcionario> { funcionario, bibliotecario };
            foreach (Funcionario f in funcionarios)
            {
                Console.WriteLine($"\n  [{f.ObterTipo()}] {f.Nome}:");
                Console.WriteLine($"  {f.ObterPermissoes()}");
            }

            Console.WriteLine("\n  -- Detalhes do primeiro livro cadastrado --");
            if (_livros.Count > 0)
            {
                // Usa a referência da classe base ItemBiblioteca para acessar os detalhes
                ItemBiblioteca item = _livros[0];
                Console.WriteLine($"  Categoria: {item.ObterCategoria()}");
                Console.WriteLine($"  {item.ObterDetalhes()}");
            }

            Validador.PausarTela();
        }

        // ==================== AUXILIARES ====================

        private void ExibirCabecalho(string titulo)
        {
            Console.WriteLine();
            Console.WriteLine(LINHA);
            Console.WriteLine($"  {titulo}");
            Console.WriteLine(LINHA);
            Console.WriteLine();
        }
    }
}
