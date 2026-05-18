using Exercicio01_Funcionarios;

Gerente g1 = new Gerente("Carlos Silva", 5000.00);
Gerente g2 = new Gerente("Ana Souza", 6000.00);
Desenvolvedor d1 = new Desenvolvedor("João Lima", 4000.00);
Desenvolvedor d2 = new Desenvolvedor("Maria Oliveira", 4500.00);

List<Funcionario> funcionarios = new List<Funcionario>();
funcionarios.Add(g1);
funcionarios.Add(g2);
funcionarios.Add(d1);
funcionarios.Add(d2);

Console.WriteLine("      RELATÓRIO DE FUNCIONÁRIOS         \n");

foreach (Funcionario f in funcionarios)
{
    Console.WriteLine($"--- Funcionário: {f.Nome} ---");

    double salario = f.CalcularSalario();

    if (f is IBonificacao bonificado)
    {
        double bonus = bonificado.CalcularBonus();
        Console.WriteLine($"Nome:          {f.Nome}");
        Console.WriteLine($"Salário Final: R$ {salario:F2}");
        Console.WriteLine($"Bônus:         R$ {bonus:F2}");
    }

    Console.WriteLine();
}
