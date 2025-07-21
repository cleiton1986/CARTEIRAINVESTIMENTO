using CarteirasInvestimento.Infra;

namespace CarteirasInvestimento.DataAcess.Entity
{
    public class Ativo
    {
        public Ativo(){ }
        public int Id { get; set; }
        public int CarteiraId { get; set; }
        public TipoEnum Tipo { get; set; }
        public int Quantidade { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal PrecoUnitario { get; set; }
        public Carteira Carteira { get; set; }
        public bool Validate()
        {
            Extensions.ValidateInt(this.Quantidade, "Quantidade deve ser maior que zero.");
            Extensions.ValidateString(this.Codigo, "Código deve ser maior que zero.");
            return Notification.IsValid();
        }
    }

}
