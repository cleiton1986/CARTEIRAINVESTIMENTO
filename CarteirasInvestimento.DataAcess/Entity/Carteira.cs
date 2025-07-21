using CarteirasInvestimento.Infra;

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
        public IEnumerable<Ativo> Ativos { get; set; }

        public bool Validate()
        {
            Extensions.ValidateInt(this.ClienteId, "ClienteId deve ser maior que zero.");
            return Notification.IsValid();
        }

    }
}
