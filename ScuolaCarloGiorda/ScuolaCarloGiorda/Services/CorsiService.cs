using ScuolaCarloGiorda.Models;

namespace ScuolaCarloGiorda.Services;

public class CorsiService
{
    private readonly SchoolDbContext _schoolDbContext;
    public CorsiService(SchoolDbContext schoolDbContext)
    {
        this._schoolDbContext = schoolDbContext;
        
       
    }

    public List<Corso> ListCorsi()
    {
        var corsi = _schoolDbContext.Corsi
            .ToList();
        return corsi;
    }

    public void AddCorso(Corso corso)
    {
        
        _schoolDbContext.Corsi.Add(corso);
        _schoolDbContext.SaveChanges();
    }

}