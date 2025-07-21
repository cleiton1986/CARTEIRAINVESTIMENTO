namespace CarteirasInvestimento.AppServer.Interfaces
{
    public interface IAtivoAppServe
    {
        Task<IEnumerable<AtivoPesquisaView>> GetAllAsync();
    }
}
