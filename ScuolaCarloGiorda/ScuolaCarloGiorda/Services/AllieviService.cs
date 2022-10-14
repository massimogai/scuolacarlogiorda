using ScuolaCarloGiorda.Models;

namespace ScuolaCarloGiorda.Services;

public class AllieviService
{
    private readonly SchoolDbContext _schoolDbContext;

    public AllieviService(SchoolDbContext schoolDbContext)
    {
        _schoolDbContext = schoolDbContext;
    }
    
    public List<Allievo> ListAllievi()
    {
        var corsi = _schoolDbContext.Allievi
            .ToList();
        return corsi;
    }

    public void AddAllievo(Allievo allievo)
    {
        
        _schoolDbContext.Allievi.Add(allievo);
        _schoolDbContext.SaveChanges();
    }

    public void Delete(Allievo allievo)
    {
        _schoolDbContext.Allievi.Remove(allievo);
        _schoolDbContext.SaveChanges();
    }
    public void Save()
    {
        _schoolDbContext.SaveChanges();
    }
}