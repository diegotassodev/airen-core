using AirenApi.Data.DTOs;
using AirenApi.Models;
using AutoMapper;
namespace AirenApi.Profiles;

public class TecnicaProfile : Profile {
    public TecnicaProfile() {
        CreateMap<CreateTecnicaDto, Tecnica>();
        CreateMap<UpdateTecnicaDto, Tecnica>();
        CreateMap<Tecnica, ReadTecnicaDto>()
            .ForMember(t => t.NomePersonagem, opt => opt.MapFrom(t => t.Versao!.Personagem!.Nome));
    }
}