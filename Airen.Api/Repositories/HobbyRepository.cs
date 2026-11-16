using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Airen.Contracts.DTOs;
using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

public class HobbyRepository : IRepository<Hobby, ReadHobbyDto, UpdateHobbyDto>
{
    private AirenContext _context;

    public HobbyRepository(AirenContext context) 
    {
        _context = context;
    }

    public ReadHobbyDto Create(Hobby hobby)
    {
        _context.Hobbies!.Add(hobby);
        _context.SaveChanges();
        return hobby.MapToReadHobby();
    }

    public IEnumerable<ReadHobbyDto> ReadAll() 
    {
        return _context.Hobbies!
            .Include(h => h.Versao).Include(h => h.Versao!.Personagem)
            .Select(h => h.MapToReadHobby()).ToList();
    }

    public ReadHobbyDto? ReadById(int id) 
    {
        var hobby =_context.Hobbies!.FirstOrDefault(h => h.Id == id);
        if (hobby is not null) return hobby.MapToReadHobby();
        else return null;
    }

    public Hobby? ReadEntityById(int id) 
    {
        var hobby =_context.Hobbies!.FirstOrDefault(h => h.Id == id);
        if (hobby is not null) return hobby;
        else return null;
    }

    public ReadHobbyDto? Update(UpdateHobbyDto hobbyDto, Hobby hobby)
    {
        var novoHobby = hobbyDto.MapToHobby(hobby);
        _context.SaveChanges();
        return novoHobby.MapToReadHobby();
    }

    public void Delete(Hobby hobby)
    {
        _context.Hobbies!.Remove(hobby);
        _context.SaveChanges();
    }
}
