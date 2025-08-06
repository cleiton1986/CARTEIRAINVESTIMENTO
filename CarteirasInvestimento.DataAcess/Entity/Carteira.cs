namespace CarteirasInvestimento.DataAcess.Entity
{
    public class Carteira
    {
        public Carteira()
        {
          this.Ativos = new List<Ativo>();
        }
        public int Id { get; set; }
        public int AtivoId { get; set; }
        public int ClienteId { get; set; }
        public ICollection<Ativo> Ativos { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime DataCadastro
        {
            get { return DateTime.Now; } private set;
        }
      

    }
}
