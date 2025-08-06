namespace CarteirasInvestimento.AppServer.ViewModel
{
    public class ClienteView
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
        public string DataNascimento { get; set; }
        public List<CarteiraView> Carteiras { get; set; } = new List<CarteiraView>();
    }
}
