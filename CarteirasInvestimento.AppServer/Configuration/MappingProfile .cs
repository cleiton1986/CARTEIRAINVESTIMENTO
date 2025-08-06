using AutoMapper;
using CarteirasInvestimento.AppServer.ViewModel;
using CarteirasInvestimento.DataAcess.Entity;
using CarteirasInvestimento.Infra;

namespace CarteirasInvestimento.AppServer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {


            CreateMap<Carteira, CarteiraCadastroView>()
                 .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId));

            CreateMap<CarteiraCadastroView, Carteira>()
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.DataCadastro, opt => opt.Ignore())
                .ForMember(dest => dest.AtivoId, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());



            //.ForMember(dest => dest.Ativos, opt => opt.MapFrom(src => src.Ativos.Select(a => new AtivoCarteiraView
            //{
            //    Quantidade = a.Quantidade,
            //    Codigo = a.Codigo
            //}).ToList()));

            CreateMap<Carteira, CarteiraView>()
              .ForMember(dest => dest.ValorToal,
                    opt => opt.MapFrom(src => Convert.ToDecimal(src.Ativos.Sum(a => a.PrecoUnitario * a.Quantidade)))
              );
            //.ForMember(dest => dest.Ativos, 
            //    opt => opt.MapFrom(src => src.Ativos.Select(a => new AtivoCarteiraView
            //    {
            //        Quantidade = a.Quantidade,
            //        Codigo = a.Codigo
            //    }).ToList())
            //);


            CreateMap<CarteiraView, Carteira>()
              .ForMember(dest => dest.Id, opt => opt.Ignore())
              .ForMember(dest => dest.AtivoId, opt => opt.Ignore())
              .ForMember(dest => dest.ClienteId, opt => opt.Ignore())
              .ForMember(dest => dest.DataCadastro, opt => opt.Ignore())
              .ForMember(dest => dest.Id, opt => opt.Ignore());


            CreateMap<Ativo, AtivoCarteiraView>()
              .ForMember(dest => dest.Quantidade, opt => opt.MapFrom(src => src.Quantidade))
              .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Codigo));


            CreateMap<AtivoCarteiraView, Ativo>()
              .ForMember(dest => dest.Quantidade, opt => opt.MapFrom(src => src.Quantidade))
              .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Codigo))
              .ForMember(dest => dest.Tipo, opt => opt.Ignore())
              .ForMember(dest => dest.Id, opt => opt.Ignore())
              .ForMember(dest => dest.Nome, opt => opt.Ignore())
              .ForMember(dest => dest.PrecoUnitario, opt => opt.Ignore());

            CreateMap<Ativo, AtivoView>()
              //.ForMember(dest => dest.Quantidade, opt => opt.MapFrom(src => src.Quantidade))
              //.ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Codigo))
              //.ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
              //.ForMember(dest => dest.PrecoUnitario, opt => opt.MapFrom(src => src.PrecoUnitario))
              .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo.EnumDescription()))
              .ForMember(dest => dest.TipoId, opt => opt.MapFrom(src => src.Tipo.EnumToInt()));

            CreateMap<AtivoView, Ativo>()
              //.ForMember(dest => dest.Quantidade, opt => opt.MapFrom(src => src.Quantidade))
              //.ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Codigo))
              //.ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
              //.ForMember(dest => dest.PrecoUnitario, opt => opt.MapFrom(src => src.PrecoUnitario))
              .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => (TipoEnum)src.TipoId));


            CreateMap<Cliente, ClienteView>()
             .ForMember(dest => dest.DataNascimento, opt => opt.MapFrom(src => src.DataNascimento.ConvertDateToString()));


            CreateMap<ClienteView, Cliente>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.DataNascimento, opt => opt.MapFrom(src => src.DataNascimento.ConvertStringToDate()))
             .ForMember(dest => dest.DataCadastro, opt => opt.Ignore())
             .ForMember(dest => dest.Carteiras, opt => opt.Ignore());

            CreateMap<ClienteCadastroView, Cliente>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.DataNascimento, opt => opt.MapFrom(src => src.DataNascimento.ConvertStringToDate()))
             .ForMember(dest => dest.DataCadastro, opt => opt.Ignore())
             .ForMember(dest => dest.Carteiras, opt => opt.Ignore());

           CreateMap<ClienteEditarView, Cliente>()
            .ForMember(dest => dest.DataNascimento, opt => opt.MapFrom(src => src.DataNascimento.ConvertStringToDate()))
            .ForMember(dest => dest.DataCadastro, opt => opt.Ignore())
            .ForMember(dest => dest.Carteiras, opt => opt.Ignore());
            
        }
    }
}
