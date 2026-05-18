namespace Exercicio01_Funcionarios;

public class Desenvolvedor : Funcionario, IBonificacao
{
    public Desenvolvedor(string nome, double salarioBase) : base(nome, salarioBase)
    {
    }

    public override double CalcularSalario()
    {
        Console.WriteLine("Estou no método CalcularSalario da classe Desenvolvedor");
        return 0;
    }

    public double CalcularBonus()
    {
        Console.WriteLine("Estou no método CalcularBonus da classe Desenvolvedor");
        return 0;
    }
}
