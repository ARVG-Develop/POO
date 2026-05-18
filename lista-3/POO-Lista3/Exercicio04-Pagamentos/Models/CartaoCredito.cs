namespace Exercicio04_Pagamentos;

public class CartaoCredito : Pagamento, IComprovante
{
    public CartaoCredito(double valor) : base(valor)
    {
    }

    public override void ProcessarPagamento()
    {
        Console.WriteLine("Estou no método ProcessarPagamento da classe CartaoCredito");
    }

    public void EmitirComprovante()
    {
        Console.WriteLine("Estou no método EmitirComprovante da classe CartaoCredito");
    }
}
