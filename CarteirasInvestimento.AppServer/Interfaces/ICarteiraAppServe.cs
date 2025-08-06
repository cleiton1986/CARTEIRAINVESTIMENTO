namespace CarteirasInvestimento.AppServer.Interfaces
{
    public interface ICarteiraAppServe
    {
        Task<CarteiraView> GetByClienteIdAsync(int id);
        Task AddAsync(CarteiraCadastroView view);
        Task UpdateAsync(CarteiraCadastroView view);
        Task DeleteAsync(CarteiraCadastroView view);
        Task<List<CarteiraView>> GetAllAsync();
    }
}
