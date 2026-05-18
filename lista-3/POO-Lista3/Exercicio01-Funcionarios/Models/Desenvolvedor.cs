namespace Exercicio01_Funcionarios;

public class Desenvolvedor : Funcionario, IBonificacao
{
    public Desenvolvedor(string nome, double salarioBase) : base(nome, salarioBase)
    {
    }

    public override double CalcularSalario()
    {
        return SalarioBase + CalcularBonus();
    }

    public double CalcularBonus()
    {
        return SalarioBase * 0.20;
    }
}
