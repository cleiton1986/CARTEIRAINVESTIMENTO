using CarteirasInvestimento.Infra;

namespace CarteirasInvestimento.DataAcess.Entity
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Logadouro { get; set; }
        public int Numero { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public DateTime DataCadastro
        {
            get
            {
                return DateTime.Now;
            }
            private set;
        }
        public DateTime DataNascimento { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public ICollection<Carteira> Carteiras { get; set; } = new List<Carteira>();
        public bool Validate()
        {
            Extensions.ValidateString(this.Nome, "Nome é obrigatório.");
            Extensions.ValidateEmail(this.Email, "Email inválido.");
            Extensions.ValidateString(this.Telefone, "Telefone é obrigatório.");
            Extensions.ValidateCep(this.Cep, "Cep é obrigatório.");
            Extensions.ValidateInt(this.Numero, "Número é obrigatório.");
            Extensions.ValidateString(this.Logadouro, "Logadouro é obrigatório.");
            Extensions.ValidateString(this.Bairro, "Bairro é obrigatório.");
            Extensions.ValidateDate(this.DataNascimento, "DataNacimento é obrigatória.");
            return Notification.IsValid();
        }
    }

}
