namespace CarteirasInvestimento.AppServer
{
    public class AtivoView
    {
        public int Id { get; set; }
        public int TipoId { get; set; }
        public int CarteiraId { get; set; }
        public string Tipo { get; set; }
        public int Quantidade { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal PrecoUnitario { get; set; }

    }
}


