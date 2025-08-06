namespace CarteirasInvestimento.AppServer
{
    public class CarteiraView
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public decimal ValorToal { get; set; }
        public int AtivoId { get; set; }

        public List<AtivoView> Ativos { get; set; } = new List<AtivoView>();
    }
}
