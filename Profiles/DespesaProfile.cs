using ApiFinanceira.Dtos;
using ApiFinanceira.Dtos.Responses;
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

            CreateMap<Tag, TagResponseDto>();

            CreateMap<Categoria, CategoriaResponseDto>();

            CreateMap<Despesa, DespesaResponseDto>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));


        }
    }
}
