namespace CarteirasInvestimento.AppServer
{
    public class CarteiraView
    {
        public int ClienteId { get; set; }
        public decimal ValorToal { get; set; }
        public List<AtivoCarteiraView> Ativos { get; set; } = new List<AtivoCarteiraView>();
    }
}
