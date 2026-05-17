namespace Exercicio01_Funcionarios;

abstract class Funcionario
{
    public string Nome { get; set; }
    public double SalarioBase { get; set; }

    public Funcionario(string nome, double salarioBase)
    {
        Nome = nome;
        SalarioBase = salarioBase;
    }

    public abstract double CalcularSalario();
}
