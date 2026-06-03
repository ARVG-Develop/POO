using System;
using System.Collections.Generic;
using SistemaBiblioteca.Models;
using SistemaBiblioteca.Services;
using SistemaBiblioteca.Utils;

namespace SistemaBiblioteca.Menus
{
    public class MenuPrincipal
    {
        private readonly BibliotecaService _service;
        private const string LINHA = "  ════════════════════════════════════════════════════";
        private const string LINHA_FINA = "  ────────────────────────────────────────────────────";

        public MenuPrincipal()
        {
            _service = new BibliotecaService();
        }

        public void Iniciar()
        {
            ExibirBoasVindas();
            bool executando = true;
            while (executando)
            {
                ExibirMenuPrincipal();
                int opcao = Validador.LerInteiro("  Opção: ", 0, 5);
                executando = ProcessarOpcaoPrincipal(opcao);
            }
        }

        private void ExibirBoasVindas()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("  ╔══════════════════════════════════════════════════╗");
            Console.WriteLine("  ║        SISTEMA DE GERENCIAMENTO BIBLIOTECA       ║");
            Console.WriteLine("  ║                  BiblioSys v1.0                  ║");
            Console.WriteLine("  ╚══════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        private void ExibirMenuPrincipal()
        {
            Console.Clear();
            ExibirBoasVindas();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(LINHA);
            Console.WriteLine("                   MENU PRINCIPAL");
            Console.WriteLine(LINHA);
            Console.WriteLine();
            Console.WriteLine("  [1]  Gerenciar Alunos");
            Console.WriteLine("  [2]  Gerenciar Livros");
            Console.WriteLine("  [3]  Gerenciar Empréstimos");
            Console.WriteLine("  [4]  Relatórios");
            Console.WriteLine("  [5]  Demo Polimorfismo");
            Console.WriteLine("  [0]  Sair");
            Console.WriteLine();
            Console.WriteLine(LINHA_FINA);
            Console.ResetColor();
        }

        private bool ProcessarOpcaoPrincipal(int opcao)
        {
            switch (opcao)
            {
                case 1: MenuAlunos(); break;
                case 2: MenuLivros(); break;
                case 3: MenuEmprestimos(); break;
                case 4: MenuRelatorios(); break;
                case 5: DemoPolimorfismo(); break;
                case 0:
                    Console.WriteLine("\n  Encerrando sistema. Até logo!\n");
                    return false;
            }
            return true;
        }

        // ==================== MENU ALUNOS (CRUD) ====================
        private void MenuAlunos()
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.Clear();
                ExibirCabecalho("GERENCIAMENTO DE ALUNOS");
                Console.WriteLine("  [1]  Cadastrar Aluno");
                Console.WriteLine("  [2]  Listar Alunos");
                Console.WriteLine("  [3]  Buscar Aluno por ID");
                Console.WriteLine("  [4]  Buscar Aluno por Nome");
                Console.WriteLine("  [5]  Atualizar Aluno");
                Console.WriteLine("  [6]  Remover Aluno");
                Console.WriteLine("  [0]  Voltar");
                Console.WriteLine();

                int opcao = Validador.LerInteiro("  Opção: ", 0, 6);
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
                string nome = Validador.LerStringObrigatoria("  Nome completo: ", 3);
                string cpf = Validador.LerCpf("  CPF (somente números): ");
                DateTime nascimento = Validador.LerData("  Data de nascimento (dd/MM/yyyy): ");
                string matricula = Validador.LerStringObrigatoria("  Matrícula: ");
                string curso = Validador.LerStringObrigatoria("  Curso: ");
                string email = Validador.LerEmail("  Email: ");
                string telefone = Validador.LerStringObrigatoria("  Telefone: ");
                int anoIngresso = Validador.LerInteiro($"  Ano de ingresso ({2000}-{DateTime.Now.Year}): ",
                                                        2000, DateTime.Now.Year);

                var aluno = new Aluno(0, nome, cpf, nascimento, matricula, curso,
                                      email, telefone, anoIngresso);
                _service.CadastrarAluno(aluno);
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
            var alunos = _service.ListarAlunos();
            if (alunos.Count == 0)
            {
                Validador.ExibirAviso("Nenhum aluno cadastrado.");
            }
            else
            {
                Console.WriteLine($"  Total: {alunos.Count} aluno(s)\n");
                Console.WriteLine(LINHA_FINA);
                foreach (var a in alunos)
                {
                    Console.ForegroundColor = a.Ativo ? ConsoleColor.White : ConsoleColor.DarkGray;
                    Console.WriteLine($"  {a}");
                    Console.ResetColor();
                }
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
                var aluno = _service.BuscarAluno(id);
                Console.WriteLine();
                Console.WriteLine(LINHA_FINA);
                Console.WriteLine($"  {aluno}");
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
            var alunos = _service.BuscarAlunosPorNome(nome);

            if (alunos.Count == 0)
                Validador.ExibirAviso("Nenhum aluno encontrado.");
            else
            {
                Console.WriteLine($"\n  {alunos.Count} resultado(s):\n");
                foreach (var a in alunos)
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
                var aluno = _service.BuscarAluno(id);
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

                bool ativoAtual = aluno.Ativo;
                Console.Write($"  Ativo? [{(ativoAtual ? "S" : "N")}] (S/N): ");
                string ativo = Console.ReadLine()?.Trim().ToUpper();
                if (ativo == "S") aluno.Ativo = true;
                else if (ativo == "N") aluno.Ativo = false;

                _service.AtualizarAluno(aluno);
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
                var aluno = _service.BuscarAluno(id);
                Console.WriteLine($"\n  Aluno: {aluno.Nome} | Matrícula: {aluno.Matricula}");

                if (Validador.ConfirmarOperacao("  Confirmar remoção?"))
                {
                    _service.RemoverAluno(id);
                    Validador.ExibirSucesso("Aluno removido com sucesso!");
                }
                else
                {
                    Validador.ExibirAviso("Operação cancelada.");
                }
            }
            catch (Exception ex)
            {
                Validador.ExibirErro(ex.Message);
            }
            Validador.PausarTela();
        }

        // ==================== MENU LIVROS (CRUD) ====================
        private void MenuLivros()
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.Clear();
                ExibirCabecalho("GERENCIAMENTO DE LIVROS");
                Console.WriteLine("  [1]  Cadastrar Livro");
                Console.WriteLine("  [2]  Listar Livros");
                Console.WriteLine("  [3]  Livros Disponíveis");
                Console.WriteLine("  [4]  Buscar por Título");
                Console.WriteLine("  [5]  Buscar por Autor");
                Console.WriteLine("  [6]  Ver Detalhes do Livro");
                Console.WriteLine("  [7]  Atualizar Livro");
                Console.WriteLine("  [8]  Remover Livro");
                Console.WriteLine("  [0]  Voltar");
                Console.WriteLine();

                int opcao = Validador.LerInteiro("  Opção: ", 0, 8);
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
                string titulo = Validador.LerStringObrigatoria("  Título: ");
                string autor = Validador.LerStringObrigatoria("  Autor: ");
                string editora = Validador.LerStringObrigatoria("  Editora: ");
                string isbn = Validador.LerStringObrigatoria("  ISBN (10 ou 13 dígitos): ");
                int ano = Validador.LerInteiro($"  Ano de publicação (1400-{DateTime.Now.Year}): ",
                                               1400, DateTime.Now.Year);
                string genero = Validador.LerStringObrigatoria("  Gênero: ");
                int paginas = Validador.LerInteiroPositivo("  Número de páginas: ");
                int qtd = Validador.LerInteiroPositivo("  Quantidade de exemplares: ");

                var livro = new Livro(0, titulo, autor, editora, isbn, ano, genero, paginas, qtd);
                _service.CadastrarLivro(livro);
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
            string titulo = somenteDisponiveis ? "LIVROS DISPONÍVEIS" : "TODOS OS LIVROS";
            ExibirCabecalho(titulo);
            var livros = somenteDisponiveis ? _service.ListarLivrosDisponiveis() : _service.ListarLivros();

            if (livros.Count == 0)
                Validador.ExibirAviso("Nenhum livro encontrado.");
            else
            {
                Console.WriteLine($"  Total: {livros.Count} livro(s)\n");
                Console.WriteLine(LINHA_FINA);
                foreach (var l in livros)
                {
                    Console.ForegroundColor = l.QuantidadeDisponivel > 0 ? ConsoleColor.White : ConsoleColor.DarkGray;
                    Console.WriteLine($"  {l}");
                    Console.ResetColor();
                }
                Console.WriteLine(LINHA_FINA);
            }
            Validador.PausarTela();
        }

        private void BuscarLivroPorTitulo()
        {
            ExibirCabecalho("BUSCAR LIVRO POR TÍTULO");
            string busca = Validador.LerStringObrigatoria("  Título (parcial): ");
            var livros = _service.BuscarLivrosPorTitulo(busca);
            ExibirListaLivros(livros);
            Validador.PausarTela();
        }

        private void BuscarLivroPorAutor()
        {
            ExibirCabecalho("BUSCAR LIVRO POR AUTOR");
            string busca = Validador.LerStringObrigatoria("  Autor (parcial): ");
            var livros = _service.BuscarLivrosPorAutor(busca);
            ExibirListaLivros(livros);
            Validador.PausarTela();
        }

        private void ExibirListaLivros(List<Livro> livros)
        {
            if (livros.Count == 0)
                Validador.ExibirAviso("Nenhum livro encontrado.");
            else
            {
                Console.WriteLine($"\n  {livros.Count} resultado(s):\n");
                foreach (var l in livros)
                    Console.WriteLine($"  {l}");
            }
        }

        private void VerDetalhesLivro()
        {
            ExibirCabecalho("DETALHES DO LIVRO");
            try
            {
                int id = Validador.LerInteiroPositivo("  ID do livro: ");
                var livro = _service.BuscarLivro(id);
                Console.WriteLine();
                Console.WriteLine(LINHA_FINA);
                // POLIMORFISMO: usando método de ItemBiblioteca
                _service.ExibirDetalhesItem(livro);
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
                var livro = _service.BuscarLivro(id);
                Console.WriteLine($"\n  Livro atual: {livro.Titulo}");
                Console.WriteLine("  (Pressione Enter para manter o valor atual)\n");

                Console.Write($"  Novo título [{livro.Titulo}]: ");
                string t = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(t)) livro.Titulo = t;

                Console.Write($"  Novo autor [{livro.Autor}]: ");
                string a = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(a)) livro.Autor = a;

                Console.Write($"  Nova editora [{livro.Editora}]: ");
                string e = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(e)) livro.Editora = e;

                Console.Write($"  Novo gênero [{livro.Genero}]: ");
                string g = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(g)) livro.Genero = g;

                _service.AtualizarLivro(livro);
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
                var livro = _service.BuscarLivro(id);
                Console.WriteLine($"\n  Livro: {livro.Titulo} | Autor: {livro.Autor}");

                if (Validador.ConfirmarOperacao("  Confirmar remoção?"))
                {
                    _service.RemoverLivro(id);
                    Validador.ExibirSucesso("Livro removido com sucesso!");
                }
                else
                    Validador.ExibirAviso("Operação cancelada.");
            }
            catch (Exception ex)
            {
                Validador.ExibirErro(ex.Message);
            }
            Validador.PausarTela();
        }

        // ==================== MENU EMPRÉSTIMOS (CRUD) ====================
        private void MenuEmprestimos()
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.Clear();
                ExibirCabecalho("GERENCIAMENTO DE EMPRÉSTIMOS");
                Console.WriteLine("  [1]  Novo Empréstimo");
                Console.WriteLine("  [2]  Devolver Livro");
                Console.WriteLine("  [3]  Renovar Empréstimo");
                Console.WriteLine("  [4]  Listar Todos os Empréstimos");
                Console.WriteLine("  [5]  Empréstimos Ativos");
                Console.WriteLine("  [6]  Empréstimos de um Aluno");
                Console.WriteLine("  [7]  Empréstimos Atrasados");
                Console.WriteLine("  [0]  Voltar");
                Console.WriteLine();

                int opcao = Validador.LerInteiro("  Opção: ", 0, 7);
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
            ExibirCabecalho("NOVO EMPRÉSTIMO");
            try
            {
                int alunoId = Validador.LerInteiroPositivo("  ID do aluno: ");
                int livroId = Validador.LerInteiroPositivo("  ID do livro: ");

                var emprestimo = _service.RealizarEmprestimo(alunoId, livroId);
                Validador.ExibirSucesso($"Empréstimo registrado! ID: {emprestimo.Id:D4}");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\n  Devolução prevista: {emprestimo.DataPrevistaDevolucao:dd/MM/yyyy}");
                Console.ResetColor();
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
                int id = Validador.LerInteiroPositivo("  ID do empréstimo: ");
                var emp = _service.BuscarEmprestimo(id);
                Console.WriteLine($"\n  Aluno: {emp.AlunoNome}");
                Console.WriteLine($"  Livro: {emp.LivroTitulo}");
                if (emp.Atrasado)
                {
                    Validador.ExibirAviso($"Empréstimo atrasado! {emp.DiasAtraso} dia(s) - Multa: R${emp.MultaPorAtraso:F2}");
                }

                if (Validador.ConfirmarOperacao("  Confirmar devolução?"))
                {
                    _service.DevolverEmprestimo(id);
                    Validador.ExibirSucesso("Devolução registrada com sucesso!");
                }
                else
                    Validador.ExibirAviso("Operação cancelada.");
            }
            catch (Exception ex)
            {
                Validador.ExibirErro(ex.Message);
            }
            Validador.PausarTela();
        }

        private void RenovarEmprestimo()
        {
            ExibirCabecalho("RENOVAR EMPRÉSTIMO");
            try
            {
                int id = Validador.LerInteiroPositivo("  ID do empréstimo: ");
                var emp = _service.BuscarEmprestimo(id);
                Console.WriteLine($"\n  Aluno: {emp.AlunoNome} | Livro: {emp.LivroTitulo}");
                Console.WriteLine($"  Renovações: {emp.NumeroRenovacoes}/{Emprestimo.MaxRenovacoes}");

                bool renovado = _service.RenovarEmprestimo(id);
                if (renovado)
                {
                    emp = _service.BuscarEmprestimo(id);
                    Validador.ExibirSucesso($"Empréstimo renovado! Nova devolução: {emp.DataPrevistaDevolucao:dd/MM/yyyy}");
                }
                else
                    Validador.ExibirAviso("Não foi possível renovar. Limite de renovações atingido ou status inválido.");
            }
            catch (Exception ex)
            {
                Validador.ExibirErro(ex.Message);
            }
            Validador.PausarTela();
        }

        private void ListarEmprestimos(bool somenteAtivos)
        {
            string titulo = somenteAtivos ? "EMPRÉSTIMOS ATIVOS" : "TODOS OS EMPRÉSTIMOS";
            ExibirCabecalho(titulo);
            var lista = somenteAtivos ? _service.ListarEmprestimosAtivos() : _service.ListarEmprestimos();

            if (lista.Count == 0)
                Validador.ExibirAviso("Nenhum empréstimo encontrado.");
            else
            {
                Console.WriteLine($"  Total: {lista.Count} empréstimo(s)\n");
                Console.WriteLine(LINHA_FINA);
                foreach (var e in lista)
                {
                    Console.ForegroundColor = e.Atrasado ? ConsoleColor.Red :
                                              e.Status == StatusEmprestimo.Devolvido ? ConsoleColor.DarkGray :
                                              ConsoleColor.White;
                    Console.WriteLine($"  {e}");
                    Console.ResetColor();
                }
                Console.WriteLine(LINHA_FINA);
            }
            Validador.PausarTela();
        }

        private void EmprestimosDoAluno()
        {
            ExibirCabecalho("EMPRÉSTIMOS POR ALUNO");
            try
            {
                int alunoId = Validador.LerInteiroPositivo("  ID do aluno: ");
                var aluno = _service.BuscarAluno(alunoId);
                var lista = _service.ListarEmprestimosAluno(alunoId);
                Console.WriteLine($"\n  Aluno: {aluno.Nome}");
                Console.WriteLine($"  Total de empréstimos: {lista.Count}\n");
                Console.WriteLine(LINHA_FINA);
                foreach (var e in lista)
                {
                    Console.ForegroundColor = e.Atrasado ? ConsoleColor.Red : ConsoleColor.White;
                    Console.WriteLine($"  {e}");
                    Console.ResetColor();
                }
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
            ExibirCabecalho("EMPRÉSTIMOS ATRASADOS");
            var lista = _service.ListarEmprestimosAtrasados();
            if (lista.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n  Nenhum empréstimo atrasado! Ótimo gerenciamento!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  ATENÇÃO: {lista.Count} empréstimo(s) atrasado(s)!\n");
                Console.ResetColor();
                Console.WriteLine(LINHA_FINA);
                decimal totalMultas = 0;
                foreach (var e in lista)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  {e}");
                    Console.ResetColor();
                    totalMultas += e.MultaPorAtraso;
                }
                Console.WriteLine(LINHA_FINA);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  Total de multas acumuladas: R${totalMultas:F2}");
                Console.ResetColor();
            }
            Validador.PausarTela();
        }

        // ==================== RELATÓRIOS ====================
        private void MenuRelatorios()
        {
            Console.Clear();
            ExibirCabecalho("RELATÓRIOS");
            var alunos = _service.ListarAlunos();
            var livros = _service.ListarLivros();
            var emprestimos = _service.ListarEmprestimos();
            var ativos = _service.ListarEmprestimosAtivos();
            var atrasados = _service.ListarEmprestimosAtrasados();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  ┌─────────────────────────────────────────────────┐");
            Console.WriteLine("  │              RESUMO DO SISTEMA                  │");
            Console.WriteLine("  ├─────────────────────────────────────────────────┤");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"  │  Alunos cadastrados     : {alunos.Count,-5}                   │");
            Console.WriteLine($"  │  Alunos ativos          : {alunos.FindAll(a => a.Ativo).Count,-5}                   │");
            Console.WriteLine($"  │  Livros no acervo       : {livros.Count,-5}                   │");
            Console.WriteLine($"  │  Livros disponíveis     : {_service.ListarLivrosDisponiveis().Count,-5}                   │");
            Console.WriteLine($"  │  Total de empréstimos   : {emprestimos.Count,-5}                   │");
            Console.WriteLine($"  │  Empréstimos ativos     : {ativos.Count,-5}                   │");
            Console.ForegroundColor = atrasados.Count > 0 ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine($"  │  Empréstimos atrasados  : {atrasados.Count,-5}                   │");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  └─────────────────────────────────────────────────┘");
            Console.ResetColor();
            Validador.PausarTela();
        }

        // ==================== DEMO POLIMORFISMO ====================
        private void DemoPolimorfismo()
        {
            Console.Clear();
            ExibirCabecalho("DEMONSTRAÇÃO DE POLIMORFISMO");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  Demonstrando polimorfismo com diferentes objetos da hierarquia:\n");
            Console.ResetColor();

            // Criando objetos de diferentes classes da hierarquia
            var funcionario = new Funcionario(99, "João Gestor", "11122233344",
                new DateTime(1985, 6, 15), "joao@bib.com", "31911111111",
                "FUNC001", "Assistente", 2800m, new DateTime(2018, 3, 1));

            var bibliotecario = new Bibliotecario(100, "Maria Silva", "55566677788",
                new DateTime(1978, 9, 20), "maria@bib.com", "31922222222",
                "BIB001", 4500m, new DateTime(2010, 1, 15), "CRB-6/1234", true);

            var aluno = new Aluno(1, "Pedro Alves", "99988877766",
                new DateTime(2001, 4, 10), "CC2020001", "CC", "pedro@email.com", "31933333333", 2020);

            Console.WriteLine("  ── Método ObterTipo() (polimórfico via Pessoa) ──");
            // Lista de Pessoa - demonstra polimorfismo
            var pessoas = new List<Pessoa> { funcionario, bibliotecario, aluno };
            foreach (var p in pessoas)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"  {p.ObterTipo(),-20}");
                Console.ResetColor();
                Console.WriteLine($"→ {p.Nome}");
            }

            Console.WriteLine("\n  ── Método ObterPermissoes() (polimórfico via Funcionario) ──");
            var funcionarios = new List<Funcionario> { funcionario, bibliotecario };
            foreach (var f in funcionarios)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  [{f.ObterTipo()}] {f.Nome}:");
                Console.ResetColor();
                Console.WriteLine($"  {f.ObterPermissoes()}");
            }

            Console.WriteLine("\n  ── ObterDetalhes() via ItemBiblioteca (polimorfismo) ──");
            var livros = _service.ListarLivros();
            if (livros.Count > 0)
            {
                // Polimorfismo: ItemBiblioteca -> Livro
                ItemBiblioteca item = livros[0];
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\n  Categoria: {item.ObterCategoria()}");
                Console.ResetColor();
                Console.WriteLine($"  {item.ObterDetalhes()}");
            }

            Validador.PausarTela();
        }

        // ==================== AUXILIARES ====================
        private void ExibirCabecalho(string titulo)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine(LINHA);
            Console.WriteLine($"  {titulo.PadLeft((LINHA.Length - 2 + titulo.Length) / 2).PadRight(LINHA.Length - 4)}");
            Console.WriteLine(LINHA);
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}