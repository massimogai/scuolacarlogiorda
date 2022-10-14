using ScuolaCarloGiorda.Models;

namespace ScuolaCarloGiorda.Services;

public class IstruttoriService
{
    private readonly SchoolDbContext _schoolDbContext;

    public IstruttoriService(SchoolDbContext schoolDbContext)
    {
        _schoolDbContext = schoolDbContext;
    }
    
    public List<Istruttore> ListIstruttori()
    {
        var corsi = _schoolDbContext.Istruttori
            .ToList();
        return corsi;
    }

    public void AddIstruttore(Istruttore corso)
    {
        
        _schoolDbContext.Istruttori.Add(corso);
        _schoolDbContext.SaveChanges();
    }
    
    public void Delete(Istruttore istruttore)
    {
        _schoolDbContext.Istruttori.Remove(istruttore);
        _schoolDbContext.SaveChanges();
    }
    public void Save()
    {
        _schoolDbContext.SaveChanges();
    }
    public void DiscardChanges(Istruttore istruttore)
    {
        _schoolDbContext.Entry(istruttore).Reload();
    }
}