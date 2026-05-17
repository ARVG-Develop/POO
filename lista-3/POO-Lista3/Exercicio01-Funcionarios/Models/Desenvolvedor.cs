namespace Exercicio01_Funcionarios;

class Desenvolvedor : Funcionario, IBonificacao
{
    public Desenvolvedor(string nome, double salarioBase) : base(nome, salarioBase)
    {
    }

    public override double CalcularSalario()
    {
        Console.WriteLine("Estou no método CalcularSalario() da classe Desenvolvedor");
        return SalarioBase + CalcularBonus();
    }

    public double CalcularBonus()
    {
        Console.WriteLine("Estou no método CalcularBonus() da classe Desenvolvedor");
        return SalarioBase * 0.20;
    }
}
