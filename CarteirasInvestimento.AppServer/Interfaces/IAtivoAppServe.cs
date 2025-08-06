using CarteirasInvestimento.DataAcess.Entity;

namespace CarteirasInvestimento.AppServer.Interfaces
{
    public interface IAtivoAppServe
    {
        Task<IEnumerable<AtivoPesquisaView>> GetAllAsync();
        Task<AtivoView> GetByIdAsync(int id);
        Task AddAtivoAsync(AtivoView ativoView);
        Task UpdateAtivoAsync(AtivoView ativoView);
        Task DeleteAtivoAsync(int id);
        Task<IEnumerable<AtivoView>> GetAtivosAllAsync();
    }
}
