using ApiFinanceira.Dtos;
using ApiFinanceira.Model;
using AutoMapper;
namespace ApiFinanceira.Profiles
{
    public class DespesaProfile : Profile
    {
        public DespesaProfile()
        {
            CreateMap<DespesaDto, Despesa>().ForMember(dest => dest.Situacao,
                opt => opt.MapFrom(src => "pendente"));

            CreateMap<DespesasUpdateDto, Despesa>();

            //CreateMap<DespesasUpdateDto, Despesa>()
            //    .ForMember(
            //        dest => dest.DataPagamento,
            //        opt => opt.MapFrom(
            //            src => src.DataPagamento
            //        )
            //    );
        }
    }
}
