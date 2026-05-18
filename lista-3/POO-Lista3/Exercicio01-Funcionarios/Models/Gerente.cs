namespace Exercicio01_Funcionarios;

public class Gerente : Funcionario, IBonificacao
{
    public Gerente(string nome, double salarioBase) : base(nome, salarioBase)
    {
    }

    public override double CalcularSalario()
    {
        return SalarioBase + CalcularBonus();
    }

    public double CalcularBonus()
    {
        return SalarioBase * 0.30;
    }
}
