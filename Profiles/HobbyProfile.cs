using AirenApi.Data.DTOs;
using AirenApi.Models;
using AutoMapper;
namespace AirenApi.Profiles;

public class HobbyProfile : Profile {
    public HobbyProfile() {
        CreateMap<CreateHobbyDto, Hobby>();
        CreateMap<UpdateHobbyDto, Hobby>();
        CreateMap<Hobby, ReadHobbyDto>()
            .ForMember(h => h.NomePersonagem, opt => opt.MapFrom(h => h.Versao!.Personagem!.Nome));
    }
}