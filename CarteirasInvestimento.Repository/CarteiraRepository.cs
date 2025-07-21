using CarteirasInvestimento.DataAcess.Entity;
using Microsoft.Extensions.Configuration;
using System;

namespace CarteirasInvestimento.Repository
{
    public class CarteiraRepository : ICarteiraRepository
    {
        private readonly IConfiguration _configuration;

        public CarteiraRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task AddAsync(Carteira carteira)
        {
            await Task.Run(() =>
            {
                var listaCarteira = new List<Carteira>();
                listaCarteira.Add(carteira);
                var carteiraJson = CarteiraFactory.SerializeCarteira(listaCarteira);
      
                var filePath = FilePath();
                string json = File.ReadAllText(filePath);

                File.WriteAllText(filePath, carteiraJson);
            });

        }

        public async Task UpdateAsync(List<Carteira> listaCarteira)
        {
            await Task.Run(() =>
            {
                string json = File.ReadAllText(FilePath());
                File.WriteAllText(FilePath(), CarteiraFactory.SerializeCarteira(listaCarteira));
            });

        }
        public async Task<Carteira> GetByCarteiraIdAsync(int id)
        {
            var listaCarteira = CarteiraFactory.DeserializeCarteira(FilePath());
            return await Task.Run(() => { return listaCarteira.FirstOrDefault(x => x.ClienteId == id); });
        }
        public async Task<IEnumerable<Carteira>> GetByCarteirasIdAsync(int id)
        {
            var listaCarteira = CarteiraFactory.DeserializeCarteira(FilePath());
            return await Task.Run(() => { return listaCarteira.FindAll(x => x.ClienteId == id); });
        }
        public async Task<IEnumerable<Carteira>> GetAllAsync()
        {
            var listaCarteira = CarteiraFactory.DeserializeListaCarteira(FilePath());
            return await Task.Run(() => { return listaCarteira.ToList(); });
        }

        public async Task<bool> ExistsRecordsByAsync()
        {
            var listaCarteira = CarteiraFactory.DeserializeListaCarteira(FilePath());
            var exists = listaCarteira.Any();
            return await Task.Run(() => { return exists; });
        }

        private string FilePath()
        {
            return Path.GetFullPath("Dados\\carteiras.json");
        }
    }
}
