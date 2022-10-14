using ScuolaCarloGiorda.Models;

namespace ScuolaCarloGiorda.Services;

public class UsciteService
{
    private readonly SchoolDbContext _schoolDbContext;

    public UsciteService(SchoolDbContext schoolDbContext)
    {
        _schoolDbContext = schoolDbContext;
    }
    public List<Uscita> ListUscite()
    {
        var corsi = _schoolDbContext.Uscite
            .ToList();
        return corsi;
    }

    public void AddUscita(Uscita uscita)
    {
        
        _schoolDbContext.Uscite.Add(uscita);
        _schoolDbContext.SaveChanges();
    }
}