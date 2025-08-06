using CarteirasInvestimento.Infra;

namespace CarteirasInvestimento.DataAcess.Entity
{
    public class Ativo
    {
        public Ativo(){ }
        public int Id { get; set; }
        public TipoEnum Tipo { get; set; }
        public int Quantidade { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal PrecoUnitario { get; set; }
        public ICollection<Carteira> Carteiras { get; set; } = new List<Carteira>();
        public bool Validate()
        {
            Extensions.ValidateString(this.Nome, "Nome deve ser preenchido.");
            Extensions.ValidateInt(this.Quantidade, "Quantidade deve ser maior que zero.");
            Extensions.ValidateInt((int)this.Tipo, "Tipo do ativo deve ser preenchido.");
            Extensions.ValidateString(this.Codigo, "Código deve ser maior que zero.");
            Extensions.ValidateDecimal(this.PrecoUnitario, "Preço unitário deve ser preenchido.");
            return Notification.IsValid();
        }
    }

}
