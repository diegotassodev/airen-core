using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Airen.Contracts.DTOs;
using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

public class PersonagemRepository : IRepository<Personagem, ReadPersonagemDto, UpdatePersonagemDto>
{
    private AirenContext _context;

    public PersonagemRepository(AirenContext context) 
    {
        _context = context;
    }

    public ReadPersonagemDto Create(Personagem personagem)
    {
        _context.Personagens!.Add(personagem);
        _context.SaveChanges();
        return personagem.MapToReadPersonagem();
    }

    public IEnumerable<ReadPersonagemDto> ReadAll() 
    {
        return _context.Personagens!
            .Include(p => p.Potentia)
            .Include(p => p.Potentia!).ThenInclude(p => p.Habilidades)
            .Include(p => p.Versoes!).ThenInclude(v => v.Hobbies)
            .Include(p => p.Versoes!).ThenInclude(v => v.Tecnicas)
            .Include(p => p.Versoes!).ThenInclude(v => v.Volumes)
            .Include(p => p.Afiliacoes)
            .Include(p => p.Parentes!).ThenInclude(p => p.Parente)
            .Select(p => p.MapToReadPersonagem()).ToList();
    }

    public ReadPersonagemDto? ReadById(int id) 
    {
        var personagem =_context.Personagens!.FirstOrDefault(p => p.Id == id);
        if (personagem is not null) return personagem.MapToReadPersonagem();
        else return null;
    }

    public Personagem? ReadEntityById(int id) 
    {
        var personagem =_context.Personagens!.FirstOrDefault(p => p.Id == id);
        if (personagem is not null) return personagem;
        else return null;
    }

    public ReadPersonagemDto? Update(UpdatePersonagemDto personagemDto, Personagem personagem)
    {
        var novoPersonagem = personagemDto.MapToPersonagem(personagem);
        _context.SaveChanges();
        return novoPersonagem.MapToReadPersonagem();
    }

    public void Delete(Personagem personagem)
    {
        var parentescos = _context.Parentescos!
            .Where(item => item.IdPersonagem == personagem.Id || item.IdParente == personagem.Id);
        _context.Parentescos!.RemoveRange(parentescos);
        _context.Personagens!.Remove(personagem);
        _context.SaveChanges();
    }
}