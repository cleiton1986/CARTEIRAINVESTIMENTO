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
        public CarteiraAppServe(ICarteiraRepository carteiraRepository,
                                IAtivoRepository ativoRepository
            )
        {
            _carteiraRepository = carteiraRepository;
            _ativoRepository = ativoRepository;
        }

        public async Task<CarteiraView> GetByClienteIdAsync(int id)
        {
            var carteiraView = new CarteiraView();
        
            try
            {
                var listaCarteira = await _carteiraRepository.GetByCarteirasIdAsync(id);
                var listaAtivos = await _ativoRepository.GetByClienteIdAsync(id);
     
                if (listaCarteira.Any())
                {
                    listaCarteira.ToList().ForEach(c =>
                    {
                        carteiraView.Ativos.AddRange(c.Ativos.Select(at => new AtivoCarteiraView
                        {
                            Quantidade = at.Quantidade,
                            Codigo = at.Codigo
                        }).ToList());

                        carteiraView.ClienteId = c.ClienteId;
                        carteiraView.ValorToal = Convert.ToDecimal(listaAtivos.Where(a => a.Id == c.AtivoId).Sum(at => at.PrecoUnitario * at.Quantidade));
                    });
                }
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
                var carteira = new Carteira
                {
                    ClienteId = view.ClienteId,
                    AtivoId = Convert.ToInt32(Extensions.GetNumero()),
                    Ativos = view.Ativos.Select(a => new Ativo
                    {
                        Quantidade = a.Quantidade,
                        Codigo = a.Codigo

                    }).ToList()
                };


                if (carteira.Validate() && carteira.Ativos.Any(a => a.Validate()))
                {
                    if (!await _carteiraRepository.ExistsRecordsByAsync())
                    {
                        await _carteiraRepository.AddAsync(carteira);
                        await AddAtivo(carteira);
                    }  
                    else
                        await UpdateAsync(carteira);
                }
            }
            catch (Exception ex)
            {
                Notification.Notify($"Erro ao criar carteira. : {ex.Message} ", "AddAsync");
            }
        }
        private async Task UpdateAsync(Carteira carteira)
        {
            try
            {
                var listaCarteira = await _carteiraRepository.GetAllAsync();
                var listaCarteiraUpdate = listaCarteira.ToList();
                if (listaCarteiraUpdate.Count > 0)
                {
                    listaCarteiraUpdate.Add(carteira);
                    await _carteiraRepository.UpdateAsync(listaCarteiraUpdate);
                    await AddAtivo(carteira);
                }
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
                Ativos = c.Ativos.Select(a => new AtivoCarteiraView
                {
                    Quantidade = a.Quantidade,
                    Codigo = a.Codigo
                }).ToList()
            }).ToList();
        }

        private async Task AddAtivo(Carteira carteira)
        {
  
            var _carteira = carteira;

            carteira.Ativos.Select((a) => 
            {
                a.PrecoUnitario = Convert.ToDecimal(Extensions.GetNumero());
                a.Id = carteira.AtivoId;
                a.CarteiraId = Convert.ToInt32(Extensions.GetNumero());
                a.Carteira = new Carteira { ClienteId = _carteira .ClienteId, AtivoId = carteira.AtivoId};
                a.Nome = "CDBXP";
                a.Tipo = TipoEnum.CDB;

                return a;
           }).ToList();

            await _ativoRepository.AddAsync(carteira.Ativos.ToList());
        }
    }
}
