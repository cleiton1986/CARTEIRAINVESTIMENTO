using CarteirasInvestimento.AppServer.Interfaces;
using CarteirasInvestimento.Infra;
using CarteirasInvestimento.Repository.Interfaces;

namespace CarteirasInvestimento.AppServer
{
    public class AtivoAppServe : Notification, IAtivoAppServe
    {
        private readonly IAtivoRepository _ativoRepository;

        public AtivoAppServe(IAtivoRepository ativoRepository)
        {
            _ativoRepository = ativoRepository;
        }

        public async Task<IEnumerable<AtivoPesquisaView>> GetAllAsync()
        {
            var listaAtivosView = new List<AtivoPesquisaView>();

            try
            {
                var listaAtivos = await _ativoRepository.GetAllAsync();

                listaAtivos.ToList().Select(selector => new AtivoPesquisaView
                {  
                    Codigo = selector.Codigo,
                    Tipo = selector.Tipo.ToString(),
                    Nome = selector.Nome,
                    PrecoUnitario = selector.PrecoUnitario,
                }).ToList().TrueForAll(item =>
                {
                    listaAtivosView.Add(item);
                    return true;
                });
                
            }
            catch (Exception ex)
            {
                Notification.Notify($"Erro ao buscar ativos. : {ex.Message} ");
            }

            return listaAtivosView;
        }


    }
}
