namespace Exercicio04_Pagamentos;

public class CartaoCredito : Pagamento, IComprovante
{
    public CartaoCredito(double valor) : base(valor)
    {
    }

    public override void ProcessarPagamento()
    {
        Console.WriteLine($"Processando pagamento de R$ {Valor:F2} no Cartão de Crédito.");
    }

    public void EmitirComprovante()
    {
        Console.WriteLine($"Comprovante: Pagamento de R$ {Valor:F2} realizado no Cartão de Crédito.");
    }
}
