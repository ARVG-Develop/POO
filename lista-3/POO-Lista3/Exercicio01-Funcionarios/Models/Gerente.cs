namespace Exercicio01_Funcionarios;

public class Gerente : Funcionario, IBonificacao
{
    public Gerente(string nome, double salarioBase) : base(nome, salarioBase)
    {
    }

    public override double CalcularSalario()
    {
        Console.WriteLine("Estou no método CalcularSalario da classe Gerente");
        return 0;
    }

    public double CalcularBonus()
    {
        Console.WriteLine("Estou no método CalcularBonus da classe Gerente");
        return 0;
    }
}
