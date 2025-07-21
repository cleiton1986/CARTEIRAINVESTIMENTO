using CarteirasInvestimento.DataAcess.Entity;
using CarteirasInvestimento.Repository.Interfaces;

namespace CarteirasInvestimento.Repository
{
    public class AtivoRepository : IAtivoRepository
    {
        public AtivoRepository(){}
        public async Task<IEnumerable<Ativo>> GetAllAsync()
        {

            var listaAtivos = AtivoFactory.DeserializeListaAtivo(FilePath());
            return await Task.Run(() => { return listaAtivos.ToList(); });
        }

        public async Task AddAsync(List<Ativo> listaAtivo)
        {
            await Task.Run(async () =>
            {

                var listaAtivoCadastrada = await GetAllAsync();
                listaAtivo.AddRange(listaAtivoCadastrada);
               
                var ativoJson = AtivoFactory.SerializeAtivo(listaAtivo);
                string json = File.ReadAllText(FilePath());
                File.WriteAllText(FilePath(), ativoJson);

            });
        }

        public async Task<IEnumerable<Ativo>> GetByClienteIdAsync(int id)
        {
            var listaAtivoByCliente = AtivoFactory.DeserializeListaAtivo(FilePath());
            return await Task.Run(() => { return listaAtivoByCliente.Where(x => x.Carteira.ClienteId == id).ToList(); });
        }
        private string FilePath()
        {
            return Path.GetFullPath("Dados\\ativos.json");
        }
    }
}
