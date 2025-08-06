using AutoMapper;
using CarteirasInvestimento.AppServer.Interfaces;
using CarteirasInvestimento.DataAcess.Entity;
using CarteirasInvestimento.Infra;
using CarteirasInvestimento.Repository;
using CarteirasInvestimento.Repository.Interfaces;

namespace CarteirasInvestimento.AppServer
{
    public class CarteiraAppServe : ICarteiraAppServe
    {
        private readonly ICarteiraRepository _carteiraRepository;
        private readonly IAtivoRepository    _ativoRepository;
        private readonly IMapper _mapper;
        public CarteiraAppServe(ICarteiraRepository carteiraRepository,
                                IAtivoRepository ativoRepository,
                                IMapper mapper
            )
        {
            _carteiraRepository = carteiraRepository;
            _ativoRepository = ativoRepository;
            _mapper = mapper;
        }

        public async Task<CarteiraView> GetByClienteIdAsync(int id)
        {
            var carteiraView = new CarteiraView();
        
            try
            {
                var carteira = await _carteiraRepository.GetByCarteirasIdAsync(id);
                var ativos = await _ativoRepository.GetByClienteIdAsync(id);

                if (carteira == null)
                {
                    Notification.Notify($"Carteira não encontrada para o Cliente com Id: {id}", "GetByClienteIdAsync");
                    return carteiraView;
                }

                carteiraView = _mapper.Map<CarteiraView>(carteira);

                //carteiraView.Ativos.AddRange(c.Ativos.Select(at => new AtivoCarteiraView
                //{
                //    Quantidade = at.Quantidade,
                //    Codigo = at.Codigo
                //}).ToList());

               // carteiraView.ClienteId = carteira.ClienteId;
               // carteiraView.ValorToal = Convert.ToDecimal(ativos.Where(a => a.Id == c.AtivoId).Sum(at => at.PrecoUnitario * at.Quantidade));
            }
            catch (Exception ex)
            {
                Notification.Notify($"Erro ao consultar carteira por Id do Cliente: {ex.Message} ", "GetByClienteIdAsync");
            }
         
            return carteiraView;
        }

        public async Task AddAsync(CarteiraCadastroView view)
        {
            try
            {

                var carteira = _mapper.Map<Carteira>(view);

                if (carteira.Ativos.Any(a => a.Validate()))
                {
                    await _carteiraRepository.AddAsync(carteira);
                }
            }
            catch (Exception ex)
            {
                Notification.Notify($"Erro ao criar carteira. : {ex.Message} ", "AddAsync");
            }
        }
        public async Task UpdateAsync(CarteiraCadastroView view)
        {
            try
            {



                //if (carteira.Validate() && carteira.Ativos.Any(a => a.Validate()))
                //{
                //    if (!await _carteiraRepository.ExistsRecordsByAsync())
                //    {

                //        await _carteiraRepository.UpdateAsync(carteira);
                //    }
                //}

                //var listaCarteira = await _carteiraRepository.GetByCarteiraIdAsync(carteira.Id);
                //if (listaCarteiraUpdate.Count > 0)
                //{
                //    listaCarteiraUpdate.Add(carteira);
                //    await _carteiraRepository.UpdateAsync(carteira);
                //}
            }
            catch (Exception ex)
            {

                Notification.Notify($"Erro ao criar carteira. : {ex.Message} ", "AddAsync");
            }

        }

        public async Task<List<CarteiraView>> GetAllAsync()
        {
            var listaCarteira = await _carteiraRepository.GetAllAsync();
            return listaCarteira.Select(c => new CarteiraView
            {
                ClienteId = c.ClienteId,
                //Ativos = c.Ativos.Select(a => new AtivoCarteiraView
                //{
                //    Quantidade = a.Quantidade,
                //    Codigo = a.Codigo
                //}).ToList()
            }).ToList();
        }

        public async Task DeleteAsync(CarteiraCadastroView view)
        {
            var listaCarteira = await _carteiraRepository.GetByCarteiraIdAsync(view.ClienteId);
            await _carteiraRepository.DeletarAsync(listaCarteira);
        }
    }
}
