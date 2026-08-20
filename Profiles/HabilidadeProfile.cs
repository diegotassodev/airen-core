using AirenApi.Data.DTOs;
using AirenApi.Models;
using AutoMapper;
namespace AirenApi.Profiles;

public class HabilidadeProfile : Profile {
    public HabilidadeProfile() {
        CreateMap<CreateHabilidadeDto, Habilidade>();
        CreateMap<UpdateHabilidadeDto, Habilidade>();
        CreateMap<Habilidade, ReadHabilidadeDto>()
            .ForMember(habilidadeDto => habilidadeDto.NomePotentia, opt => opt.MapFrom(habilidade => habilidade.Potentia!.Nome));;
    }
}