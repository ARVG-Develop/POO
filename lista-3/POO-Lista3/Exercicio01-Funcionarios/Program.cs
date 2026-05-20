using Exercicio01_Funcionarios;

Gerente g1 = new Gerente("Carlos Silva", 5000.00);
Desenvolvedor d1 = new Desenvolvedor("João Lima", 4000.00);

List<Funcionario> funcionarios = new List<Funcionario>();
funcionarios.Add(g1);
funcionarios.Add(d1);

Console.WriteLine("=== RELATÓRIO DE FUNCIONÁRIOS ===\n");
Console.WriteLine("=== RELATÓRIO DE FUNCIONÁRIOS ===\n");
foreach (Funcionario f in funcionarios)
{
    Console.WriteLine($"--- Funcionário: {f.Nome} ---");
    f.CalcularSalario();

    if (f is IBonificacao bonificado)
    {
        bonificado.CalcularBonus();
    }

    Console.WriteLine();
}
