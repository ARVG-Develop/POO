namespace Exercicio04_Pagamentos;

public abstract class Pagamento
{
    public double Valor { get; set; }

    public Pagamento(double valor)
    {
        Valor = valor;
    }

    public abstract void ProcessarPagamento();
}
