namespace CarteirasInvestimento.AppServer
{
    public class CarteiraCadastroView
    {
        public int ClienteId { get; set; }
        public List<AtivoCarteiraView> Ativos { get; set; } = new List<AtivoCarteiraView>();
    }
}
