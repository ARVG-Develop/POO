using Exercicio04_Pagamentos;

CartaoCredito cartao = new CartaoCredito(150.00);
Pix pix = new Pix(89.90);

List<Pagamento> pagamentos = new List<Pagamento>();
pagamentos.Add(cartao);
pagamentos.Add(pix);

foreach (Pagamento p in pagamentos)
{
    p.ProcessarPagamento();

    if (p is IComprovante comprovante)
    {
        comprovante.EmitirComprovante();
    }

    Console.WriteLine();
}
