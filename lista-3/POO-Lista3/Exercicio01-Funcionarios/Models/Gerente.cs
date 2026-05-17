namespace Exercicio01_Funcionarios;

class Gerente : Funcionario, IBonificacao
{
    public Gerente(string nome, double salarioBase) : base(nome, salarioBase)
    {
    }

    public override double CalcularSalario()
    {
        Console.WriteLine("Estou no método CalcularSalario() da classe Gerente");
        return SalarioBase + CalcularBonus();
    }

    public double CalcularBonus()
    {
        Console.WriteLine("Estou no método CalcularBonus() da classe Gerente");
        return SalarioBase * 0.30;
    }
}
