using AutoMapper;
using CarteirasInvestimento.AppServer.Interfaces;
using CarteirasInvestimento.DataAcess.Entity;
using CarteirasInvestimento.Infra;
using CarteirasInvestimento.Repository.Interfaces;

namespace CarteirasInvestimento.AppServer
{
    public class AtivoAppServe : Notification, IAtivoAppServe
    {
        private readonly IAtivoRepository _ativoRepository;

        private readonly IMapper _mapper;

        public AtivoAppServe(IAtivoRepository ativoRepository, IMapper mapper)
        {
            _ativoRepository = ativoRepository;
            _mapper = mapper;
        }

        public async Task AddAtivoAsync(AtivoView ativoView)
        {
            try
            {
                var ativo = _mapper.Map<Ativo>(ativoView);
                if (ativo != null && ativo.Validate())
                {
                    await _ativoRepository.AddAtivoAsync(ativo);
                }
            }
            catch (Exception ex)
            {
                Notification.Notify($"Erro ao cadastrar ativo. : {ex.Message} ", "AddAtivoAsync");
            }
        }
        public async Task UpdateAtivoAsync(AtivoView ativoView)
        {
            try
            {
                var ativo = await _ativoRepository.GetByIdAsync(ativoView.Id);

                if (ativo == null)
                {
                    Notification.Notify($"Ativo com Id: {ativoView.Id} não foi encontrado.", "UpdateAtivoAsync");
                    return;
                }

                ativo.PrecoUnitario = ativoView.PrecoUnitario;
                ativo.Nome = ativoView.Nome;
                ativo.Codigo = ativoView.Codigo;
                ativo.Tipo = (TipoEnum)ativoView.TipoId;
                ativo.Quantidade = ativoView.Quantidade;

                await _ativoRepository.UpdateAtivoAsync(ativo);
            }
            catch (Exception ex)
            {
                Notification.Notify($"Erro ao atualizar ativo. : {ex.Message} ", "UpdateAtivoAsync");
            }
        }
        public async Task DeleteAtivoAsync(int id)
        {
            try
            {
                if (Extensions.ValidateInt(id, "Id do Ativo é obrigatório."))
                    return;

                var ativo = await _ativoRepository.GetByClienteIdAsync(id);
                await _ativoRepository.DeleteAtivoAsync(ativo);
            }
            catch (Exception ex)
            {
                Notification.Notify($"Erro ao deletar ativo. : {ex.Message} ", "DeleteAtivoAsync");
            }
        }


        public async Task<Ativo> GetByClienteIdAsync(int id)
        {
            return await _ativoRepository.GetByClienteIdAsync(id);
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

        public async Task<AtivoView> GetByIdAsync(int id)
        {
            var ativo = await _ativoRepository.GetByIdAsync(id);
            var ativosView = _mapper.Map<AtivoView>(ativo);
            return ativosView;
        }

        public async Task<IEnumerable<AtivoView>> GetAtivosAllAsync()
        {
            var ativos = await _ativoRepository.GetAllAsync();
            var listaAtivosView = _mapper.Map<List<AtivoView>>(ativos);
            return listaAtivosView;
        }
    }
}
