using AirenApi.Data.DTOs;
using AirenApi.Models;
using AutoMapper;
namespace AirenApi.Profiles;

public class ParentescoProfile : Profile {
    public ParentescoProfile() {
        CreateMap<CreateParentescoDto, Parentesco>();
        CreateMap<UpdateParentescoDto, Parentesco>();
        CreateMap<Parentesco, ReadParentescoDto>()  
             .ForMember(parenteDto => parenteDto.NomePersonagem, opt => opt.MapFrom(parente => parente.Personagem!.Nome))
             .ForMember(parenteDto => parenteDto.NomeParente, opt => opt.MapFrom(parente => parente.Parente!.Nome));
        
    }
}